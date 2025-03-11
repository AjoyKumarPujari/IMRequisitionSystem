using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Models.RoleMapping
{
    public class RoleMappingModel
    {
        public int Role_Id { get; set; }
        public string Mapping_Id { get; set; }
        public string Role_Name { get; set; }
        public string Assigned_To_Name { get; set; }
        public string Assigned_By_Name { get; set; }
        public string Assigned_To { get; set; }
        public string Assigned_By { get; set; }
        public string Assigned_DateTime { get; set; }
        public string Last_Modified_DateTime { get; set; }
        public string Last_Modified_By { get; set; }
        public bool Status { get; set; }
      
    }
}