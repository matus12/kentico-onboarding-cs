﻿using System.Web.Http;

namespace TodoApp.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(RoutesConfig.Register);
            GlobalConfiguration.Configure(DependencyInjectionConfig.Register);
        }
    }
}
