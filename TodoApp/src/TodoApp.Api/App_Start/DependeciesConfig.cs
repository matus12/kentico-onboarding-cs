using System.Web.Http;
using TodoApp.Api.Resolver;
using TodoApp.Contracts;
using TodoApp.Database;
using TodoApp.Services;
using Unity;

namespace TodoApp.Api
{
    internal static class DependenciesConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var apiDependencyContainer = new UnityContainer()
                .Register<ApiBootstrapper>()
                .Register<ServicesBootstrapper>()
                .Register<DatabaseBootstrapper>();
            config.DependencyResolver = new UnityResolver(apiDependencyContainer);
        }

        private static IUnityContainer Register<TBootstrapper>(this IUnityContainer container)
            where TBootstrapper : IBootstrapper, new()
            => new TBootstrapper().RegisterTypes(container);
    }
}