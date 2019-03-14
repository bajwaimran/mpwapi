using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using MasspackWebApi.Controllers;
using MasspackWebApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MasspackWebApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            XpoHelper.GetDataLayer();
            ModelBinders.Binders.DefaultBinder = new DevExpress.Web.Mvc.DevExpressEditorsBinder();

            InitializeDatabaseEntities();

        }

        void InitializeDatabaseEntities()
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                int cnt = (int)uow.Evaluate<MasspackWebApi.Models.XpoApplicationRole>(CriteriaOperator.Parse("count"), null);
                if (cnt == 0)
                {
                    new XpoApplicationRole(uow)
                    {
                        Name = "SuperAdmin"
                    };
                    new XpoApplicationRole(uow)
                    {
                        Name = "Admin"
                    };
                    new XpoApplicationRole(uow)
                    {
                        Name = "User"
                    };
                    uow.CommitChanges();
                }
                //cnt = (int)uow.Evaluate<MasspackWebApi.Models.XpoApplicationUser>(CriteriaOperator.Parse("count"), null);

                //IPasswordHasher hasher = new PasswordHasher();


                //string hashPassword = hasher.HashPassword("Abona@2018");
                //if (cnt == 0)
                //{
                //    var user = new XpoApplicationUser(uow)
                //    {
                //        Email = "i.munawer@abona-erp.com",
                //        EmailConfirmed = true,
                //        PasswordHash = hashPassword
                //    };
                //    uow.CommitChanges();
                //    var role = uow.FindObject<XpoApplicationRole>(CriteriaOperator.Parse("NameUpper LIKE ?", "SUPERADMIN"));
                //    user.Roles.Add(role);
                //    uow.CommitChanges();
                //}


            }
        }
    }
}
