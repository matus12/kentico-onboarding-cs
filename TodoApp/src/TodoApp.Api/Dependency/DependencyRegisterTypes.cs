using TodoApp.Interfaces;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Api.Dependency
{
    public class DependencyRegisterTypes : IBootstrapper
    {
        public IUnityContainer RegisterTypes(IUnityContainer container)
            => container.RegisterType<ILocationHelper, LocationHelper>(new HierarchicalLifetimeManager());
    }
}