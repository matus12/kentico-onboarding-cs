using TodoApp.Contracts;
using TodoApp.Contracts.Dependency;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace TodoApp.Database.Dependency
{
    public class DatabaseBootstrapper : IBootstrapper
    {
        private readonly string _connectionString;

        public DatabaseBootStrapper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IUnityContainer RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IItemRepository, ItemsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ItemsRepository>(
                new InjectionConstructor(_connectionString));
            return container;
        }
    }
}