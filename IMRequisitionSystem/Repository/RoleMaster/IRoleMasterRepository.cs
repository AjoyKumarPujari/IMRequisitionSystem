using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMRequisitionSystem.Models.RoleMaster;
using IMRequisitionSystem.Models.RoleMapping;

namespace IMRequisitionSystem.Repository.RoleMaster
{
    public interface IRoleMasterRepository
    {
        SPOutputMessage InsertRoleMaster(RoleMasterModel roleMasterModel);
     
        List<RoleMasterModel> GetAllRoleMaster();
        List<RoleMasterModel> GetAllRoleMasterForDropDown();
        SPOutputMessage UpdateActiveDeActiveStatus(RoleMasterModel roleMasterModel);
       
    }
}
