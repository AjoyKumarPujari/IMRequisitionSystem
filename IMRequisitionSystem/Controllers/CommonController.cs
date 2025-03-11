using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMRequisitionSystem.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        [HttpPost]
        public JsonResult SetSession(string key, string requisition_No)
        {
            Session["" + key] = requisition_No;
            return Json(new { success = true });
        }
    }
}