using IMRequisitionSystem.Models.Assets;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMRequisitionSystem.Models;

namespace IMRequisitionSystem.Repository.Issuer
{
    public interface IIMIssuerRepository
    {

        List<RequisitionRequestModel> GetRequisitionIssuerList();
        RequisitionRequestModel CheckAvailabilityOfDevice(string deviceTypeId);
        List<RequisitionRequestModel> GetDeviceTypeID(string deviceTypeId);

        SPOutputMessage IMIssuerUpdate(RequisitionRequestModel requisitionRequestModel);

        List<RequisitionRequestModel> GetRequisitionIMIssuedList();
        List<RequisitionRequestModel> GetRequisitionIMIssuedListForAllocator();
        List<RequisitionRequestModel> GetRequisitionAllocatedList();

        SPOutputMessage IMAllocatorUpdate(RequisitionRequestModel requisitionRequestModel);

        RequisitionRequestModel GetDetailsDataForIssuer(string requisition_No);
       

        SPOutputMessage IssuerCloseUpdate(RequisitionRequestModel requisitionRequestModel);

    }
}
