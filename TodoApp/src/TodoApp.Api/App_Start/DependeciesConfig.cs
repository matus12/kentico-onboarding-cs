using System;
using System.Configuration;
using System.Web.Http;
using TodoApp.Api.Dependency;
using TodoApp.Api.Resolver;
using TodoApp.Contracts.Dependency;
using TodoApp.Database.Dependency;
using TodoApp.Services.Dependency;
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
                .Register<DatabaseBootstrapper>(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            config.DependencyResolver = new UnityResolver(apiDependencyContainer);
        }

        private static IUnityContainer Register<TBootstrapper>(this IUnityContainer container,
            string connectionString = null)
            where TBootstrapper : IBootstrapper, new()
        {
            if (connectionString == null)
            {
                return new TBootstrapper().RegisterTypes(container);
            }
            var bootstrapper = (TBootstrapper) Activator.CreateInstance(typeof(TBootstrapper), connectionString);
            return bootstrapper.RegisterTypes(container);
        } 
    }
}