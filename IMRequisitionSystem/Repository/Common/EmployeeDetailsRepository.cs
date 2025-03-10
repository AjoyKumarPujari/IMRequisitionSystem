using Dapper;
using IMRequisitionSystem.Models;
using IMRequisitionSystem.Models.Area;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Repository.Common
{
    public class EmployeeDetailsRepository : BaseRepository, IEmployeeDetailsRepository
    {

        public List<EmployeeModel> GetAllNonmanagementUserData()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ALL_NON_MNGMNT_USER_DATA");

                return ExecuteStoredProcedure("UserMaster_SP", parameters, reader =>
                {
                    return reader.Read<EmployeeModel>().AsList();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new List<EmployeeModel>();
            }
        }
        public List<EmployeeModel> GetApproverDataViaGreade(string employeeCode)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_USER_DATA_VIA_GREADE");
                parameters.Add("@EmployeeCode", employeeCode);

                return ExecuteStoredProcedure("UserMaster_SP", parameters, reader =>
                {
                    return reader.Read<EmployeeModel>().AsList();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new List<EmployeeModel>();
            }
        }

        public List<EmployeeModel> GetAllManagementUserData()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ALL_MNGMNT_USER_DATA");

                return ExecuteStoredProcedure("UserMaster_SP", parameters, reader =>
                {
                    return reader.Read<EmployeeModel>().AsList();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new List<EmployeeModel>();
            }
        }



    }
}