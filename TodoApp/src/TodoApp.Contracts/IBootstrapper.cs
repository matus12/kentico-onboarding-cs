using Unity;

namespace TodoApp.Contracts
{
    public interface IBootstrapper
    {
        IUnityContainer RegisterTypes(IUnityContainer container);
    }
}
