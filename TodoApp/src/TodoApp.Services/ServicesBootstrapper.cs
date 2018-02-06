using TodoApp.Contracts;
using TodoApp.Contracts.Services;
using TodoApp.Contracts.Services.Factories;
using TodoApp.Services.Services;
using TodoApp.Services.Services.Factories;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Services
{
    public class ServicesBootstrapper : IBootstrapper
    {
        public IUnityContainer RegisterTypes(IUnityContainer container)
            => container
                .RegisterType<IItemService, ItemService>(new HierarchicalLifetimeManager())
                .RegisterType<ITimeService, TimeService>(new ContainerControlledLifetimeManager())
                .RegisterType<IGuidService, GuidService>(new ContainerControlledLifetimeManager());
    }
}