using Dapper;
using Holiday_Home.Util;
using IMRequisitionSystem.Models;
using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Repository.Requisition
{
    public class RequisitionRequestRepository : BaseRepository, IRequisitionRequestRepository
    {
        public SPOutputMessage CreateRequisitionRequest(RequisitionRequestModel requisitionRequestModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "RequestRequisition");
                parameters.Add("@Requestor_ID", SessionData.GetSessionUserCode());
                parameters.Add("@DeviceType", requisitionRequestModel.DeviceType);
                parameters.Add("@Requisition_Date", requisitionRequestModel.Requisition_Date);
                parameters.Add("@Use_Location", requisitionRequestModel.Use_Location);
                parameters.Add("@Use_Department", requisitionRequestModel.Use_Department);
                parameters.Add("@Use_Area", requisitionRequestModel.Use_Area);
                parameters.Add("@RequisitionFor", requisitionRequestModel.RequisitionFor);
                parameters.Add("@RequisitionForWhome", requisitionRequestModel.RequisitionForWhome);
                parameters.Add("@Non_Management_staff", requisitionRequestModel.Non_Management_staff);
                parameters.Add("@ThirdParty", requisitionRequestModel.ThirdParty);
                parameters.Add("@CISF", requisitionRequestModel.CISF);
                parameters.Add("@Approve_By", requisitionRequestModel.Approve_By);
                parameters.Add("@Purpose_Of_Use", requisitionRequestModel.Purpose_Of_Use);

                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
                //parameters.Add("@PK", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("Requisition_Details_SP", parameters, reader =>
                {
                    return reader.ReadFirstOrDefault<int>();
                });

                spResponse = new SPOutputMessage
                {
                    Status = parameters.Get<int>("@Status"),
                    Message = parameters.Get<string>("@Message"),
                    //PK = parameters.Get<string>("@PK")
                };
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return spResponse;
        }


        

        public List<RequisitionRequestModel> GetRequisitionDetailsForRequestor()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "Get_RequisitionData_viaEmpCode");
                parameters.Add("@Requestor_ID", SessionData.GetSessionUserCode());

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

        public List<RequisitionRequestModel> GetRequisitionDetailsForAllocator()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "Get_RequisitionDataForAllocation_viaEmpCode");
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

    }
}