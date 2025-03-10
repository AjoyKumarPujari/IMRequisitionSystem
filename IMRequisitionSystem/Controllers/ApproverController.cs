using IMRequisitionSystem.Models;
using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Repository;
using IMRequisitionSystem.Repository.Approver;
using IMRequisitionSystem.Repository.Common;
using IMRequisitionSystem.Repository.Requisition;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static IMRequisitionSystem.Util.Enums;

namespace IMRequisitionSystem.Controllers 
{
    
    public class ApproverController : Controller
    {
        private readonly IRequisitionApproveList _requisitionApproveList;
        private readonly IIMRequisitionApproverList _iMRequisitionApproverList;
        // GET: Approver
        public ApproverController(
            IRequisitionApproveList requisitionApproveList,
            IIMRequisitionApproverList iMRequisitionApproverList
            )
        {
            _requisitionApproveList = requisitionApproveList;
            _iMRequisitionApproverList = iMRequisitionApproverList;

        }

        public ActionResult RequisitionApproveList(string status, string message, bool isSwal = false)
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
                ViewBag.RequisitionDetailsForRequestorDataDD = _requisitionApproveList.GetRequisitionApproveList();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        public ActionResult DetailsPageForHODUnitInCharge(RequisitionRequestModel requisitionRequestModel, string status, string message, string requisition_No)
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
                requisitionRequestModel = _requisitionApproveList.GetDetailsPageForUnitIncharge(requisition_No);
                return View(requisitionRequestModel);
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        [HttpPost]
        public ActionResult DetailsPageForHODUnitInCharge(RequisitionRequestModel requisitionRequestModel)
        {
            try
            {
                SPOutputMessage response = _requisitionApproveList.HODApproveUpdate(requisitionRequestModel);

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

        public JsonResult RequisitionApproveByHOD(RequisitionRequestModel requisitionRequestModel)
        {
            var spResponse = _requisitionApproveList.HODApproveUpdate(requisitionRequestModel);

            Session["Requisition_No"] = requisitionRequestModel.Requisition_No;

            var jsonResponseHandler = new JsonResponseHandler(Url);
            return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "DetailsPageForHODUnitInCharge", "Approver", requisitionRequestModel.Requisition_No);
        }

        public ActionResult RequisitionApprovedList(string status, string message, bool isSwal = false)
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
                ViewBag.RequisitionApprovedData = _requisitionApproveList.GetRequisitionApprovedList();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        public ActionResult RequisitionRequestListIMApprover(string status, string message, bool isSwal = false)
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
                ViewBag.RequisitionIMApproveDataDD = _iMRequisitionApproverList.GetRequisitionIMApproverList();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        public ActionResult DetailsPageForIMApprover(RequisitionRequestModel requisitionRequestModel, string status, string message, string requisition_No)
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
                requisitionRequestModel = _iMRequisitionApproverList.GetDetailsDataForIMApprover(requisition_No);
                return View(requisitionRequestModel);
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        public JsonResult RequisitionApproveByIMApprover(RequisitionRequestModel requisitionRequestModel)
        {
            var spResponse = _iMRequisitionApproverList.IMApproverUpdate(requisitionRequestModel);

            Session["Requisition_No"] = requisitionRequestModel.Requisition_No;

            var jsonResponseHandler = new JsonResponseHandler(Url);
            return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "DetailsPageForIMApprover", "Approver", requisitionRequestModel.Requisition_No);
        }

        public ActionResult RequisitionIMApprovedList(string status, string message, bool isSwal = false)
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
                ViewBag.RequisitionIMApprovedData = _iMRequisitionApproverList.GetRequisitionIMApprovedList();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        public ActionResult RequisitionDetailsPage(RequisitionRequestModel requisitionRequestModel, string status, string message, string requisition_No)
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
                requisitionRequestModel = _iMRequisitionApproverList.GetDetailsDataForIMApprover(requisition_No);
                return View(requisitionRequestModel);
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }
        
        public JsonResult RequisitionRejectByHOD(RequisitionRequestModel requisitionRequestModel)
        {
            var spResponse = _requisitionApproveList.HODRejectUpdate(requisitionRequestModel);

            Session["Requisition_No"] = requisitionRequestModel.Requisition_No;

            var jsonResponseHandler = new JsonResponseHandler(Url);
            return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "RequisitionApproveList", "Approver", requisitionRequestModel.Requisition_No);
        }

    }
}