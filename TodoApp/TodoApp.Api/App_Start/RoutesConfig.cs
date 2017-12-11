using System.Web.Http;

namespace TodoApp.Api
{
    internal static class RoutesConfig
    {
        public static readonly string ApiV2Route = "ApiV2";

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: ApiV2Route,
                routeTemplate: "api/v2/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );
        }
    }
}