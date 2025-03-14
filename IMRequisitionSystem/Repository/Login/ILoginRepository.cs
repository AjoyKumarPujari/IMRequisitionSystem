﻿using IMRequisitionSystem.Models;
using IMRequisitionSystem.Models.Login;
using IMRequisitionSystem.Models.SessionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRequisitionSystem.Repository.Login
{
    public interface ILoginRepository
    {

        LoginModel CheckUserLogin(string username, string password);
        SessionDataModel CheckAdminAccessByUserCode(string userCode);
        (SPOutputMessage, LoginModel) GetUserDetailsByUserCode(string userCode);


    }
}
