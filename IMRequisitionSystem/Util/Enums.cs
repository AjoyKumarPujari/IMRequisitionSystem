using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Util
{
    public class Enums
    {

        public enum ToastMessageType
        {
            Success,
            Error,
            Info,
            Warning
        }

        public enum ToastMessageParameter
        {
            MessageType,
            Message,
            SwalMessage,
            IsSwal,
        }

        public enum RequestStatus
        {
            REQUESTED,
            APPROVED,
            REJECTED,
            CONFIRMED,
            CANCELLED,
            CANCELLED_BY_EMPLOYEE,
            CANCELLED_BY_HOTEL_EXECUTIVE,
            CANCELLED_BY_ADMIN,
            AVAILED,
            NO_SHOW,
            CANCELLED_WITH_RETENTION_AMOUNT,
        }

        public enum DocumentType
        {
            CONFIRMATION_VOUCHER,
            INVOICE
        }

        public enum UserType
        {
            ADMIN,
            HOTEL_EXECUTIVE,
            EMPLOYEE,
            HOD_FH,
            HOD,
            FH,
            DIRECTOR,
        }

        public enum ReportFilter
        {
            BOOKING_REQUEST_NO,
            REQUESTED_BY,
            STATUS,
            CHECK_IN_DATE,
            CHECK_OUT_DATE
        }
      
        public enum HotelActualChangeRate
        {
            DOWN,
            BOTH_UP_DOWN,
            NO_CHANGE
        }

    }   
}