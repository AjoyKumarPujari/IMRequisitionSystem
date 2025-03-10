using IMRequisitionSystem.Models.RoleMapping;
using IMRequisitionSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMRequisitionSystem.Models.RoleMaster;
using IMRequisitionSystem.Models.Assets;

namespace IMRequisitionSystem.Repository.RoleMaster
{
    public interface IRoleMappingRepository
    {
        SPOutputMessage InsertUserRoleMapping(RoleMappingModel roleMappingModel);
        List<RoleMappingModel> GetAllRoleMappingMaster();
        SPOutputMessage UpdateActiveDeActiveMappingStatus(RoleMappingModel roleMappingModel);

    }
}
