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

        #region IMIssuer
        public ActionResult RequisitionIssueList()
        {
            return View();
        }
        public ActionResult DetailPageForIssuer()
        {
            return View();
        }
        public ActionResult AllRequisitionIssueList()
        {
            return View();
        }


        #endregion
        #region Return
        public ActionResult ReturnRequestDetailsEmployee()
        {
            return View();
        }
        #endregion

        #region IMAllocator
        public ActionResult RequisitionAllocateList()
        {
            return View();
        }
       public ActionResult DetailPageForAlocator()
        {
            return View();
        }

        #endregion

        public ActionResult AssignLocationalAdmin()
        {
            return View();
        }

        public ActionResult RequisitionDetailsPage()
        {
            return View();
        }
        public ActionResult ReturnedRequisitionDetailsPage()
        {
            return View();
        }

        public ActionResult IssuedRequisitionList()
        {
            return View();
        }

        public ActionResult IssuedAssetList()
        {
            return View();
        }
        public ActionResult IssuedAssetReturnRequestListOnBehalf()
        {
            return View();
        }

        public ActionResult AssetReturnRequestList()
        {
            return View();
        }

        public ActionResult RequisitionDetailsPageIssued()
        {
            return View();
        }

        public ActionResult AllocatorAllocatedRequisition()
        {
            return View();
        }

        public ActionResult PeriodicHealthCheckupDueList()
        {
            return View();
        }

        public ActionResult PeriodicHealthCheckupHistory()
        {
            return View();
        }

        public ActionResult ReturnReceivePendingList()
        {
            return View();
        }

        public ActionResult ReturnReceiveReceivedList()
        {
            return View();
        }

        public ActionResult ReturnReceiveReceivedDetailsPage()
        {
            return View();
        }

        public ActionResult AMCMappingPage()
        {
            return View();
        }
    }
}