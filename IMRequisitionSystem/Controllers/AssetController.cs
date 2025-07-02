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
    [CustomAdminAuthorize]
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

                model.asset_custodian_location = SessionData.GetSessionData(SessionData.Emp_loc_code);

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
                assetsModel.asset_custodian_location = SessionData.GetSessionData(SessionData.Emp_loc_code);
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
                List<SelectListItem> AssetStatus = new List<SelectListItem>()
                {    new SelectListItem { Value = "Good", Text = "Good" },
                     new SelectListItem { Value = "Defective", Text = "Defective" },
                     new SelectListItem { Value = "BER", Text = "BER" },
                     new SelectListItem { Value = "Condemn with DoT", Text = "Condemn with DoT" },
                };
                ViewBag.AssetStatusDD = AssetStatus;

                ViewBag.AssetMasterDataDD = _assetMasterRepository.GetAllAssetMaster();
                ViewBag.AssetModelDataDD = _assetMasterRepository.GetAllModelNo();
                ViewBag.LicenseDataDD = _assetMasterRepository.GetAllLicenseNo();
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
        public JsonResult ChangePhysicalCondition(AssetsModel assetsModel)
        {
            var spResponse = _assetMasterRepository.UpdateActivePhysicalCondition(assetsModel);

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
                return Json(new { redirectUrl = Url.Action("PeriodicHealthCheckupDueList", "Asset", new { status = ToastMessageType.Success, message = response.Message }) });
               
            }
            else
            {
                return Json(new { redirectUrl = Url.Action("PeriodicHealthCheckupDueList", "Asset", new { status = ToastMessageType.Error, message = response.Message }) });
                
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

                ViewBag.AssetMasterDataDD = _assetMasterRepository.GetAllAssetMaster();

                ViewBag.HealthCheckupDueAssetDataDD = _assetMasterRepository.GetHealthCheckupHistory();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        public ActionResult IssuedAssetList(string status, string message, bool isSwal = false)
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

                ViewBag.AssetMasterDataDD = _assetMasterRepository.GetAllIssuedAssetMaster();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }
        public ActionResult AMCMappingPage(string status, string message, bool isSwal = false)
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
                ViewBag.AssetMasterDataDD = _assetMasterRepository.GetAllAssetMaster();


            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View(model);
        }

        
        [HttpPost]
        public ActionResult AMCMappingPage(AssetsModel assetsModel)
        {
            try
            {
                SPOutputMessage response = _assetMasterRepository.InsertAssetAMCMasterMapping(assetsModel);

                if (response.Status == 1)
                {
                    return RedirectToAction("AMCMappingPage", "Asset", new { status = ToastMessageType.Success, message = response.Message, isSwal = true });
                }
                else
                {
                    return RedirectToAction("AMCMappingPage", "Asset", new { status = ToastMessageType.Error, message = response.Message, isSwal = true });
                }
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return RedirectToAction("AMCMappingPage", "Asset", new { status = ToastMessageType.Error, message = "Something went wrong. Please try again", isSwal = true });
            }
        }

        //public JsonResult GetDetailsByWorkOrderNoOfProductionServer(string Id)
        //{

        //    var spResponse = _assetMasterRepository.GetDetailsByWorkOrderNoFromProduction(Id);

        //    var jsonResponseHandler = new JsonResponseHandler(Url);
        //    return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "AssetMasterTable", "Asset", Id);
        //}

        public JsonResult GetDetailsByWorkOrderNoOfProductionServer(string id)
        {
            AssetModelForAPI spResponse = _assetMasterRepository.GetDetailsByWorkOrderNoFromProduction(id);
            return Json(spResponse, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAssetsDataBySAPID(string id)
        {
            AssetsModel spResponse = _assetMasterRepository.GetAssetsDataBySAPID(id);
            return Json(spResponse, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AMCMappingRPOPage(string status, string message, bool isSwal = false)
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
                ViewBag.AssetMasterDataDD = _assetMasterRepository.GetAllAssetMaster();


            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AMCMappingRPOPage(AssetsModel assetsModel)
        {
            try
            {
                assetsModel.RPO = true;
                SPOutputMessage response = _assetMasterRepository.InsertAssetAMCMasterMapping(assetsModel);

                if (response.Status == 1)
                {
                    return RedirectToAction("AMCMappingPage", "Asset", new { status = ToastMessageType.Success, message = response.Message, isSwal = true });
                }
                else
                {
                    return RedirectToAction("AMCMappingPage", "Asset", new { status = ToastMessageType.Error, message = response.Message, isSwal = true });
                }
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return RedirectToAction("AMCMappingPage", "Asset", new { status = ToastMessageType.Error, message = "Something went wrong. Please try again", isSwal = true });
            }
        }
        public ActionResult AMCMappingList(string status, string message, bool isSwal = false)
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
                ViewBag.AssetMasterMappingDataDD = _assetMasterRepository.GetAllAMCMappingAssetMaster();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        public ActionResult AMCMappingPageEdit(string status, string message,  string asset_Code_System, bool isSwal = false)
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
                asset_Code_System= System.Web.HttpContext.Current.Session["asset_Code_System"] as string;

                model = _assetMasterRepository.GetMappingAssetDataByID(asset_Code_System);


            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AMCMappingPageEditUpdate(AssetsModel assetsModel)
        {
            try
            {
                assetsModel.RPO = true;
                SPOutputMessage response = _assetMasterRepository.AMCMappingPageEditUpdate(assetsModel);

                if (response.Status == 1)
                {
                    return RedirectToAction("AMCMappingList", "Asset", new { status = ToastMessageType.Success, message = response.Message, isSwal = true });
                }
                else
                {
                    return RedirectToAction("AMCMappingList", "Asset", new { status = ToastMessageType.Error, message = response.Message, isSwal = true });
                }
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return RedirectToAction("AMCMappingList", "Asset", new { status = ToastMessageType.Error, message = "Something went wrong. Please try again", isSwal = true });
            }
        }

        public ActionResult AMCMappingHistory(string status, string message, bool isSwal = false)
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
                ViewBag.AssetMasterMappingHistoryDataDD = _assetMasterRepository.GetAllAMCMappingHistoryAssetMaster();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }
        [HttpPost]
        public ActionResult GetAMCMappingListWithFilter(AssetsModel assetModel)
        {
            try
            {
                //List<BBSObservationCreateModel> filterData = _bbsMethod.PlantWiseObservationCounts(bbsObservationCreateModel);
                List<AssetsModel> assetsList = _assetMasterRepository.GetAMCMappingListWithFilterData(assetModel);
                var jsonResult = Json(assetsList, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {

                return Json(new List<AssetsModel>(), JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult GetMasterListWithFilter(AssetsModel assetModel)
        {
            try
            {
                //List<BBSObservationCreateModel> filterData = _bbsMethod.PlantWiseObservationCounts(bbsObservationCreateModel);
                List<AssetsModel> assetsList = _assetMasterRepository.GetMasterListWithFilterData(assetModel);
                var jsonResult = Json(assetsList, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {

                return Json(new List<AssetsModel>(), JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult AMCMappingHistoryListWithFilter(AssetsModel assetModel)
        {
            try
            {
                //List<BBSObservationCreateModel> filterData = _bbsMethod.PlantWiseObservationCounts(bbsObservationCreateModel);
                List<AssetsModel> assetsList = _assetMasterRepository.GetAMCMappingHistoryListWithFilterData(assetModel);
                var jsonResult = Json(assetsList, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
               
                return Json(new List<AssetsModel>(), JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult GetHealthCheckupListWithFilter(AssetsModel assetModel)
        {
            try
            {
                //List<BBSObservationCreateModel> filterData = _bbsMethod.PlantWiseObservationCounts(bbsObservationCreateModel);
                List<AssetsModel> assetsList = _assetMasterRepository.GetHealthCheckupListWithFilterData(assetModel);
                var jsonResult = Json(assetsList, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {

                return Json(new List<AssetsModel>(), JsonRequestBehavior.AllowGet);
            }

        }
        
    }
}