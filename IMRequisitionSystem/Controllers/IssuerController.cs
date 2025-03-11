using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Repository;
using IMRequisitionSystem.Repository.Approver;
using IMRequisitionSystem.Repository.Issuer;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static IMRequisitionSystem.Util.Enums;

namespace IMRequisitionSystem.Controllers
{
    public class IssuerController : Controller
    {
        private readonly IIMIssuerRepository _imIssuerRepository;
        private readonly IAssetCategoryRepository _assetCategoryRepository;
        // GET: Approver
        public IssuerController(
            IIMIssuerRepository imIssuerRepository,
             IAssetCategoryRepository assetCategoryRepository
            )
        {

            _imIssuerRepository = imIssuerRepository;
            _assetCategoryRepository = assetCategoryRepository;

        }
        public ActionResult RequisitionIssueList(string status, string message, bool isSwal = false)
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
                ViewBag.RequisitionDetailsForIssuerDataDD = _imIssuerRepository.GetRequisitionIssuerList();
                ViewBag.AssetCategoryDD = _assetCategoryRepository.GetAllAssetCategoryForDropDown();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }


        [HttpGet]
        
        public JsonResult CheckAvailability(string deviceTypeId)
        {
            try
            {
                var result = _imIssuerRepository.CheckAvailabilityOfDevice(deviceTypeId);
                return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpGet]

        //public JsonResult GetDeviceTypeID(string deviceTypeId)
        //{
        //    try
        //    {
        //        var result = _imIssuerRepository.GetDeviceTypeID(deviceTypeId);
        //        return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpGet]
        public JsonResult GetDeviceTypeID(string deviceTypeId)
        {
            try
            {
                List<RequisitionRequestModel> filterData = _imIssuerRepository.GetDeviceTypeID(deviceTypeId);
                return Json(filterData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new List<RequisitionRequestModel>(), JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult RequisitionIssueByIMIssuer(RequisitionRequestModel requisitionRequestModel)
        {
            var spResponse = _imIssuerRepository.IMIssuerUpdate(requisitionRequestModel);

            Session["Requisition_No"] = requisitionRequestModel.Requisition_No;

            var jsonResponseHandler = new JsonResponseHandler(Url);
            return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "RequisitionIssueList", "Issuer", requisitionRequestModel.Requisition_No);
        }

        public ActionResult IssuedRequisitionList(string status, string message, bool isSwal = false)
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
                ViewBag.RequisitionIMIssuededData = _imIssuerRepository.GetRequisitionIMIssuedList();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        public ActionResult RequisitionAllocateList(string status, string message, bool isSwal = false)
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
                ViewBag.RequisitionIMIssuededData = _imIssuerRepository.GetRequisitionIMIssuedList();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        public JsonResult RequisitionAllocate(RequisitionRequestModel requisitionRequestModel)
        {
            var spResponse = _imIssuerRepository.IMAllocatorUpdate(requisitionRequestModel);

            Session["Requisition_No"] = requisitionRequestModel.Requisition_No;

            var jsonResponseHandler = new JsonResponseHandler(Url);
            return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "RequisitionAllocateList", "Issuer", requisitionRequestModel.Requisition_No);
        }
        public ActionResult RequisitionAllocatedList(string status, string message, bool isSwal = false)
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
                ViewBag.RequisitionIMIssuededData = _imIssuerRepository.GetRequisitionAllocatedList();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

       

        public ActionResult DetailPageForIssuer(RequisitionRequestModel requisitionRequestModel, string status, string message, string requisition_No)
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
                TempData[ToastMessageParameter.IsSwal.ToString()] = true;
                //ViewBag.CancelRequest = cancelRequest;
                requisition_No = System.Web.HttpContext.Current.Session["requisition_No"] as string;
                //ViewBag.RequisitionDetailsForRequestorDataDD = _requisitionApproveList.GetDetailsPageForUnitIncharge(requisition_No);
                requisitionRequestModel = _imIssuerRepository.GetDetailsDataForIssuer(requisition_No);
                return View(requisitionRequestModel);
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        public JsonResult RequisitionSortCloseByIssuer(RequisitionRequestModel requisitionRequestModel)
        {
            var spResponse = _imIssuerRepository.IssuerCloseUpdate(requisitionRequestModel);

            Session["Requisition_No"] = requisitionRequestModel.Requisition_No;

            var jsonResponseHandler = new JsonResponseHandler(Url);
            return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "RequisitionIssueList", "Issuer", requisitionRequestModel.Requisition_No);
        }

        public ActionResult DetailPageForAlocator(RequisitionRequestModel requisitionRequestModel, string status, string message, string requisition_No)
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
                TempData[ToastMessageParameter.IsSwal.ToString()] = true;
                //ViewBag.CancelRequest = cancelRequest;
                requisition_No = System.Web.HttpContext.Current.Session["requisition_No"] as string;
                //ViewBag.RequisitionDetailsForRequestorDataDD = _requisitionApproveList.GetDetailsPageForUnitIncharge(requisition_No);
                requisitionRequestModel = _imIssuerRepository.GetDetailsDataForIssuer(requisition_No);
                return View(requisitionRequestModel);
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

    }
}