using IMRequisitionSystem.Models.Area;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    public class AreaMasterRepository : BaseRepository, IAreaMasterRepository
    {
        public List<AreaModel> GetAllAreaMaster()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_ALL_AREA_FOR_LIST_VIEW");

                return ExecuteStoredProcedure("DepartmentMaster_SP", parameters, reader =>
                {
                    return reader.Read<AreaModel>().AsList();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new List<AreaModel>();
            }
        }

    }
}