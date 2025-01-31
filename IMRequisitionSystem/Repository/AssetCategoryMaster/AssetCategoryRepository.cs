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
using System.Web.Mvc;

namespace IMRequisitionSystem.Repository
{
    public class AssetCategoryRepository : BaseRepository, IAssetCategoryRepository
    {
        public SPOutputMessage InsertAssetCategoryMaster(AssetCategoryModel assetsCategoryModel)
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
                parameters.Add("@DeviceType", assetsCategoryModel.DeviceType);
                parameters.Add("@DeviceDescription", assetsCategoryModel.DeviceDescription);
                parameters.Add("@Location", assetsCategoryModel.Location);
                parameters.Add("@CreatedBy", SessionData.GetSessionUserCode());
                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("AssetCategory_SP", parameters, reader =>
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

       


        public List<AssetCategoryModel> GetAllAssetCategoryMaster()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ALL_ASSET_CATEGORY_FOR_LIST_VIEW");

                return ExecuteStoredProcedure("AssetCategory_SP", parameters, reader =>
                {
                    return reader.Read<AssetCategoryModel>().AsList();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new List<AssetCategoryModel>();
            }
        }

        public List<AssetCategoryModel> GetAllAssetCategoryForDropDown()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ALL_CATEGORY_FOR_DROP_DOWN");

                return ExecuteStoredProcedure("AssetCategory_SP", parameters, reader =>
                {
                    return reader.Read<AssetCategoryModel>().AsList();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new List<AssetCategoryModel>();
            }
        }

        public SPOutputMessage UpdateActiveStatus(int ID)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "UPDATE_ACTIVE_STATUS");
                parameters.Add("@ID", ID);
                parameters.Add("@ModifiedBy", SessionData.GetSessionUserCode());

                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("AssetCategory_SP", parameters, reader =>
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