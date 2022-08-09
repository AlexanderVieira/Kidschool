using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Universal.EBI.Childs.API.Configuration;
using Universal.EBI.Childs.API.Configurations;

namespace Universal.EBI.Childs.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;
        public Startup(IWebHostEnvironment hostEnvironment)
        {
            _env = hostEnvironment;
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (_env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddApiConfiguration(Configuration, _env);            
            //services.AddJwtConfiguration(Configuration);
            services.AddSwaggerConfiguration();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.RegisterServices();
            services.AddMessageBusConfiguration(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerConfiguration();
            app.UseApiConfiguration(env);
        }
    }
}
