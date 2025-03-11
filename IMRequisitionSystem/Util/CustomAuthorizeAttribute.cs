using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMRequisitionSystem.Util
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            if (session == null || session["IsAuthenticated"] == null || !(bool)session["IsAuthenticated"])
            {
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}