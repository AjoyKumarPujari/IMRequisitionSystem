
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using IMRequisitionSystem.Repository;
using Unity.AspNet.Mvc;
using IMRequisitionSystem.Repository.Login;
using IMRequisitionSystem.Repository.Common;
using IMRequisitionSystem.Repository.Requisition;
using IMRequisitionSystem.Repository.Approver;
using IMRequisitionSystem.Repository.RoleMaster;
using IMRequisitionSystem.Repository.Issuer;
using IMRequisitionSystem.Repository.Return;

namespace IMRequisitionSystem.DI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Register all your components with the container here
            // This is where the mappings between interfaces and concrete types are defined
            container.RegisterType<IAssetCategoryRepository, AssetCategoryRepository>();
            container.RegisterType<IAssetMasterRepository, AssetMasterRepository>();
            container.RegisterType<ILoginRepository, LoginRepository>();
            container.RegisterType<IDepartmentMasterRepository, DepartmentMasterRepository>();
            container.RegisterType<IAreaMasterRepository, AreaMasterRepository>();
            container.RegisterType<IRequisitionRequestRepository, RequisitionRequestRepository>();
            container.RegisterType<IEmployeeDetailsRepository, EmployeeDetailsRepository>();
            container.RegisterType<IRequisitionApproveList, RequisitionApproveList>();
            container.RegisterType<IRoleMasterRepository, RoleMasterRepository>();
            container.RegisterType<IRoleMappingRepository, RoleMappingRepository>();
            container.RegisterType<IIMRequisitionApproverList, IMRequisitionApproverList>();
            container.RegisterType<IIMIssuerRepository, IMIssuerRepository>();
            container.RegisterType<IReturnRepository, ReturnRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}