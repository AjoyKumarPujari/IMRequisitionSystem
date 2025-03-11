using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Models.RoleMaster
{
    public class RoleMasterModel
    {
        public String Role_Id { get; set; }
        public String Role_Name { get; set; }
        public String Role_Description { get; set; }
        public String Assigned_To { get; set; }
        public String Assigned_By { get; set; }
        public String Assigned_DateTime { get; set; }
        public String Last_Modified_DateTime { get; set; }
        public String Last_Modified_By { get; set; }
        public bool Role_Status { get; set; }

    }
}