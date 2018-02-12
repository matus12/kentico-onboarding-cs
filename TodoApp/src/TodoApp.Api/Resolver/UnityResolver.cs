using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

namespace TodoApp.Api.Resolver
{
    internal sealed class UnityResolver : IDependencyResolver
    {
        private const string HostBufferPolicySelectorException = "System.Web.Http.Hosting.IHostBufferPolicySelector";
        private const string HttpControllerSelectorException = "System.Web.Http.Dispatcher.IHttpControllerSelector";
        private const string HttpControllerActivatorException = "System.Web.Http.Dispatcher.IHttpControllerActivator";
        private const string HttpActionSelectorException = "System.Web.Http.Controllers.IHttpActionSelector";
        private const string HttpActionInvokerException = "System.Web.Http.Controllers.IHttpActionInvoker";
        private const string ExceptionHandlerException = "System.Web.Http.ExceptionHandling.IExceptionHandler";
        private const string ContentNegotiatorException = "System.Net.Http.Formatting.IContentNegotiator";
        private const string ModelMetadataProviderException = "System.Web.Http.Metadata.ModelMetadataProvider";
        private const string ModelValidatorCacheException = "System.Web.Http.Validation.IModelValidatorCache";

        private static IEnumerable<string> Exceptions
        {
            get
            {
                yield return HostBufferPolicySelectorException;
                yield return HttpControllerSelectorException;
                yield return HttpControllerActivatorException;
                yield return HttpActionSelectorException;
                yield return HttpActionInvokerException;
                yield return ExceptionHandlerException;
                yield return ContentNegotiatorException;
                yield return ModelMetadataProviderException;
                yield return ModelValidatorCacheException;
            }
        }

        private readonly IUnityContainer _container;
        private bool _disposed;

        public UnityResolver(IUnityContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
                when (Exceptions.Contains(serviceType?.FullName))
            {
                return null;
            }
            catch (ResolutionFailedException)
            {
                throw;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
                when (Exceptions.Contains(serviceType?.FullName))
            {
                return new List<object>();
            }
            catch (ResolutionFailedException)
            {
                throw;
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _container.Dispose();

            _disposed = true;
        }
    }
}