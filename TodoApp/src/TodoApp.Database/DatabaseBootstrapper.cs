using TodoApp.Contracts;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Database
{
    public class DatabaseBootstrapper : IBootstrapper
    {
        public IUnityContainer RegisterTypes(IUnityContainer container)
            => container.RegisterType<IItemRepository, ItemsRepository>(new HierarchicalLifetimeManager());
    }
}