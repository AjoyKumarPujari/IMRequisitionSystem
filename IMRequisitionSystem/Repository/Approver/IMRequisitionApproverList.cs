using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Models;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IMRequisitionSystem.Util;
using System.Data;
using Holiday_Home.Util;

namespace IMRequisitionSystem.Repository.Approver
{
    public class IMRequisitionApproverList : BaseRepository, IIMRequisitionApproverList
    {
        public List<RequisitionRequestModel> GetRequisitionIMApproverList()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "REQUISITION_DATA_FOR_IM_APPROVER");
                //parameters.Add("@Approve_By", SessionData.GetSessionUserCode());
                return ExecuteStoredProcedure("Requisition_Details_SP", parameters, reader =>
                {
                    return reader.Read<RequisitionRequestModel>().AsList();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new List<RequisitionRequestModel>();
            }
        }

        public RequisitionRequestModel GetDetailsDataForIMApprover(string requisition_No)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_REQUISITION_DETAILS_BY_requisition_No");
                parameters.Add("@Requisition_No", requisition_No);

                return ExecuteStoredProcedure("Requisition_Details_SP", parameters, reader =>
                {
                    return reader.ReadFirstOrDefault<RequisitionRequestModel>();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new RequisitionRequestModel();
            }
        }


        public SPOutputMessage IMApproverUpdate(RequisitionRequestModel requisitionRequestModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "UPDATE_IM_APPROVE");
                parameters.Add("@IM_Approver_Comment", requisitionRequestModel.IM_Approver_Comment);
                parameters.Add("@Requisition_No", requisitionRequestModel.Requisition_No);
                parameters.Add("@IM_Approve_By", SessionData.GetSessionUserCode());
                

                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("Requisition_Details_SP", parameters, reader =>
                {
                    return reader.ReadFirstOrDefault<int>();
                });

                spResponse = new SPOutputMessage
                {
                    Status = parameters.Get<int>("@Status"),
                    Message = parameters.Get<string>("@Message"),
                };

            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return spResponse;
        }


        public List<RequisitionRequestModel> GetRequisitionIMApprovedList()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_IM_APPROVED_LIST");
                //parameters.Add("@Approve_By", SessionData.GetSessionUserCode());
                return ExecuteStoredProcedure("Requisition_Details_SP", parameters, reader =>
                {
                    return reader.Read<RequisitionRequestModel>().AsList();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new List<RequisitionRequestModel>();
            }
        }

    }
}