using IMRequisitionSystem.Models;
using IMRequisitionSystem.Models.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRequisitionSystem.Repository.Approver
{
    public interface IRequisitionApproveList
    {
        List<RequisitionRequestModel> GetRequisitionApproveList();
        
        RequisitionRequestModel GetDetailsPageForUnitIncharge(string requisition_No);

        SPOutputMessage HODApproveUpdate(RequisitionRequestModel requisitionRequestModel);
        SPOutputMessage HODRejectUpdate(RequisitionRequestModel requisitionRequestModel);

        List<RequisitionRequestModel> GetRequisitionApprovedList();
        
    }
}
