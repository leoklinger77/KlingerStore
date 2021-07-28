using KlingerStore.Core.Domain.Interfaces;
using KlingerStore.WebApp.Mvc.Data;
using KlingerStore.WebApp.Mvc.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace KlingerStore.WebApp.Mvc.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection IdentityResolve(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IUser, AspNetUser>();
            return services;
        }
    }
}
