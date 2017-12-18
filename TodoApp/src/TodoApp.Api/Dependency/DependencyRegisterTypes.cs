using System.Net.Http;
using System.Web;
using TodoApp.Interfaces;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace TodoApp.Api.Dependency
{
    public class DependencyRegisterTypes : IBootstrapper
    {
        public IUnityContainer RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ILocationHelper, LocationHelper>(new HierarchicalLifetimeManager());
            container.RegisterType<HttpRequestMessage>(new HierarchicalLifetimeManager(),
                new InjectionFactory(unityContainer => (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"]));
            return container;
        }
    }
}