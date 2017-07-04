
using Autofac;
using Autofac.Integration.Mvc;
using JourneysApp.App_Start;
using JourneysApp.Models;
using JourneysApp.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JourneysApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            // autofac registration
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<JourneysContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<JourneysDbService>().As<IJourneysDbService>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // git hub webhook receiver registration
            GlobalConfiguration.Configure(WebHookConfig.Register);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // anti forgery token
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}
