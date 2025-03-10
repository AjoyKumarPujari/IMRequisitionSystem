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
        List<RequisitionRequestModel> GetRequisitionRequestedList();


        SPOutputMessage CreateRequisitionRequestSaveAsDraft(RequisitionRequestModel requisitionRequestModel);
        List<RequisitionRequestModel> GetRequisitionDraftDetailsForRequestor();

        RequisitionRequestModel GetDetailsDataForRequisiyioner(string requisition_No);

        SPOutputMessage RequisitionCancaled(RequisitionRequestModel requisitionRequestModel);
        SPOutputMessage RequisitionDraftGenerateUpdate(RequisitionRequestModel requisitionRequestModel);


    }
}
