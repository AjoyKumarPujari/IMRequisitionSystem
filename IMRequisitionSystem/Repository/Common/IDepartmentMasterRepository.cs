using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRequisitionSystem.Repository.Common
{
    public interface IDepartmentMasterRepository
    {
        List<DepartmentModel> GetAllDepartmentMaster();
    }
}
