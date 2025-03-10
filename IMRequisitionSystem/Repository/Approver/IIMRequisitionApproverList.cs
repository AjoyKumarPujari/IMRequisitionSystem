using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMRequisitionSystem.Models;
using IMRequisitionSystem.Models.Assets;


namespace IMRequisitionSystem.Repository.Approver
{
    public interface IIMRequisitionApproverList
    {
      List<RequisitionRequestModel> GetRequisitionIMApproverList();

      RequisitionRequestModel GetDetailsDataForIMApprover(string requisition_No);

     SPOutputMessage IMApproverUpdate(RequisitionRequestModel requisitionRequestModel);
        
     List<RequisitionRequestModel> GetRequisitionIMApprovedList();
    }
}
