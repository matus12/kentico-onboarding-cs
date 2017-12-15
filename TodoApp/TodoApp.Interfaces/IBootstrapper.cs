using Unity;

namespace TodoApp.Interfaces
{
    public interface IBootstrapper
    {
        IUnityContainer RegisterTypes(IUnityContainer container);
    }
}
