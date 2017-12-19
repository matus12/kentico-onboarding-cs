﻿using System.Web.Http;
using TodoApp.Api.Dependency;
using TodoApp.Contracts.Resolver;
using Unity;

namespace TodoApp.Api
{
    internal static class DependenciesConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var newContainer = new ApiBootStrapper().RegisterTypes(new UnityContainer());
            var newContainer2 = new Database.Dependency.DatabaseBootStrapper().RegisterTypes(newContainer);
            config.DependencyResolver = new UnityResolver(newContainer2);
        }
    }
}