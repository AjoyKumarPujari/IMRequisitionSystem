using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Models.Deshboard
{
    public class AllocatorDeshboardModel
    {
        public int RequestedCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public int CancelledCount { get; set; }
        public int AllocatedCount { get; set; }
        public int AssetMasterCount { get; set; }
        public int RoleMasterCount { get; set; }
        public int RoleAssignDetailsCount { get; set; }

    }
}