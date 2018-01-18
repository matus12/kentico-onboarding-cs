﻿using System.Web.Http;
using TodoApp.Api.Dependency;
using TodoApp.Api.Resolver;
using TodoApp.Contracts.Dependency;
using TodoApp.Database.Dependency;
using TodoApp.Services.Dependency;
using Unity;
using System.Configuration;
using TodoApp.Api.Resolver;

namespace TodoApp.Api
{
    internal static class DependenciesConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var apiDependencyContainer = new UnityContainer()
                .Register<ApiBootstrapper>()
                .Register<DatabaseBootstrapper>();
            config.DependencyResolver = new UnityResolver(apiDependencyContainer);
        }

        private static IUnityContainer Register<TBootstrapper>(this IUnityContainer container)
            where TBootstrapper : IBootstrapper, new()
            => new TBootstrapper().RegisterTypes(container);
    }
}