using System.Web.Http;

namespace TodoApp.Api
{
    internal static class RoutesConfig
    {
        internal static readonly string ApiV2Route = "ApiV2";

        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: ApiV2Route,
                routeTemplate: "api/v2/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );
        }
    }
}