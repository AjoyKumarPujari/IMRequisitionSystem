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
                ViewBag.AssetCategoryDD = _assetCategoryRepository.GetAllAssetCategoryMaster();
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
                    return RedirectToAction("AssetListView", "AssetCategory", new { status = ToastMessageType.Success, message = response.Message, isSwal = true });
                }
                else
                {
                    return RedirectToAction("AddAssetMaster", "AssetCategory", new { status = ToastMessageType.Error, message = response.Message, isSwal = true });
                }
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return RedirectToAction("AddAssetMaster", "AssetCategory", new { status = ToastMessageType.Error, message = "Someting went wrong. Please try again", isSwal = true });
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
    }
}