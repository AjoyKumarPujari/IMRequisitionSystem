using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Models.LocationAdmin
{
    public class ApprovedModel
    {

        public string RequisitionRequestDate { get; set; }
        public string Location { get; set; }
        public string Area { get; set; }
        public string Department { get; set; }
        public string RequisitionFor { get; set; }
        public string OthersName { get; set; }
        public string PurposeofRequisition { get; set; }
        public string RequestStatus { get; set; }


        public string Approved { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovedByName { get; set; }
        public string ApprovedByDesignation { get; set; }
        public string ApprovedByRemarks { get; set; }

        public string RejectRemarks { get; set; }
        public string Rejected { get; set; }

        public string Confirmed { get; set; }
        public string ConfirmedBy { get; set; }
        public string ConfirmedByName { get; set; }
        public string ConfirmedDate { get; set; }
        public string ConfirmedRemarks { get; set; }

        public string HPODnConfirmedBy { get; set; }
        public string HODConfirmedByName { get; set; }
        public string HODConfirmedDate { get; set; }
        public string HODRemarks { get; set; }

        public string CancelledDate { get; set; }
        public string CancelledBy { get; set; }
        public string CancelledByName { get; set; }
        public string CancelledRemarks { get; set; }
    }
}