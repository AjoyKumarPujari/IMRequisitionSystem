using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static IMRequisitionSystem.Util.Enums;

namespace IMRequisitionSystem.Util
{
    public class CustomAdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            if (session == null || session["IsAuthenticated"] == null || !(bool)session["IsAuthenticated"])
            {
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
            }
            else if((string)session["Admin"] != "Y" )
            {
                filterContext.Result = new RedirectResult("~/Employee/EmployeeDashboard?status=" + ToastMessageType.Error + "&message=You do not have admin right.");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}