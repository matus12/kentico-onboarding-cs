using TodoApp.Contracts;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Database.Dependency
{
    public class DatabaseBootStrapper : IBootstrapper
    {
        public IUnityContainer RegisterTypes(IUnityContainer container)
            => container.RegisterType<IItemRepository, ItemsRepository>(new HierarchicalLifetimeManager());
    }
}
