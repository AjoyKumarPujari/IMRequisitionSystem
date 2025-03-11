using IMRequisitionSystem.Repository;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static IMRequisitionSystem.Util.Enums;
using System.Web.Services.Description;
using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Models;
using IMRequisitionSystem.Repository.Common;
using IMRequisitionSystem.Models.RoleMapping;

namespace IMRequisitionSystem.Controllers
{
    public class AssetController : Controller
    {
        // GET: Asset
        private readonly IAssetCategoryRepository _assetCategoryRepository;
        private readonly IAssetMasterRepository _assetMasterRepository;
        private readonly IDepartmentMasterRepository _departmentMasterRepository;
        private readonly IAreaMasterRepository _areaMasterRepository;

        public AssetController(
            IAssetCategoryRepository assetCategoryRepository,
            IAssetMasterRepository assetMasterRepository,
            IDepartmentMasterRepository departmentMasterRepository,
            AreaMasterRepository areaMasterRepository
            )
        {
            _assetCategoryRepository = assetCategoryRepository;
            _assetMasterRepository = assetMasterRepository;
            _departmentMasterRepository = departmentMasterRepository;
            _areaMasterRepository = areaMasterRepository;
        }

        public ActionResult AddAssetMaster(string status, string message, bool isSwal = false)
        {
            var model = new AssetsModel();
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
                ViewBag.AssetCategoryDD = _assetCategoryRepository.GetAllAssetCategoryForDropDown();
                ViewBag.DepartmentDD = _departmentMasterRepository.GetAllDepartmentMaster();
                ViewBag.AreaDD = _areaMasterRepository.GetAllAreaMaster();


            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddAssetMaster(AssetsModel assetsModel)
        {
            try
            {
                SPOutputMessage response = _assetMasterRepository.InsertAssetMaster(assetsModel);

                if (response.Status == 1)
                {
                    return RedirectToAction("AssetMasterTable", "Asset", new { status = ToastMessageType.Success, message = response.Message, isSwal = true });
                }
                else
                {
                    return RedirectToAction("AssetMasterTable", "Asset", new { status = ToastMessageType.Error, message = response.Message, isSwal = true });
                }
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return RedirectToAction("AddAssetMaster", "Asset", new { status = ToastMessageType.Error, message = "Something went wrong. Please try again", isSwal = true });
            }
        }
        public ActionResult EditAssetMaster()
        {
            return View();
        }

        public ActionResult AssetMasterTable(string status, string message, bool isSwal = false)
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
                TempData[ToastMessageParameter.IsSwal.ToString()] = isSwal;

                ViewBag.AssetMasterDataDD = _assetMasterRepository.GetAllAssetMaster();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }
        public JsonResult ActiveDeactiveAssets(AssetsModel assetsModel)
        {
            var spResponse = _assetMasterRepository.UpdateActiveDeActiveAssetsStatus(assetsModel);

            //Session["Requisition_No"] = requisitionRequestModel.Requisition_No;

            var jsonResponseHandler = new JsonResponseHandler(Url);
            return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "AssetMasterTable", "Asset", assetsModel.Asset_Code_System);
        }

        public ActionResult PeriodicHealthCheckupDueList(string status, string message, bool isSwal = false)
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
                TempData[ToastMessageParameter.IsSwal.ToString()] = isSwal;
                
                List<SelectListItem> assetConditions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "GOOD", Text = "GOOD" },
                    new SelectListItem { Value = "DAMAGE", Text = "DAMAGE" }
                };

                ViewBag.AssetConditions = assetConditions;
                ViewBag.HealthCheckupDueAssetDataDD = _assetMasterRepository.GetHealthCheckupDueAsset();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        [HttpPost]
        public ActionResult HealthCheckUpUpdate(AssetsModel assetsModel)
        {

            SPOutputMessage response = _assetMasterRepository.UpdateActiveDeActiveAsCreateHealthCheckUpUpdatesetsStatus(assetsModel);

            if (response.Status == 1)
            {
                return RedirectToAction("PeriodicHealthCheckupDueList", "Asset", new { status = ToastMessageType.Success, message = response.Message });
            }
            else
            {
                return RedirectToAction("PeriodicHealthCheckupDueList", "Asset", new { status = ToastMessageType.Error, message = "Unable to Create Requisition Request", swalMessage = response.Message, isSwal = true });
            }
        }

        public ActionResult PeriodicHealthCheckupHistory(string status, string message, bool isSwal = false)
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
                TempData[ToastMessageParameter.IsSwal.ToString()] = isSwal;

                
                ViewBag.HealthCheckupDueAssetDataDD = _assetMasterRepository.GetHealthCheckupHistory();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }
    }
}