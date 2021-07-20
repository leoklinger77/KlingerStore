﻿using KlingerStore.Catalog.Application.AutoMapper;
using KlingerStore.Catalog.Application.Services;
using KlingerStore.Catalog.Data.Context;
using KlingerStore.Catalog.Data.Repository;
using KlingerStore.Catalog.Domain.Events;
using KlingerStore.Catalog.Domain.Interfaces;
using KlingerStore.Catalog.Domain.Interfaces.Services;
using KlingerStore.Catalog.Domain.Service;
using KlingerStore.Core.Domain.Bus;
using KlingerStore.Sales.Application.Commands;
using KlingerStore.Sales.Data.Context;
using KlingerStore.Sales.Data.Repository;
using KlingerStore.Sales.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace KlingerStore.WebApp.Mvc.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection DependencyResolve(this IServiceCollection services)
        {
            //Domain Bus
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            //AutoMapper
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));

            //Mediator
            services.AddMediatR(typeof(Startup));


            //Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();

            //
            services.AddScoped<INotificationHandler<ProductUnderStockEvent>, ProductEventHandler>();

            //Orders
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<SalesContext>();

            return services;
        }
    }
}
