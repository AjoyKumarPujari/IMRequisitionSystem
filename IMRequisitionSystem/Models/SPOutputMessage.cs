using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Models
{
    public class SPOutputMessage
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string PK { get; set; }
    }
}