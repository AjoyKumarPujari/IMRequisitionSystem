using Dapper;
using Holiday_Home.Util;
using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Models;
using IMRequisitionSystem.Repository.Requisition;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Repository.Return
{
    public class ReturnRepository : BaseRepository, IReturnRepository
    {
        public SPOutputMessage RequisitionReturn(RequisitionRequestModel requisitionRequestModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "Requisition_Return_Request");
                parameters.Add("@Return_Comment", requisitionRequestModel.Return_Comment);
                parameters.Add("@asset_Physical_Condition", requisitionRequestModel.asset_Physical_Condition);
                parameters.Add("@Asset_Code_System", requisitionRequestModel.Asset_Code_System);
                parameters.Add("@Asset_return_by", SessionData.GetSessionUserCode());


                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("Return_Details_SP", parameters, reader =>
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

        public List<AssetsModel> GetAllReturnRequestedAssetMaster()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ASSET_RETURNED_LIST");
                //parameters.Add("@Approve_By", SessionData.GetSessionUserCode());
                return ExecuteStoredProcedure("Return_Details_SP", parameters, reader =>
                {
                    return reader.Read<AssetsModel>().AsList();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new List<AssetsModel>();
            }
        }

        public List<AssetsModel> GetAllReturnRequestedAssetMasterOnBehaveOf()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ASSET_RETURNED_LIST_OnBehave_Of");
                //parameters.Add("@Approve_By", SessionData.GetSessionUserCode());
                return ExecuteStoredProcedure("Return_Details_SP", parameters, reader =>
                {
                    return reader.Read<AssetsModel>().AsList();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new List<AssetsModel>();
            }
        }

        public SPOutputMessage RequisitionReturnAcknowledge(AssetsModel assetsModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "Requisition_Return_Acknowledge");
                parameters.Add("@Return_Acknowledge_Comment", assetsModel.Return_Acknowledge_Comment);
                parameters.Add("@Return_Request_ID", assetsModel.Return_Request_ID);
                parameters.Add("@Returned_Received_By", SessionData.GetSessionUserCode());


                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("Return_Details_SP", parameters, reader =>
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

        public List<AssetsModel> GetAllReturnReceivedAssetMaster()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ASSET_RETURNED_RECEIVED_LIST");
                //parameters.Add("@Approve_By", SessionData.GetSessionUserCode());
                return ExecuteStoredProcedure("Return_Details_SP", parameters, reader =>
                {
                    return reader.Read<AssetsModel>().AsList();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new List<AssetsModel>();
            }
        }

        public AssetsModel GetDetailsDataForReturn(string return_Request_ID)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_RETURN_REQUISITION_DETAILS_BY_return_Request_ID");
                parameters.Add("@Return_Request_ID", return_Request_ID);

                return ExecuteStoredProcedure("Return_Details_SP", parameters, reader =>
                {
                    return reader.ReadFirstOrDefault<AssetsModel>();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new AssetsModel();
            }
        }
    }
}