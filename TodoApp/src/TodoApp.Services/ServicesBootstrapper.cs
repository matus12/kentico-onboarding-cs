using TodoApp.Contracts;
using TodoApp.Contracts.Services;
using TodoApp.Contracts.Services.Factories;
using TodoApp.Services.Services;
using TodoApp.Services.Services.Helpers;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Services
{
    public class ServicesBootstrapper : IBootstrapper
    {
        public IUnityContainer RegisterTypes(IUnityContainer container)
            => container
                .RegisterType<IAddItemService, AddItemService>(new HierarchicalLifetimeManager())
                .RegisterType<IGetItemByIdService, GetItemByIdService>(new HierarchicalLifetimeManager())
                .RegisterType<ITimeService, TimeService>(new ContainerControlledLifetimeManager())
                .RegisterType<IGuidService, GuidService>(new ContainerControlledLifetimeManager());
    }
}