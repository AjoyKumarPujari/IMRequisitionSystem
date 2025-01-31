using IMRequisitionSystem.Models;
using IMRequisitionSystem.Models.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRequisitionSystem.Repository.Requisition
{
    public interface IRequisitionRequestRepository
    {
        SPOutputMessage CreateRequisitionRequest(RequisitionRequestModel requisitionRequestModel );
        List<RequisitionRequestModel> GetRequisitionDetailsForRequestor();
        List<RequisitionRequestModel> GetRequisitionDetailsForAllocator();

    }
}
