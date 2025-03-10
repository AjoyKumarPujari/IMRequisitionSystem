using Holiday_Home.Util;
using IMRequisitionSystem.Models;
using IMRequisitionSystem.Repository;
using IMRequisitionSystem.Repository.Login;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using static IMRequisitionSystem.Util.Enums;

namespace IMRequisitionSystem.Controllers
{

    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }



        public ActionResult UserLogin(string status, string message)
        
        {
            try
            {

                if (!string.IsNullOrEmpty(status))
                {
                    TempData[ToastMessageParameter.MessageType.ToString()] = status;
                }
                if (!string.IsNullOrEmpty(message))
                {
                    TempData[ToastMessageParameter.Message.ToString()] = message;
                }
                TempData[ToastMessageParameter.IsSwal.ToString()] = true;

                string UserCode = "";
                UserCode = Session["UserCode"] as string;

                if (UserCode != null)
                {
                    return RedirectToAction("EmployeeDashboard", "Employee");
                }
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }
        [HttpPost]
        public ActionResult UserLoginPost(string username, string password)
        {
            try
            {
                string strPassword = "";

                if (!string.IsNullOrEmpty(password))
                {
                    strPassword = UtilityClass.Encrypt(Convert.ToString(password));
                }

                var result = _loginRepository.CheckUserLogin(username, strPassword);
                
                if (!result.IsAuthenticated)
                {
                    if (result.Message == "Credentials are incorrect")
                    {
                        return RedirectToAction("UserLogin", "Login", new { status = ToastMessageType.Error.ToString(), message = "Credentials are incorrect" });
                    }
                    if (result.Message == "Active Directory Login Expired")
                    {
                        return RedirectToAction("UserLogin", "Login", new { status = ToastMessageType.Error.ToString(), message = "Active Directory Login Expired" });
                    }
                    if (result.Message == "Not an Active Account")
                    {
                        return RedirectToAction("UserLogin", "Login", new { status = ToastMessageType.Error.ToString(), message = "Not an Active Account" });
                    }
                }

                Session["UserName"] = Convert.ToString(result.UserName);
                Session["UserCode"] = Convert.ToString(result.UserCode);
                Session["EmailID"] = Convert.ToString(result.Email);
                Session["Name"] = Convert.ToString(result.Name);
                Session["DepartmentName"] = Convert.ToString(result.DepartmentName);
                Session["Designation"] = Convert.ToString(result.Designation);
                Session["Name"] = Convert.ToString(result.Name);
                Session["Email"] = Convert.ToString(result.Email);
                Session["IsAuthenticated"] = result.IsAuthenticated;
                Session["Admin"] = Convert.ToString(result.Admin);
                Session["SuperAdmin"] = Convert.ToString(result.SuperAdmin);
                Session["ReadAccess"] = Convert.ToString(result.ReadAccess);
                Session["WriteAccess"] = Convert.ToString(result.WriteAccess);
                Session["IM_APPROVER"] = Convert.ToString(result.IM_APPROVER);
                Session["ADMIN"] = Convert.ToString(result.ADMIN);
                Session["SUPER_ADMIN"] = Convert.ToString(result.SUPER_ADMIN);
                Session["EMPLOYEE"] = Convert.ToString(result.EMPLOYEE);
                Session["HOD_UNIT_INCHARGE"] = Convert.ToString(result.HOD_UNIT_INCHARGE);
                Session["IM_ISSUER"] = Convert.ToString(result.IM_ISSUER);
                Session["ALLOCATOR"] = Convert.ToString(result.ALLOCATOR);
                Session["LOCATIONAL_ADMIN"] = Convert.ToString(result.LOCATIONAL_ADMIN);
                

                Session["IsHotelExecutive"] = Convert.ToString("N");

                if (result.IsAuthenticated)
                {
                    if (result.Admin == "Y")
                    {
                        return RedirectToAction("AdminDeshboard", "Home");
                    }
                    else
                    {
                        return RedirectToAction("AdminDeshboard", "Home");
                    }
                }
                if (username == null && password == null)
                {
                    return RedirectToAction("UserLogin", "Login", new { status = ToastMessageType.Error.ToString(), message = "Enter the Credentials" });
                }
                else
                {
                    return RedirectToAction("UserLogin", "Login", new { status = ToastMessageType.Error.ToString(), message = result.Message });
                }
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View("UserLogin");
        }

        public ActionResult UserLogout()
        {
            Session.Clear();
            return RedirectToAction("UserLogin");
        }




    }


}