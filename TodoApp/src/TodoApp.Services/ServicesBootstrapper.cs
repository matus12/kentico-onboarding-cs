﻿using TodoApp.Contracts;
using TodoApp.Contracts.Services;
using TodoApp.Contracts.Services.Factories;
using TodoApp.Services.Factories;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Services
{
    public class ServicesBootstrapper : IBootstrapper
    {
        public IUnityContainer RegisterTypes(IUnityContainer container)
            => container
                .RegisterType<IItemService, ItemService>(new HierarchicalLifetimeManager())
                .RegisterType<IDateTimeService, DateTimeService>(new ContainerControlledLifetimeManager())
                .RegisterType<IGuidService, GuidService>(new ContainerControlledLifetimeManager());
    }
}