using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Models.HOD
{
    public class HODModel
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }

        public string Admin { get; set; }
        public string SuperAdmin { get; set; }
        public string MailSent { get; set; }
        public string ReadAccess { get; set; }
        public string WriteAccess { get; set; }
        public string Approver { get; set; }


        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public string UserType { get; set; }
        public string EmployeeType { get; set; }
    }
}