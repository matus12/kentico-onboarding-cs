using System.Web.Http;
using Microsoft.Practices.Unity.Configuration;
using TodoApp.Api.Dependency;
using TodoApp.Contracts.Dependency;
using TodoApp.Contracts.Resolver;
using TodoApp.Database.Dependency;
using Unity;

namespace TodoApp.Api
{
    internal static class DependenciesConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var apiDependencyContainer = new UnityContainer()
                .Register<ApiBootstrapper>()
                .Register<DatabaseBootstrapper>();
            config.DependencyResolver = new UnityResolver(apiDependencyContainer);
        }

        private static IUnityContainer Register<TBootstrapper>(this IUnityContainer container)
            where TBootstrapper : IBootstrapper, new()
            => new TBootstrapper().RegisterTypes(container);
    }
}