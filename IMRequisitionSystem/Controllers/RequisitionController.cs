using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Repository.Common;
using IMRequisitionSystem.Repository;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static IMRequisitionSystem.Util.Enums;
using IMRequisitionSystem.Repository.Requisition;
using Holiday_Home.Util;
using IMRequisitionSystem.Models;

namespace IMRequisitionSystem.Controllers
{
    public class RequisitionController : Controller
    {
        private readonly IAssetCategoryRepository _assetCategoryRepository;
        private readonly IAssetMasterRepository _assetMasterRepository;
        private readonly IDepartmentMasterRepository _departmentMasterRepository;
        private readonly IAreaMasterRepository _areaMasterRepository;
        private readonly IRequisitionRequestRepository _requisitionRequestRepository;
        private readonly IEmployeeDetailsRepository _employeeDetailsRepository;


        public RequisitionController(

            IAssetCategoryRepository assetCategoryRepository,
            IAssetMasterRepository assetMasterRepository,
            IDepartmentMasterRepository departmentMasterRepository,
            IAreaMasterRepository areaMasterRepository,
            IRequisitionRequestRepository requisitionRequestRepository,
            IEmployeeDetailsRepository employeeDetailsRepository

            )
        {
            _assetCategoryRepository = assetCategoryRepository;
            _assetMasterRepository = assetMasterRepository;
            _departmentMasterRepository = departmentMasterRepository;
            _areaMasterRepository = areaMasterRepository;
            _requisitionRequestRepository = requisitionRequestRepository;
            _employeeDetailsRepository = employeeDetailsRepository;

        }
        // GET: Requisition
        public ActionResult RequisitionRequest(string status, string message, bool isSwal = false)
        {
            var model = new RequisitionRequestModel();
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
                TempData[ToastMessageParameter.IsSwal.ToString()] = isSwal;
                ViewBag.AssetCategoryDD = _assetCategoryRepository.GetAllAssetCategoryMaster();
                ViewBag.DepartmentDD = _departmentMasterRepository.GetAllDepartmentMaster();
                ViewBag.AreaDD = _areaMasterRepository.GetAllAreaMaster();
                ViewBag.AssetMasterDataDD = _assetMasterRepository.GetAllAssetMaster();
                ViewBag.NonMngmentUserMasterDataDD = _employeeDetailsRepository.GetAllNonmanagementUserData();
                ViewBag.ApproverDataDD = _employeeDetailsRepository.GetApproverDataViaGreade(SessionData.GetSessionUserCode());
                ViewBag.RequisitionDetailsForRequestorDataDD = _requisitionRequestRepository.GetRequisitionDetailsForRequestor();

            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult RequisitionRequest(RequisitionRequestModel requisitionRequestModel)
        {
            SPOutputMessage response = _requisitionRequestRepository.CreateRequisitionRequest(requisitionRequestModel);

            if (response.Status == 1)
            {
                return RedirectToAction("RequisitionRequest", "Requisition", new { status = ToastMessageType.Success, message = response.Message});
            }
            else
            {
                return RedirectToAction("RequisitionRequest", "Requisition", new { status = ToastMessageType.Error, message = "Unable to Create Requisition Request", swalMessage = response.Message, isSwal = true });
            }
        }

      
    }
}