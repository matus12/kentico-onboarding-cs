using Unity;

namespace TodoApp.Contracts.Dependency
{
    public interface IBootstrapper
    {
        IUnityContainer RegisterTypes(IUnityContainer container);
    }
}
