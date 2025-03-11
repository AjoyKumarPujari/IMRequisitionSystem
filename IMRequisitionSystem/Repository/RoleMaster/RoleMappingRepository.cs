using Dapper;
using Holiday_Home.Util;
using IMRequisitionSystem.Models.RoleMaster;
using IMRequisitionSystem.Models;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using IMRequisitionSystem.Models.RoleMapping;

namespace IMRequisitionSystem.Repository.RoleMaster
{
    public class RoleMappingRepository : BaseRepository, IRoleMappingRepository
    {
       
        public SPOutputMessage InsertUserRoleMapping(RoleMappingModel roleMappingModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "INSERT_ROLE_MAPPING_MASTER");
                parameters.Add("@Role_ID", roleMappingModel.Role_Name);
                parameters.Add("@Assigned_To", roleMappingModel.Assigned_To);
                parameters.Add("@Assigned_By", SessionData.GetSessionUserCode());

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

        public List<RoleMappingModel> GetAllRoleMappingMaster()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ALL_ROLE_MAPPING_INFO_FOR_LIST_VIEW");

                return ExecuteStoredProcedure("RoleMaster_SP", parameters, reader =>
                {
                    return reader.Read<RoleMappingModel>().AsList();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new List<RoleMappingModel>();
            }
        }

        public SPOutputMessage UpdateActiveDeActiveMappingStatus(RoleMappingModel roleMappingModel)
        {
            SPOutputMessage spResponse = new SPOutputMessage()
            {
                Status = 2,
                Message = "Something went wrong, try again later."
            };
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Type", "UPDATE_ACTIVE_MAPPING_STATUS");
                parameters.Add("@Mapping_Id", roleMappingModel.Mapping_Id);
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


    }
}