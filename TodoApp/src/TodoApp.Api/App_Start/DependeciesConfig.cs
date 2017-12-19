using System.Web.Http;
using TodoApp.Api.Dependency;
using TodoApp.Contracts.Resolver;
using TodoApp.Database.Dependency;
using Unity;

namespace TodoApp.Api
{
    internal static class DependenciesConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var apiDependencyContainer = new ApiBootStrapper().RegisterTypes(new UnityContainer());
            var apiAndDatabaseDependencyContainer
                = new DatabaseBootStrapper().RegisterTypes(apiDependencyContainer);
            config.DependencyResolver = new UnityResolver(apiAndDatabaseDependencyContainer);
        }
    }
}