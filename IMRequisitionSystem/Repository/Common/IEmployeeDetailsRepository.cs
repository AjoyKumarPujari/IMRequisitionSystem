using Dapper;
using IMRequisitionSystem.Models;
using IMRequisitionSystem.Models.Area;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRequisitionSystem.Repository.Common
{
    public interface IEmployeeDetailsRepository
    {
        List<EmployeeModel> GetAllNonmanagementUserData();
        List<EmployeeModel> GetApproverDataViaGreade(string employeeCode);
       
    }
}
