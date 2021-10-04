using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Universal.EBI.Core.Mediator;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.WebAPI.Core.AspNetUser;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;

namespace Universal.EBI.Childs.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {            
            // API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            // Commands
            //services.AddScoped<IRequestHandler<RegisterChildCommand, ValidationResult>, RegisterChildCommandHandler>();
            //services.AddScoped<IRequestHandler<UpdateChildCommand, ValidationResult>, UpdateChildCommandHandler>();
            //services.AddScoped<IRequestHandler<DeleteChildCommand, ValidationResult>, DeleteChildCommandHandler>();

            //// Events
            //services.AddScoped<INotificationHandler<RegisteredChildEvent>, RegisterChildEventHandler>();
            //services.AddScoped<INotificationHandler<UpdatedChildEvent>, UpdateChildEventHandler>();
            //services.AddScoped<INotificationHandler<DeletedChildEvent>, DeleteChildEventHandler>();

            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();            
            //services.AddScoped<IChildQueries, ChildQueries>();

            // Data
            //services.AddScoped<IChildRepository, ChildRepository>();            
            //services.AddScoped<ChildContext>();
        }
    }
}