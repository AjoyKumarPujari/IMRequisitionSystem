using Dapper;
using Holiday_Home.Util;
using IMRequisitionSystem.Models;
using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Models.RoleMaster;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Repository.RoleMaster
{

    public class RoleMasterRepository : BaseRepository, IRoleMasterRepository
    {
        

        public SPOutputMessage InsertRoleMaster(RoleMasterModel roleMasterModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "INSERT_ROLE_MASTER");
                parameters.Add("@Role_Name", roleMasterModel.Role_Name);
                parameters.Add("@Role_Description", roleMasterModel.Role_Description);
                parameters.Add("@Role_Created_By", SessionData.GetSessionUserCode());

                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("RoleMaster_SP", parameters, reader =>
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
        public List<RoleMasterModel> GetAllRoleMaster()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ALL_ROLE_INFO_FOR_LIST_VIEW");

                return ExecuteStoredProcedure("RoleMaster_SP", parameters, reader =>
                {
                    return reader.Read<RoleMasterModel>().AsList();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new List<RoleMasterModel>();
            }
        }

        public SPOutputMessage UpdateActiveDeActiveStatus(RoleMasterModel roleMasterModel)
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
                parameters.Add("@Role_ID", roleMasterModel.Role_Id);
                parameters.Add("@ModifiedBy", SessionData.GetSessionUserCode());

                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


                ExecuteStoredProcedure("RoleMaster_SP", parameters, reader =>
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

        public List<RoleMasterModel> GetAllRoleMasterForDropDown()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ALL_ROLE_INFO_FOR_DROPDOWN");

                return ExecuteStoredProcedure("RoleMaster_SP", parameters, reader =>
                {
                    return reader.Read<RoleMasterModel>().AsList();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new List<RoleMasterModel>();
            }
        }
    }
}