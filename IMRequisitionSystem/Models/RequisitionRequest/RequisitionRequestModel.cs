using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Models.Assets
{
    public class RequisitionRequestModel
    {

        public string Requisition_No { get; set; }
        public string Requisition_Date { get; set; }
        public string Requestor_Name { get; set; }
        public string Requestor_Designation { get; set; }
        public string Requestor_ID { get; set; }
        public string RequisitionFor { get; set; }
        public string RequisitionForWhome { get; set; }
        public string Non_Management_staff { get; set; }
        public string CISF { get; set; }
        public string ThirdParty { get; set; }
        public string Requestor_Department { get; set; }
        public string Purpose_Of_Use { get; set; }
        public string DeviceType { get; set; }
        public string DeviceName { get; set; }
        public string Use_Location { get; set; }
        public string Use_Department { get; set; }
        public string Use_Department_Name { get; set; }
        public string Use_Area { get; set; }
        public string Use_Area_Name { get; set; }
        public string Unit_In_Charge { get; set; }
        public string Created_By { get; set; }
        public string Created_DateTime { get; set; }
        public string Approve_By { get; set; }
        public string Approver_Name { get; set; }
        public string Approver_Designation { get; set; }
        public string Approve_DateTime { get; set; }
        public string Approver_Comment { get; set; }
        public string Approver_Role { get; set; }
        public string Cancelled_By { get; set; }
        public string Cancelled_DateTime { get; set; }
        public string Revert_By { get; set; }
        public string Revert_DateTime { get; set; }
        public string Revert_Remarks { get; set; }
        public string Last_Modified_By { get; set; }
        public string Last_Modified_DateTime { get; set; }
        public string Requisition_Status { get; set; }
        public string Issue_Order_Number { get; set; }
        public string Issue_Order_DateTime { get; set; }
        public string Issued_Asset_ID { get; set; }
        public string Issued_Asset_Serial_No { get; set; }
        public string Asset_Allocated { get; set; }
        public string Closed_By { get; set; }
        public string Closed_DateTime { get; set; }
        public string Closed_Remarks { get; set; }
        public string Recall_By { get; set; }
        public string Recall_DateTime { get; set; }
        public string Reject_By { get; set; }
        public string Reject_DateTime { get; set; }
        public string Reject_Reason { get; set; }
        public string IM_Approver_Comment { get; set; }
        public string IM_Approve_By { get; set; }
        public string IM_Approve_DateTime { get; set; }
        public string IM_Approver_Name { get; set; }
        public string IM_Approver_Designation { get; set; }
        public string DeviceQuantity { get; set; }
        public string TotalAssetCount { get; set; }
        public string Asset_Sl_No { get; set; }
        public string Asset_Code_System { get; set; }
       
        
        public string IM_Issuer_Comment { get; set; }
        public string IM_Issued_By { get; set; }
        public string IM_Issued_DateTime { get; set; }
        public string IM_Issuer_Designation { get; set; }
        public string IM_Issuer_Name { get; set; }


        public string IM_Allocator_Comment { get; set; }
        public string IM_Allocated_By { get; set; }
        public string IM_Allocated_DateTime { get; set; }
        public string IM_Allocator_Designation { get; set; }
        public string IM_Allocator_Name { get; set; }

        public string Return_Comment { get; set; }
        public string asset_Physical_Condition { get; set; }
        public string Cancelled_Reason { get; set; }
        public string RequisitionType { get; set; }
        






    }
}