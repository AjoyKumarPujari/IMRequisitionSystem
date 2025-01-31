using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Models.Login
{
    public class LoginModel
    {

        public string UserCode { get; set; }
        public string UserName { get; set; }

        public string Name { get; set; }
        public string Designation { get; set; }

        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        
        public string Email { get; set; }
        public string Admin { get; set; }
        public string SuperAdmin { get; set; }
        public string ReadAccess { get; set; }
        public string WriteAccess { get; set; }
        public string Approver { get; set; }




        public string UserID { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsActive { get; set; } = false;

        public string Message { get; set; }
        public bool IsAuthenticated { get; set; } = false;

    



    }
}