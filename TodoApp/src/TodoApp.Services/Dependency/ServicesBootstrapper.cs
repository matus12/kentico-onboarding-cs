using TodoApp.Contracts.Dependency;
using TodoApp.Contracts.Services;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Services.Dependency
{
    public class ServicesBootstrapper : IBootstrapper
    {
        public IUnityContainer RegisterTypes(IUnityContainer container)
            => container
                .RegisterType<IItemService, ItemService>(new HierarchicalLifetimeManager())
                .RegisterType<IDateTimeService, DateTimeService>(new HierarchicalLifetimeManager())
                .RegisterType<IGuidService, GuidService>(new HierarchicalLifetimeManager());
    }
}