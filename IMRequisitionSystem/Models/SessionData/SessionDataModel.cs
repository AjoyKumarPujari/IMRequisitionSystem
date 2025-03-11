using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Models.SessionData
{
    public class SessionDataModel
    {
        public string SUPER_ADMIN { get; set; }
        public string LOCATIONAL_ADMIN { get; set; }
        public string EMPLOYEE { get; set; }
        public string HOD_UNIT_INCHARGE { get; set; }
        public string IM_APPROVER { get; set; }
        public string IM_ISSUER { get; set; }
        public string ALLOCATOR { get; set; }
        public string ADMIN { get; set; }
    }
}