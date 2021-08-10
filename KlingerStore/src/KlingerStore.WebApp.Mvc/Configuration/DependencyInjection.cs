using KlingerStore.Catalog.Application.AutoMapper;
using KlingerStore.Catalog.Application.Services;
using KlingerStore.Catalog.Data.Context;
using KlingerStore.Catalog.Data.Repository;
using KlingerStore.Catalog.Domain.Events;
using KlingerStore.Catalog.Domain.Interfaces;
using KlingerStore.Catalog.Domain.Interfaces.Services;
using KlingerStore.Catalog.Domain.Service;
using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.Data.EventSource;
using KlingerStore.Core.Domain.Message.CommonMessages.IntefrationEvents;
using KlingerStore.Core.Domain.Message.CommonMessages.Notification;
using KlingerStore.EventSourc.Interfaces;
using KlingerStore.EventSourc.Services;
using KlingerStore.Payment.AntCorruption.Interfaces;
using KlingerStore.Payment.AntCorruption.Service;
using KlingerStore.Payment.Data.Context;
using KlingerStore.Payment.Data.Repository;
using KlingerStore.Payment.Data.Service;
using KlingerStore.Payment.Domain.Events;
using KlingerStore.Payment.Domain.Interfaces;
using KlingerStore.Sales.Application.Commands;
using KlingerStore.Sales.Application.Events;
using KlingerStore.Sales.Application.Querys;
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
            //Mediator

            services.AddScoped<IMediatrHandler, MediatrHandler>();

            //AutoMapper
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));

            //Notification
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();


            //Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();

            services.AddScoped<INotificationHandler<Catalog.Domain.Events.OrderDraftOrderInitEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<StartOrderEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<OrderProcessCanceledEvent>, ProductEventHandler>();

            //Orders            
            services.AddScoped<IOrderQuerys, OrderQuerys>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<SalesContext>();

            services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<ApplyVoucherOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CanceledOrderAndReverseStockCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CanceledProcessOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<FinishOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<StartOrderCommand, bool>, OrderCommandHandler>();
            
            services.AddScoped<INotificationHandler<Sales.Application.Events.OrderDraftOrderInitEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderItemAddEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderItemUpdateEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderStockRejectedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<PaymentSuccessEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<PaymentRefusedEvent>, OrderEventHandler>();


            //Payment
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentCartCreditFacade, PaymentCartCreditFacade>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
            services.AddScoped<IConfigurationManager, ConfigurationManager>();
            services.AddScoped<PaymentContext>();


            services.AddScoped<INotificationHandler<OrderStockConfirmadEvent>, PaymentEventHandler>();

            //EventSource
            services.AddSingleton<IEventStoreService, EventStoreService>();
            services.AddSingleton<IEventSourceRepository, EventSourceRepository>();

            return services;
        }
    }
}
