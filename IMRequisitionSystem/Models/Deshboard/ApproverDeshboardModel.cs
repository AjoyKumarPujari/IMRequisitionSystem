using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Models.Deshboard
{
    public class ApproverDeshboardModel
    {
        public int RequisitionCount { get; set; }
        public int AllocatedAssetCount { get; set; }
        public int RequisitionReturnCount { get; set; }
        public int ApprovalPendingRequisitionCount { get; set; }
        public int ApprovedRequisitionCount { get; set; }
        public int AssetMasterCount { get; set; }
        public int RoleMasterCount { get; set; }
        public int RoleAssignDetailsCount { get; set; }
    }
}