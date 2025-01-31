using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMRequisitionSystem.Util;
using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Models;
using Dapper;
using Holiday_Home.Util;
using System.Data;

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
    }
}