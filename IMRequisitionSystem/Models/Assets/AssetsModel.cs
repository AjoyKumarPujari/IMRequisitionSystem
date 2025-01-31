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
        public string asset_Physical_Condition { get; set; }
        public string Issue_status { get; set; }
        public string asset_Status { get; set; }
        public string Remarks { get; set; }
        public string Created_On { get; set; }
        public string Created_By { get; set; }
        public string Last_Modified_On { get; set; }
        public string Last_Modified_By { get; set; }
        public string IsDeleted { get; set; }
        public string IsActive { get; set; }

    }
}