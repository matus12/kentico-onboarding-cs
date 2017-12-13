using System.Runtime.CompilerServices;
using System.Web.Http;

[assembly: InternalsVisibleTo("TodoApp.Api.Tests")]

namespace TodoApp.Api
{
    internal static class RoutesConfig
    {
        internal static readonly string ApiV1Route = "ApiV1";

        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: ApiV1Route,
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );
        }
    }
}