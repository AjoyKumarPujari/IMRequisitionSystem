using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMRequisitionSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

       

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult LoginPage()
        {
            

            return View();
        }
        public ActionResult AddCategoryMaster()
        {


            return View();
        }

        public ActionResult AddAssetMaster()
        {


            return View();
        }
        public ActionResult AssetMasterTable()
        {


            return View();
        }

        public ActionResult AddRoleMaster()
        {


            return View();
        }
        public ActionResult AddUserRoleMapping()
        {


            return View();
        }
        public ActionResult RequisitionRequest()
        {


            return View();
        }
        public ActionResult RequisitionList()
        {


            return View();
        }
        public ActionResult RequisitionApproveList()
        {


            return View();
        }
        public ActionResult RequisitionApprovedList()
        {


            return View();
        }
        public ActionResult RequisitionReturnList()
        {


            return View();
        }

        public ActionResult DetailsPageForHODUnitInCharge()
        {
            return View();
        }

        public ActionResult DetailsPageForRequisitioner()
        {
            return View();
        }
           
        public ActionResult AllocatedAssetList()
        {
            return View();
        }

        public ActionResult RequisitionRequestListIMApprover()
        {
            return View();
        }

        public ActionResult DetailsPageForIMApprover()
        {
            return View();
        }


        public ActionResult RequisitionIssueList()
        {
            return View();
        }

        public ActionResult ReturnRequestDetailsEmployee()
        {
            return View();
        }
    }
}