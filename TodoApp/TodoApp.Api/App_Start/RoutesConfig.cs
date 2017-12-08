using System.Web.Http;
using TodoApp.DAL;
using TodoApp.DAL.Resolver;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Api
{
    internal static class RoutesConfig
    {
        internal static readonly string ApiV1Route = "ApiV1";

        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IItemRepository, ItemsRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: ApiV1Route,
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );
        }
    }
}