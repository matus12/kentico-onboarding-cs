using System.Net.Http;
using System.Web;
using TodoApp.Api.Helpers;
using TodoApp.Api.Repositories;
using TodoApp.Contracts;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Repositories;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace TodoApp.Api
{
    internal class ApiBootstrapper : IBootstrapper
    {
        public IUnityContainer RegisterTypes(IUnityContainer container)
            => container
                .RegisterType<ILocationHelper, LocationHelper>(new HierarchicalLifetimeManager())
                .RegisterType<IDbConnection, DbConnection>(new ContainerControlledLifetimeManager())
                .RegisterType<HttpRequestMessage>(new HierarchicalLifetimeManager(), InjectMessage());

        private static InjectionFactory InjectMessage()
            => new InjectionFactory(unityContainer
                => (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"]);
    }
}