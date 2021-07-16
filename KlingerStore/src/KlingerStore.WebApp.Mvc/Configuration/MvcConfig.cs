using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KlingerStore.WebApp.Mvc.Configuration
{
    public static class MvcConfig
    {
        public static IServiceCollection MvcResolve(this IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            return services;
        }

        public static IApplicationBuilder AppResolve(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            return app;
        }
    }
}
