using System.Web.Http;
using TodoApp.DAL;
using TodoApp.DAL.Resolver;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Api
{
    internal static class DependenciesConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IItemRepository, ItemsRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}