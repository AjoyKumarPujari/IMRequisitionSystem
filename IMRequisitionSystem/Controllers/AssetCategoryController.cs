using IMRequisitionSystem.Repository;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static IMRequisitionSystem.Util.Enums;
using System.Web.Services.Description;
using IMRequisitionSystem.Models;
using IMRequisitionSystem.Models.Assets;
using Dapper;
using Holiday_Home.Util;
using System.Data;

namespace IMRequisitionSystem.Controllers
{
    public class AssetCategoryController : Controller
    {
        // GET: AssetCategory
        private readonly IAssetCategoryRepository _assetCategoryRepository;

        public AssetCategoryController(IAssetCategoryRepository assetCategoryRepository)
        {
            _assetCategoryRepository = assetCategoryRepository;
        }
        public ActionResult InsertAssetcategory(string status, string message , bool isSwal = false)
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

                ViewBag.AssetCategoryDD = _assetCategoryRepository.GetAllAssetCategoryMaster();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }
        [HttpPost]
        public ActionResult InsertAssetcategory(AssetCategoryModel assetCategoryModel)
        {
            try
            {
                SPOutputMessage response = _assetCategoryRepository.InsertAssetCategoryMaster(assetCategoryModel);

                if (response.Status == 1)
                {
                    return RedirectToAction("InsertAssetcategory", "AssetCategory", new { status = ToastMessageType.Success, message = response.Message, isSwal = true });
                }
                else
                {
                    return RedirectToAction("InsertAssetcategory", "AssetCategory", new { status = ToastMessageType.Error, message = response.Message, isSwal = true });
                }
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return RedirectToAction("InsertAssetcategory", "AssetCategory", new { status = ToastMessageType.Error, message = "Someting went wrong. Please try again", isSwal = true });
            }
        }


        [HttpPost]
        public ActionResult ActivetoDeactiveStatus(int ID)
        {
            try
            {
                SPOutputMessage response = _assetCategoryRepository.UpdateActiveStatus(ID);

                if (response.Status == 1)
                {
                    return RedirectToAction("InsertAssetcategory", "AssetCategory", new { status = ToastMessageType.Success, message = response.Message, isSwal = true });
                }
                else
                {
                    return RedirectToAction("InsertAssetcategory", "AssetCategory", new { status = ToastMessageType.Error, message = response.Message,  isSwal = true });
                }
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return RedirectToAction("InsertAssetcategory", "AssetCategory", new { status = ToastMessageType.Error, message = "Someting went wrong. Please try again", isSwal = true });
            }
        }





    }
}