using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Models.Assets
{
    public class AssetsModel
    {
        public int ID { get; set; }
        public string DeviceType { get; set; }
        public string AsssetDeviceCharacteristics { get; set; }
        public string AssetMakeNo { get; set; }
        public string AssetModelNo { get; set; }
        public string Asset_Sl_No { get; set; }
        public string Asset_Id_SAP { get; set; }
        public string ISChargerSet { get; set; }
        public string Installation_date { get; set; }
        public string IsLicensed { get; set; }
        public string LicenseNo { get; set; }
        public string Vendor_code { get; set; }
        public string Vendor_name { get; set; }
        public string PO_No { get; set; }
        public string PO_date { get; set; }
        public string Category_Name { get; set; }
        public string Asset_delivered_Location { get; set; }
        public string Warranty_Till { get; set; }
        public string Under_AMC { get; set; }
        public string Latest_AMC_vendor_code { get; set; }
        public string Latest_AMC_vendorname { get; set; }
        public string Latest_AMC_expiry { get; set; }
        public string asset_custodian_code { get; set; }
        public string asset_custodian_name { get; set; }
        public string asset_custodian_location { get; set; }
        public string asset_custodian_area { get; set; }
        public string asset_custodian_department { get; set; }
        public string asset_Physical_Condition { get; set; } = string.Empty;
        public string Issue_status { get; set; }
        public string asset_Status { get; set; }
        public string Remarks { get; set; }
        public string Created_On { get; set; }
        public string Created_By { get; set; }
        public string Last_Modified_On { get; set; }
        public string Last_Modified_By { get; set; }
        public string IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string Return_Comment { get; set; }
        public string Asset_Code_System { get; set; }
        public string Return_Request_ID { get; set; }
        public string Return_Request_Date { get; set; }
        public string Return_Requestor_Code { get; set; }
        public string Return_Requestor_Name { get; set; }
        public string Return_Request_No { get; set; }
        public string Return_Requestor_Department { get; set; }
        public string Asset_Serial_No { get; set; }
        public string Returned_Request_Status { get; set; }
        public string Return_Acknowledge_Comment { get; set; }
        public string Requisition_No { get; set; }
        public string Asset_Due_Date { get; set; }
        public string IM_Allocated_DateTime { get; set; }
        public string Health_Checkup_Remarks { get; set; }
        public string HealthCheckUpOn { get; set; }
        
       
    }
}