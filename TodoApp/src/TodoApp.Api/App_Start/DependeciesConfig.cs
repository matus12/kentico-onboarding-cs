using System.Web.Http;
using TodoApp.Api.Dependency;
using TodoApp.Interfaces.Resolver;
using Unity;

namespace TodoApp.Api
{
    internal static class DependenciesConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var newContainer = new DependencyRegisterTypes().RegisterTypes(new UnityContainer());
            var newContainer2 = new Interfaces.Dependency.DependencyRegisterTypes().RegisterTypes(newContainer);
            config.DependencyResolver = new UnityResolver(newContainer2);
        }
    }
}