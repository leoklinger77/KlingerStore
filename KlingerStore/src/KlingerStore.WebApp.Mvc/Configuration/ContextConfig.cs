using KlingerStore.Catalog.Data.Context;
using KlingerStore.Payment.Data.Context;
using KlingerStore.Sales.Data.Context;
using KlingerStore.WebApp.Mvc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KlingerStore.WebApp.Mvc.Configuration
{
    public static class ContextConfig
    {
        public static IServiceCollection ContextResolve(this IServiceCollection services, IConfiguration configuration)
        {
            //Identity Context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //Catalog Context
            services.AddDbContext<CatalogContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //Sales Context
            services.AddDbContext<SalesContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //Payment Context
            services.AddDbContext<PaymentContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();            

            return services;
        }
    }
}
