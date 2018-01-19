using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

namespace TodoApp.Api.Resolver
{
    internal sealed class UnityResolver : IDependencyResolver
    {
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
                when (serviceType?.FullName?.StartsWith("TodoApp") == true)
            {
                throw;
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
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