using Dapper;
using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Models.Department;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Repository.Common
{

    public class DepartmentMasterRepository : BaseRepository , IDepartmentMasterRepository
    {
        public List<DepartmentModel> GetAllDepartmentMaster()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ALL_DEPARTMENT_FOR_LIST_VIEW");

                return ExecuteStoredProcedure("DepartmentMaster_SP", parameters, reader =>
                {
                    return reader.Read<DepartmentModel>().AsList();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new List<DepartmentModel>();
            }
        }
    
    }
}