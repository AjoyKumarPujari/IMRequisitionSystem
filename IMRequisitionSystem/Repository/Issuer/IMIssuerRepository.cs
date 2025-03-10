using Dapper;
using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Holiday_Home.Util;
using IMRequisitionSystem.Models;

namespace IMRequisitionSystem.Repository.Issuer
{

    public class IMIssuerRepository : BaseRepository, IIMIssuerRepository
    {
        public List<RequisitionRequestModel> GetRequisitionIssuerList()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "REQUISITION_DATA_FOR_IM_ISSUER");
                //parameters.Add("@Approve_By", SessionData.GetSessionUserCode());
                return ExecuteStoredProcedure("Requisition_Details_ISSUER_SP", parameters, reader =>
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

        public RequisitionRequestModel CheckAvailabilityOfDevice(string deviceTypeId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_DEVICE_Quantity_BY_deviceTypeId");
                parameters.Add("@DeviceType", deviceTypeId);
               

                return ExecuteStoredProcedure("Requisition_Details_ISSUER_SP", parameters, reader =>
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

        
        public List<RequisitionRequestModel> GetDeviceTypeID(string deviceTypeId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_DEVICE_Id");
                parameters.Add("@DeviceType", deviceTypeId);
                return ExecuteStoredProcedure("Requisition_Details_ISSUER_SP", parameters, reader =>
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

        public SPOutputMessage IMIssuerUpdate(RequisitionRequestModel requisitionRequestModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "UPDATE_IM_Issuer");
                parameters.Add("@IM_Issuer_Comment", requisitionRequestModel.IM_Issuer_Comment);
                parameters.Add("@Requisition_No", requisitionRequestModel.Requisition_No);
                parameters.Add("@Asset_Code_System", requisitionRequestModel.Asset_Sl_No);
                parameters.Add("@DeviceType", requisitionRequestModel.DeviceType);
                parameters.Add("@IM_Issued_By", SessionData.GetSessionUserCode());


                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("Requisition_Details_ISSUER_SP", parameters, reader =>
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


        public List<RequisitionRequestModel> GetRequisitionIMIssuedList()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_IM_ISSUED_LIST");
                //parameters.Add("@Approve_By", SessionData.GetSessionUserCode());
                return ExecuteStoredProcedure("Requisition_Details_ISSUER_SP", parameters, reader =>
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

        public List<RequisitionRequestModel> GetRequisitionIMIssuedListForAllocator()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_IM_ISSUED_LIST");
                //parameters.Add("@Approve_By", SessionData.GetSessionUserCode());
                return ExecuteStoredProcedure("Requisition_Details_ISSUER_SP", parameters, reader =>
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



        public SPOutputMessage IMAllocatorUpdate(RequisitionRequestModel requisitionRequestModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "UPDATE_IM_Allocator");
                parameters.Add("@IM_Allocator_Comment", requisitionRequestModel.IM_Allocator_Comment);
                parameters.Add("@Requisition_No", requisitionRequestModel.Requisition_No);
               
                parameters.Add("@IM_Allocated_By", SessionData.GetSessionUserCode());


                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("Requisition_Details_ISSUER_SP", parameters, reader =>
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

        public List<RequisitionRequestModel> GetRequisitionAllocatedList()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_IM_Allocated_LIST");
                //parameters.Add("@Approve_By", SessionData.GetSessionUserCode());
                return ExecuteStoredProcedure("Requisition_Details_ISSUER_SP", parameters, reader =>
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


        public RequisitionRequestModel GetDetailsDataForIssuer(string requisition_No)
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



        public SPOutputMessage IssuerCloseUpdate(RequisitionRequestModel requisitionRequestModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "CLOSED_BY_ISSUER");
                parameters.Add("@Closed_Remarks", requisitionRequestModel.Closed_Remarks);
                parameters.Add("@Requisition_No", requisitionRequestModel.Requisition_No);
                parameters.Add("@Closed_By", SessionData.GetSessionUserCode());


                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("Requisition_Details_ISSUER_SP", parameters, reader =>
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