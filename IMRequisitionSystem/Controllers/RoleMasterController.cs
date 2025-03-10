using IMRequisitionSystem.Models;
using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Models.RoleMapping;
using IMRequisitionSystem.Models.RoleMaster;
using IMRequisitionSystem.Repository;
using IMRequisitionSystem.Repository.Common;
using IMRequisitionSystem.Repository.RoleMaster;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static IMRequisitionSystem.Util.Enums;

namespace IMRequisitionSystem.Controllers
{
    public class RoleMasterController : Controller
    {
        // GET: RoleMaster

        private readonly IRoleMasterRepository _roleMasterRepository;
        private readonly IEmployeeDetailsRepository _employeeDetailsRepository;
        private readonly IRoleMappingRepository _roleMappingRepository;
        public RoleMasterController(
           IRoleMasterRepository roleMasterRepository,
           IEmployeeDetailsRepository employeeDetailsRepository,
           IRoleMappingRepository roleMappingRepository

           )
        {
            _roleMasterRepository = roleMasterRepository;
            _employeeDetailsRepository = employeeDetailsRepository;
            _roleMappingRepository = roleMappingRepository;
        }

        public ActionResult AddRoleMaster(string status, string message, bool isSwal = false)
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

                ViewBag.RoleMasterDD = _roleMasterRepository.GetAllRoleMaster();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddRoleMaster(RoleMasterModel roleMasterModel)
        {
            try
            {
                SPOutputMessage response = _roleMasterRepository.InsertRoleMaster(roleMasterModel);

                if (response.Status == 1)
                {
                    return RedirectToAction("AddRoleMaster", "RoleMaster", new { status = ToastMessageType.Success, message = response.Message, isSwal = true });
                }
                else
                {
                    return RedirectToAction("AddRoleMaster", "RoleMaster", new { status = ToastMessageType.Error, message = response.Message, isSwal = true });
                }
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return RedirectToAction("AddRoleMaster", "RoleMaster", new { status = ToastMessageType.Error, message = "Someting went wrong. Please try again", isSwal = true });
            }
        }
        public ActionResult AddUserRoleMapping(string status, string message, bool isSwal = false)
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

                ViewBag.RoleMasterForDD = _roleMasterRepository.GetAllRoleMasterForDropDown();
                ViewBag.ManagementUserDD = _employeeDetailsRepository.GetAllManagementUserData();
                ViewBag.RoleMappingDD = _roleMappingRepository.GetAllRoleMappingMaster();
                
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddUserRoleMapping(RoleMappingModel roleMappingModel)
        {
            try
            {
                SPOutputMessage response = _roleMappingRepository.InsertUserRoleMapping(roleMappingModel);

                if (response.Status == 1)
                {
                    return RedirectToAction("AddUserRoleMapping", "RoleMaster", new { status = ToastMessageType.Success, message = response.Message, isSwal = true });
                }
                else
                {
                    return RedirectToAction("AddUserRoleMapping", "RoleMaster", new { status = ToastMessageType.Error, message = response.Message, isSwal = true });
                }
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return RedirectToAction("AddUserRoleMapping", "RoleMaster", new { status = ToastMessageType.Error, message = "Someting went wrong. Please try again", isSwal = true });
            }
        }



        public JsonResult ActiveDeactiveRole(RoleMasterModel roleMasterModel)
        {
            var spResponse = _roleMasterRepository.UpdateActiveDeActiveStatus(roleMasterModel);

            //Session["Requisition_No"] = requisitionRequestModel.Requisition_No;

            var jsonResponseHandler = new JsonResponseHandler(Url);
            return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "AddRoleMaster", "RoleMaster", roleMasterModel.Role_Id);
        }

        public JsonResult ActiveDeactiveRoleMapping(RoleMappingModel roleMappingModel)
        {
            var spResponse = _roleMappingRepository.UpdateActiveDeActiveMappingStatus(roleMappingModel);

            //Session["Requisition_No"] = requisitionRequestModel.Requisition_No;

            var jsonResponseHandler = new JsonResponseHandler(Url);
            return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "AddUserRoleMapping", "RoleMaster", roleMappingModel.Mapping_Id);
        }
    }
}