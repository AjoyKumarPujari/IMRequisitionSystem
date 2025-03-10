using Dapper;
using Holiday_Home.Util;
using IMRequisitionSystem.Models;
using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Repository.Requisition;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace IMRequisitionSystem.Repository.Approver
{
    public class RequisitionApproveList : BaseRepository, IRequisitionApproveList
    {
        public List<RequisitionRequestModel> GetRequisitionApproveList()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GetRequisitionApproveListViaApproverID");
                parameters.Add("@Approve_By", SessionData.GetSessionUserCode());

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

        public RequisitionRequestModel GetDetailsPageForUnitIncharge(string requisition_No)
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


        public SPOutputMessage HODApproveUpdate(RequisitionRequestModel requisitionRequestModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "UPDATE_HOD_APPROVE");
                parameters.Add("@Approver_Comment", requisitionRequestModel.Approver_Comment);
                parameters.Add("@Requisition_No", requisitionRequestModel.Requisition_No);
               
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

        public List<RequisitionRequestModel> GetRequisitionApprovedList()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "HOD_APPROVED_REQUISITION_DATA");
                //xparameters.Add("@Approve_By", SessionData.GetSessionUserCode());
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


        public SPOutputMessage HODRejectUpdate(RequisitionRequestModel requisitionRequestModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "UPDATE_HOD_REJECT");
                parameters.Add("@Reject_Reason", requisitionRequestModel.Reject_Reason);
                parameters.Add("@Requisition_No", requisitionRequestModel.Requisition_No);

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


    }
}