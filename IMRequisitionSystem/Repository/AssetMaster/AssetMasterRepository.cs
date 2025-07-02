using System;
using System.Collections.Generic;
using System.Linq;
using IMRequisitionSystem.Util;
using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Models;
using Dapper;
using Holiday_Home.Util;
using System.Data;
using IMRequisitionSystem.Models.RoleMaster;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using IMRequisitionSystem.Controllers;

namespace IMRequisitionSystem.Repository
{
    public class AssetMasterRepository : BaseRepository, IAssetMasterRepository
    {
        public SPOutputMessage InsertAssetMaster(AssetsModel assetsModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "INSERT");
                parameters.Add("@DeviceType", assetsModel.DeviceType);
                parameters.Add("@AsssetDeviceCharacteristics", assetsModel.AsssetDeviceCharacteristics);
                parameters.Add("@AssetMakeNo", assetsModel.AssetMakeNo);
                parameters.Add("@AssetModelNo", assetsModel.AssetModelNo);
                parameters.Add("@Asset_Sl_No", assetsModel.Asset_Sl_No);
                parameters.Add("@Asset_Id_SAP", assetsModel.Asset_Id_SAP);  
                parameters.Add("@LicenseNo", assetsModel.LicenseNo);
                parameters.Add("@ISChargerSet", assetsModel.ISChargerSet);
                parameters.Add("@Installation_date", assetsModel.Installation_date);
                parameters.Add("@PO_No", assetsModel.PO_No);
                parameters.Add("@PO_date", assetsModel.PO_date);
                parameters.Add("@Vendor_code", assetsModel.Vendor_code);
                parameters.Add("@Vendor_name", assetsModel.Vendor_name);
                parameters.Add("@asset_custodian_location", assetsModel.asset_custodian_location);
                parameters.Add("@asset_custodian_department", assetsModel.asset_custodian_department);
                parameters.Add("@asset_custodian_area", assetsModel.asset_custodian_area);
                parameters.Add("@Warranty_Till", assetsModel.Warranty_Till);
                parameters.Add("@asset_Status", assetsModel.asset_Status);
                parameters.Add("@CreatedBy", SessionData.GetSessionUserCode());

                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("Asset_Master_SP", parameters, reader =>
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

        public List<AssetsModel> GetAllAssetMaster()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ALL_ASSETDATA_FOR_LIST_VIEW");
                parameters.Add("@Emp_loc_code", SessionData.GetSessionData(SessionData.Emp_loc_code));

                return ExecuteStoredProcedure("Asset_Master_SP", parameters, reader =>
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


        public List<AssetsModel> GetAllModelNo()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ALL_MODEL_NO");
                return ExecuteStoredProcedure("Asset_Master_SP", parameters, reader =>
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
        public List<AssetsModel> GetAllLicenseNo()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ALL_LICENSE_NO");
                return ExecuteStoredProcedure("Asset_Master_SP", parameters, reader =>
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

        public List<AssetsModel> GetAllAllocateAssetMaster()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ALL_ALLOCATED_ASSETDATA_FOR_LIST_VIEW");
                parameters.Add("@CreatedBy", SessionData.GetSessionUserCode());

                return ExecuteStoredProcedure("Asset_Master_SP", parameters, reader =>
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

       
        public SPOutputMessage UpdateActiveDeActiveAssetsStatus(AssetsModel assetsModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "UPDATE_ASSET_ACTIVE_STATUS");
                parameters.Add("@Asset_Code_System", assetsModel.Asset_Code_System);
                parameters.Add("@Last_Modified_By", SessionData.GetSessionUserCode());

                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("Asset_Master_SP", parameters, reader =>
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
        public SPOutputMessage UpdateActivePhysicalCondition(AssetsModel assetsModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "UPDATE_ASSET_PHYSICAL_CONDITION");
                parameters.Add("@Asset_Code_System", assetsModel.Asset_Code_System);
                parameters.Add("@Last_Modified_By", SessionData.GetSessionUserCode());

                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("Asset_Master_SP", parameters, reader =>
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


        public List<AssetsModel> GetHealthCheckupDueAsset()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_HEALTH_CHECKUP_ASSET_DATA_FOR_LIST_VIEW");

                return ExecuteStoredProcedure("Periodic_Health_Checkup_of_Assets_SP", parameters, reader =>
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


        public SPOutputMessage UpdateActiveDeActiveAsCreateHealthCheckUpUpdatesetsStatus(AssetsModel assetsModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "HealthCheckUpUpdate");
                parameters.Add("@Health_Checkup_Remarks", assetsModel.Health_Checkup_Remarks);
                parameters.Add("@asset_Physical_Condition", assetsModel.asset_Physical_Condition);
                parameters.Add("@Asset_Code_System", assetsModel.Asset_Code_System);
                parameters.Add("@HealthCheckUpBy", SessionData.GetSessionUserCode());

                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("Periodic_Health_Checkup_of_Assets_SP", parameters, reader =>
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

        public List<AssetsModel> GetHealthCheckupHistory()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_HEALTH_CHECKUP_HISTORY_ASSET_DATA_FOR_LIST_VIEW");

                return ExecuteStoredProcedure("Periodic_Health_Checkup_of_Assets_SP", parameters, reader =>
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

        
        public List<AssetsModel> GetAllIssuedAssetMaster()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ALL_ISSUED_ASSETDATA_FOR_LIST_VIEW");

                return ExecuteStoredProcedure("Asset_Master_SP", parameters, reader =>
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

        public AssetModelForAPI GetDetailsByWorkOrderNoFromProduction(string id)
        {
            try
            {
                List<AssetModelForAPI> workerDetails = new List<AssetModelForAPI>();
                string url = string.Format("http://webprdev/APIValidatePO/api/icwcs/get_po_details?pono={0}", id);

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = WebRequestMethods.Http.Get;
                request.ContentType = "application/json; charset=utf-8";
                WebResponse response = request.GetResponse();

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string strsb = reader.ReadToEnd();

                    var jsonResult = JsonConvert.DeserializeObject(strsb).ToString();
                    workerDetails = JsonConvert.DeserializeObject<List<AssetModelForAPI>>(jsonResult);
                }

                if (workerDetails == null)
                {
                    return new AssetModelForAPI();
                }
                if (workerDetails.Count <= 0)
                {
                    return new AssetModelForAPI();
                }

                return workerDetails[0];
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new AssetModelForAPI();

            }
        }

        public AssetsModel GetAssetsDataBySAPID(string id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ASSET_DATA_BY_ASSERSAPID");
                parameters.Add("@Asset_Code_System", id);

                return ExecuteStoredProcedure("Asset_Master_SP", parameters, reader =>
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

        public SPOutputMessage InsertAssetAMCMasterMapping(AssetsModel assetsModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "CREATE");

                parameters.Add("@PO_No", assetsModel.PO_No);
                parameters.Add("@Vendor_code", assetsModel.Vendor_code);
                parameters.Add("@Vendor_name", assetsModel.Vendor_name);
                //parameters.Add("@Asset_Code_System", assetsModel.Asset_Code_System);
                parameters.Add("@Asset_Code_System", assetsModel.Asset_Id_SAP);
                parameters.Add("@Asset_Sl_No", assetsModel.Asset_Sl_No);
                parameters.Add("@AssetMakeNo", assetsModel.AssetMakeNo);
                parameters.Add("@AssetModelNo", assetsModel.AssetModelNo);
                parameters.Add("@Installation_date", assetsModel.Installation_date);
                parameters.Add("@DeviceType", assetsModel.DeviceType);
                parameters.Add("@asset_custodian_department", assetsModel.asset_custodian_department);
                parameters.Add("@Latest_AMC_vendor_code", assetsModel.Latest_AMC_vendor_code);
                parameters.Add("@Latest_AMC_vendorname", assetsModel.Latest_AMC_vendorname);
                parameters.Add("@Latest_AMC_expiry", assetsModel.Latest_AMC_expiry);
                parameters.Add("@Latest_AMC_Start_Date", assetsModel.Latest_AMC_Start_Date);
                parameters.Add("@PO_No_For_AMC", assetsModel.PO_No_For_AMC);
                parameters.Add("@Vendor_code_For_AMC", assetsModel.Vendor_code_For_AMC);
                parameters.Add("@Vendor_name_For_AMC", assetsModel.Vendor_name_For_AMC);
                parameters.Add("@AMC_expiry_For_AMC", assetsModel.AMC_expiry_For_AMC);
                parameters.Add("@RPO", assetsModel.RPO);
                parameters.Add("@CreatedBy", SessionData.GetSessionUserCode());

                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("AssetAMCMasterMapping_SP", parameters, reader =>
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

        public List<AssetsModel> GetAllAMCMappingAssetMaster()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "Get_AMC_Asset_Mapping_Data");

                return ExecuteStoredProcedure("AssetAMCMasterMapping_SP", parameters, reader =>
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
        public AssetsModel GetMappingAssetDataByID(string asset_Code_System)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "Get_AMC_Asset_Mapping_Data_ByID");
                parameters.Add("@Asset_Code_System", asset_Code_System);

                return ExecuteStoredProcedure("AssetAMCMasterMapping_SP", parameters, reader =>
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

        

            public SPOutputMessage AMCMappingPageEditUpdate(AssetsModel assetsModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "UpdateRPOPO");

                parameters.Add("@Latest_AMC_PO_NO", assetsModel.AMC_PO_NO);
                
                parameters.Add("@Latest_AMC_vendor_code", assetsModel.Latest_AMC_vendor_code);
                parameters.Add("@Latest_AMC_vendorname", assetsModel.Latest_AMC_vendorname);
                parameters.Add("@Asset_Code_System", assetsModel.Asset_Code_System);
                parameters.Add("@Latest_AMC_Start_Date", assetsModel.Latest_AMC_Start_Date);
                parameters.Add("@Latest_AMC_expiry", assetsModel.Latest_AMC_End_Date);
              
                parameters.Add("@ModifiedBy", SessionData.GetSessionUserCode());

                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("AssetAMCMasterMapping_SP", parameters, reader =>
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
        public List<AssetsModel> GetAllAMCMappingHistoryAssetMaster()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "Get_AMC_Asset_Mapping_History_Data");

                return ExecuteStoredProcedure("AssetAMCMasterMapping_SP", parameters, reader =>
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
        public List<AssetsModel> GetAMCMappingListWithFilterData(AssetsModel assetsModel)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GetAMCMappingListWithFilterData");
                parameters.Add("@PO_No", assetsModel.PO_No);
                parameters.Add("@Asset_Sl_No", assetsModel.Asset_Sl_No);
                parameters.Add("@Asset_Id_SAP", assetsModel.Asset_Id_SAP);

                return ExecuteStoredProcedure("AssetAMCMasterMapping_SP", parameters, reader =>
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

        public List<AssetsModel> GetMasterListWithFilterData(AssetsModel assetsModel)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GetMasterListWithFilterData");
                parameters.Add("@LicenseNo", assetsModel.LicenseNo);
                parameters.Add("@asset_Status", assetsModel.asset_Status);
                parameters.Add("@AssetModelNo", assetsModel.AssetModelNo);

                return ExecuteStoredProcedure("Asset_Master_SP", parameters, reader =>
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
        public List<AssetsModel> GetAMCMappingHistoryListWithFilterData(AssetsModel assetsModel)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "Get_AMC_Asset_Mapping_History_Filter_Data");
                parameters.Add("@PO_No", assetsModel.PO_No);
                parameters.Add("@Asset_Sl_No", assetsModel.Asset_Sl_No);
                parameters.Add("@Asset_Id_SAP", assetsModel.Asset_Id_SAP);

                return ExecuteStoredProcedure("AssetAMCMasterMapping_SP", parameters, reader =>
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
        public List<AssetsModel> GetHealthCheckupListWithFilterData(AssetsModel assetsModel)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_HEALTH_CHECKUP_HISTORY_ASSET_DATA_FOR_LIST_VIEW_WITH_FILTER");
                parameters.Add("@Asset_Sl_No", assetsModel.Asset_Sl_No);
                parameters.Add("@Asset_Id_SAP", assetsModel.Asset_Id_SAP);

                return ExecuteStoredProcedure("Periodic_Health_Checkup_of_Assets_SP", parameters, reader =>
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

    }



}