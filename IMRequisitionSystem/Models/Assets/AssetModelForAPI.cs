using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Models.Assets
{
    public class AssetModelForAPI
    {
        public string VendorCode { get; set; }
        public string PoNo { get; set; }
        public string PoDate { get; set; }
        public string JobDetails { get; set; }
        public string VendorName { get; set; }
    }
}