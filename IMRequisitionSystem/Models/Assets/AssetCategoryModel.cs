using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Models.Assets
{
    public class AssetCategoryModel
    {
        public string ID { get; set; }
        public string DeviceType { get; set; }
        public string DeviceDescription { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }


        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}