using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Models.Deshboard
{
    public class HODDeshboard
    {
        public int RequestedCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public int CancelledCount { get; set; }
        public int AllocatedCount { get; set; }


        
    }
}