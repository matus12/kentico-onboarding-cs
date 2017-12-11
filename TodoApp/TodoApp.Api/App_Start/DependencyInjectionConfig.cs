using System.Web.Http;
using TodoApp.Api.Controllers;
using TodoApp.Api.Resolvers;
using Unity;
using Unity.Injection;

namespace TodoApp.Api
{
    public class DependencyInjectionConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<ItemsController>(new InjectionConstructor(RoutesConfig.ApiV2Route));
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}