using IMRequisitionSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static IMRequisitionSystem.Util.Enums;
using System.Web.Mvc;

namespace IMRequisitionSystem.Util
{
    public class JsonResponseHandler
    {
        private readonly UrlHelper _urlHelper;

        public JsonResponseHandler(UrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public JsonResult HandleResponse(SPOutputMessage response, string actionName, string controllerName)
        {
            if (response.Status == 1)
            {
                return new JsonResult
                {
                    Data = new
                    {
                        redirectUrl = _urlHelper.Action(actionName, controllerName, new
                        {
                            status = ToastMessageType.Success,
                            message = response.Message,
                            isSwal = true
                        })
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new
                    {
                        status = ToastMessageType.Error,
                        redirectUrl = _urlHelper.Action(actionName, controllerName, new
                        {
                            status = ToastMessageType.Error,
                            message = response.Message,
                            isSwal = true
                        })
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        
        public JsonResult HandleResponseWithBookingRequestNo(SPOutputMessage response, string actionName, string controllerName, string bookingRequestNo)
        {
            if (response.Status == 1)
            {
                return new JsonResult
                {
                    Data = new
                    {
                        redirectUrl = _urlHelper.Action(actionName, controllerName, new
                        {
                            status = ToastMessageType.Success,
                            message = response.Message,
                            /*bookingRequestNo = bookingRequestNo*/
                        })
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new
                    {
                        status = ToastMessageType.Error,
                        redirectUrl = _urlHelper.Action(actionName, controllerName, new
                        {
                            status = ToastMessageType.Error,
                            message = response.Message,
                            /*bookingRequestNo = bookingRequestNo*/
                        })
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public JsonResult HandleException(Exception ex, string actionName, string controllerName)
        {
            LoggingClass.SaveExceptionLog(ex);

            return new JsonResult
            {
                Data = new
                {
                    status = ToastMessageType.Error,
                    redirectUrl = _urlHelper.Action(actionName, controllerName, new
                    {
                        status = ToastMessageType.Error,
                        message = "Something went wrong. Please try again"
                    })
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }

}