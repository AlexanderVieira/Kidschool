using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetDevPack.Security.Jwt.AspNetCore;
using Universal.EBI.Auth.API.Services;
using Universal.EBI.WebAPI.Core.AspNetUser;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;
using Universal.EBI.WebAPI.Core.Auth;

namespace Universal.EBI.Auth.API.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<AuthService>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            return services;
        }
        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseJwksDiscovery();

            return app;
        }
    }
}
