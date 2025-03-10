using Dapper;
using Holiday_Home.Util;
using IMRequisitionSystem.Models;
using IMRequisitionSystem.Models.Login;
using IMRequisitionSystem.Models.SessionData;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace IMRequisitionSystem.Repository.Login
{
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        
        public LoginModel CheckUserLogin(string username, string password)
        {
            LoginModel user = new LoginModel();
            try
            {

                var (spOutputMessageDev, employeeDev) = GetUserDetailsByUserCode(username);


                user.Message = "Login Successfully";
                user.IsAuthenticated = true;

                if (spOutputMessageDev.Status == 1)
                {
                    if (employeeDev != null)
                    {

                        user.UserName = employeeDev.Name;
                        user.Password = password;
                        user.UserCode = employeeDev.UserCode;
                        user.Designation = employeeDev.Designation;
                        user.DepartmentID = employeeDev.DepartmentID;
                        user.DepartmentName = employeeDev.DepartmentName;
                        user.Name = employeeDev.Name;
                        user.Email = employeeDev.Email;

                        SessionDataModel employeeAdmin = CheckAdminAccessByUserCode(employeeDev.UserCode);

                        if (employeeAdmin != null)
                        {
                            
                            user.SUPER_ADMIN = employeeAdmin.SUPER_ADMIN;
                            user.LOCATIONAL_ADMIN = employeeAdmin.LOCATIONAL_ADMIN;
                            user.EMPLOYEE = employeeAdmin.EMPLOYEE;
                            user.HOD_UNIT_INCHARGE = employeeAdmin.HOD_UNIT_INCHARGE;
                            user.IM_ISSUER = employeeAdmin.IM_ISSUER;
                            user.IM_APPROVER = employeeAdmin.IM_APPROVER;
                            user.ALLOCATOR = employeeAdmin.ALLOCATOR;
                            user.ADMIN = employeeAdmin.ADMIN;
                        }
                        

                        user.Message = "Login Successfully";
                        user.IsAuthenticated = true;

                    }
                    else
                    {
                        user.Message = "Credentials are incorrect";
                        user.IsAuthenticated = false;
                    }
                    user.UserName = employeeDev.Name;
                    user.Password = password;
                    user.UserCode = employeeDev.UserCode;
                    user.Designation = employeeDev.Designation;
                    user.DepartmentID = employeeDev.DepartmentID;
                    user.DepartmentName = employeeDev.DepartmentName;
                    user.Name = employeeDev.Name;
                    user.Email = employeeDev.Email;
                   
                }
                else
                {
                    user.Message = spOutputMessageDev.Message;
                    user.IsAuthenticated = false;
                }

                return user;

            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);

                user.Message = "Credentials are incorrect";
                user.IsAuthenticated = false;

            }
            return user;
        }

        public SessionDataModel CheckAdminAccessByUserCode(string userCode)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "CHECK_USER_ROLE");
                parameters.Add("@UserCode", userCode);

                return ExecuteStoredProcedure("UserLogin", parameters, reader =>
                {
                    return reader.ReadFirstOrDefault<SessionDataModel>();
                });
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                return new SessionDataModel();
            }
        }




        public (SPOutputMessage, LoginModel) GetUserDetailsByUserCode(string userCode)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Type", "GET_EMPLOYEE_DETAILS_BY_USERCODE");
                parameters.Add("@UserCode", userCode);

                // Output parameters
                parameters.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@Message", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

                LoginModel loginUser = new LoginModel();

                loginUser = ExecuteStoredProcedure("UserLogin", parameters, reader =>
                {
                    return reader.ReadFirstOrDefault<LoginModel>();
                });

                SPOutputMessage spResponse = new SPOutputMessage
                {
                    Status = parameters.Get<int>("@Status"),
                    Message = parameters.Get<string>("@Message"),
                };

                return (spResponse, loginUser);
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);

                SPOutputMessage spResponse = new SPOutputMessage()
                {
                    Status = 2,
                    Message = "Something went wrong, try again later."
                };
                return (spResponse, new LoginModel());
            }

        }


    }
}