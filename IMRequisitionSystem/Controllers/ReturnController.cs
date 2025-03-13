using IMRequisitionSystem.Repository.Issuer;
using IMRequisitionSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMRequisitionSystem.Repository.Return;
using IMRequisitionSystem.Util;
using static IMRequisitionSystem.Util.Enums;
using IMRequisitionSystem.Models.Assets;

namespace IMRequisitionSystem.Controllers
{
    public class ReturnController : Controller
    {
        private readonly IReturnRepository _iReturnRepository;
        private readonly IAssetMasterRepository _iAssetMasterRepository;
       
        // GET: Approver
        public ReturnController(
            IReturnRepository returnRepository,
            IAssetMasterRepository assetMasterRepository
            )
        {
            _iReturnRepository = returnRepository;
            _iAssetMasterRepository = assetMasterRepository;
            
        }
        // GET: Return
        public ActionResult AllocatedAssetList(string status, string message, bool isSwal = false)
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
                ViewBag.AllocatedAssetMasterDataDD = _iAssetMasterRepository.GetAllAllocateAssetMaster();
                
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }
        public JsonResult RequisitionReturnRequest(RequisitionRequestModel requisitionRequestModel)
        {
            var spResponse = _iReturnRepository.RequisitionReturn(requisitionRequestModel);

            Session["Requisition_No"] = requisitionRequestModel.Requisition_No;

            var jsonResponseHandler = new JsonResponseHandler(Url);
            return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "AllocatedAssetList", "Return", requisitionRequestModel.Requisition_No);
        }
        public JsonResult RequisitionReturnRequestFRomIssuer(RequisitionRequestModel requisitionRequestModel)
        {
            var spResponse = _iReturnRepository.RequisitionReturn(requisitionRequestModel);

            Session["Requisition_No"] = requisitionRequestModel.Requisition_No;

            var jsonResponseHandler = new JsonResponseHandler(Url);
            return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "IssuedAssetList", "Asset", requisitionRequestModel.Requisition_No);
        }
        public ActionResult ReturnReceivePendingList(string status, string message, bool isSwal = false)
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
                ViewBag.AssetMasterDataDD = _iReturnRepository.GetAllReturnRequestedAssetMaster();

            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }
        public JsonResult RequisitionReturnAcknowledge(AssetsModel assetsModel)
        {
            var spResponse = _iReturnRepository.RequisitionReturnAcknowledge(assetsModel);

            //Session["Requisition_No"] = requisitionRequestModel.Requisition_No;

            var jsonResponseHandler = new JsonResponseHandler(Url);
            return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "ReturnReceivePendingList", "Return", assetsModel.Return_Request_ID);
        }
        
        public ActionResult ReturnReceiveReceivedList(string status, string message, bool isSwal = false)
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
                ViewBag.AssetMasterDataDD = _iReturnRepository.GetAllReturnReceivedAssetMaster();

            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        public ActionResult RequisitionReturnList(string status, string message, bool isSwal = false)
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
                ViewBag.AssetMasterDataDD = _iReturnRepository.GetAllReturnRequestedAssetMaster();

            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        public ActionResult ReturnedRequisitionDetailsPage(AssetsModel assetsModel, string status, string message, string return_Request_ID)
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
                return_Request_ID = System.Web.HttpContext.Current.Session["return_Request_ID"] as string;
                //ViewBag.RequisitionDetailsForRequestorDataDD = _requisitionApproveList.GetDetailsPageForUnitIncharge(requisition_No);
                assetsModel = _iReturnRepository.GetDetailsDataForReturn(return_Request_ID);
                return View(assetsModel);
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }
        public ActionResult IssuedAssetReturnRequestListOnBehalf(string status, string message, bool isSwal = false)
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
                ViewBag.AssetMasterDataDD = _iReturnRepository.GetAllReturnRequestedAssetMasterOnBehaveOf();
                //ViewBag.AssetMasterDataDD = _iReturnRepository.GetAllReturnRequestedAssetMaster();

            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }
    }
}