using IMRequisitionSystem.Models.Area;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRequisitionSystem.Repository.Common
{
    public interface IAreaMasterRepository
    {
        List<AreaModel> GetAllAreaMaster();
    }
}
