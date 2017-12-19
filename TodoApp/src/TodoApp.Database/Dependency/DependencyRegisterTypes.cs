using TodoApp.Interfaces;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Database.Dependency
{
    public class DependencyRegisterTypes : IBootstrapper
    {
        public IUnityContainer RegisterTypes(IUnityContainer container)
            => container.RegisterType<IItemRepository, ItemsRepository>(new HierarchicalLifetimeManager());
    }
}
