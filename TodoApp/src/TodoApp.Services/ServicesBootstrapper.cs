﻿using TodoApp.Contracts;
using TodoApp.Contracts.Factories;
using TodoApp.Contracts.Services;
using TodoApp.Services.Factories;
using TodoApp.Services.Services;
using Unity;
using Unity.Lifetime;

namespace TodoApp.Services
{
    public class ServicesBootstrapper : IBootstrapper
    {
        public IUnityContainer RegisterTypes(IUnityContainer container)
            => container
                .RegisterType<IAddItemService, AddItemService>(new HierarchicalLifetimeManager())
                .RegisterType<IGetItemByIdService, GetItemByIdService>(new HierarchicalLifetimeManager())
                .RegisterType<IUpdateItemService, UpdateItemService>(new HierarchicalLifetimeManager())
                .RegisterType<ITimeFactory, TimeFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<IGuidFactory, GuidFactory>(new ContainerControlledLifetimeManager());
    }
}