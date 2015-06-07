using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Infrastructure.Binders;

namespace SportsStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //tell MVC Framework that it can use the CartModelBinder class to create instances of Cart
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
