﻿using System.Web.Http;

namespace TodoApp.Api
{
    internal static class RoutesConfig
    {
        internal static readonly string ApiV1Route = "ApiV1";

        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                ApiV1Route,
                "api/v1/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );
        }
    }
}