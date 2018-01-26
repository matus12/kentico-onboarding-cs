using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using TodoApp.Api.Helpers;
using TodoApp.Contracts;
using TodoApp.Contracts.Dependency;
using TodoApp.Contracts.Helpers;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace TodoApp.Api.Dependency
{
    internal class ApiBootstrapper : IBootstrapper
    {
        public IUnityContainer RegisterTypes(IUnityContainer container)
            => container
                .RegisterType<ILocationHelper, LocationHelper>(new HierarchicalLifetimeManager())
                .RegisterType<IDbConnection, DbConnection>(new HierarchicalLifetimeManager())
                .RegisterType<UrlHelper>(new HierarchicalLifetimeManager(), InjectUrlHelper());

        private static InjectionFactory InjectUrlHelper()
            => new InjectionFactory(unityContainer
                => new UrlHelper(GetRequestMessage()));

        private static HttpRequestMessage GetRequestMessage()
            => (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];
    }
}