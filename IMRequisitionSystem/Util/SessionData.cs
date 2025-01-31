using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Helpers;

namespace Holiday_Home.Util
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

        public static string GetSessionHotelExecutiveID()
        {
            string HotelExecutiveID = "";
            try
            {
                HotelExecutiveID = HttpContext.Current.Session["HotelExecutiveID"] as string;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return HotelExecutiveID;
        }



        public static string IsAdmin()
        {
            string UserCode = "";
            try
            {
                UserCode = HttpContext.Current.Session["Admin"] as string;
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
                UserCode = HttpContext.Current.Session["SuperAdmin"] as string;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return UserCode;
        }
        public static string IsHotelExecutive()
        {
            string UserCode = "";
            try
            {
                UserCode = HttpContext.Current.Session["IsHotelExecutive"] as string;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return UserCode;
        }
        public static string IsApprover()
        {
            string UserCode = "";
            try
            {
                UserCode = HttpContext.Current.Session["Approver"] as string;
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