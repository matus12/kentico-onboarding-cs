using TodoApp.Contracts;
using TodoApp.Contracts.Repositories;
using TodoApp.Database.Repositories;
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