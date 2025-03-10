using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Helpers;

namespace IMRequisitionSystem.Util
{
    public static class SessionData
    {

        public static string GetSessionUserCode()
        {
            string UserCode = "";
            try
            {
                UserCode = HttpContext.Current.Session["UserCode"] as string;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return UserCode;
        }

     

        public static string IsAdmin()
        {
            string UserCode = "";
            try
            {
                UserCode = HttpContext.Current.Session["ADMIN"] as string;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return UserCode;
        }
        public static string IsSuperAdmin()
        {
            string UserCode = "";
            try
            {
                UserCode = HttpContext.Current.Session["SUPER_ADMIN"] as string;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return UserCode;
        }
        public static string IsAllocator()
        {
            string UserCode = "";
            try
            {
                UserCode = HttpContext.Current.Session["ALLOCATOR"] as string;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return UserCode;
        }
        public static string IsImApprover()
        {
            string UserCode = "";
            try
            {
                UserCode = HttpContext.Current.Session["IM_APPROVER"] as string;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return UserCode;
        }
         public static string IsImIssuer()
        {
            string UserCode = "";
            try
            {
                UserCode = HttpContext.Current.Session["IM_ISSUER"] as string;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return UserCode;
        }
        public static string IsHodUnitIncharge()
        {
            string UserCode = "";
            try
            {
                UserCode = HttpContext.Current.Session["HOD_UNIT_INCHARGE"] as string;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return UserCode;
        }
         public static string IsEmployee()
        {
            string UserCode = "";
            try
            {
                UserCode = HttpContext.Current.Session["EMPLOYEE"] as string;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return UserCode;
        }
        public static string IsLocationAdmin()
        {
            string UserCode = "";
            try
            {
                UserCode = HttpContext.Current.Session["LOCATIONAL_ADMIN"] as string;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return UserCode;
        }




        public static string GetSessionData(string key)
        {
            string UserCode = "";
            try
            {
                UserCode = HttpContext.Current.Session[key] as string;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return UserCode;
        }

        public static string GetInitialLetterOfName()
        {
            string UserName = "";
            try
            {
                UserName = HttpContext.Current.Session[Name] as string;
                var words = UserName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                UserName = UserName.ToUpper().Trim()[0].ToString();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return UserName;
        }


        public const string UserName = "UserName";
        public const string UserCode = "UserCode";
        public const string EmailID = "EmailID";
        public const string Name = "Name";
        public const string MobileNo = "MobileNo";
        public const string DepartmentName = "DepartmentName";
        public const string Designation = "Designation";
        public const string Grade = "Grade";
        public const string GradeCategory = "GradeCategory";
        public const string Email = "Email";
        public const string StaffLocation = "StaffLocation";
        public const string ReportingStaffNo = "ReportingStaffNo";
        public const string ReportingStaffName = "ReportingStaffName";
        public const string IsAuthenticated = "IsAuthenticated";
        public const string Admin = "Admin";
        public const string SuperAdmin = "SuperAdmin";
        public const string MailSent = "MailSent";
        public const string ReadAccess = "ReadAccess";
        public const string WriteAccess = "WriteAccess";


    }
}