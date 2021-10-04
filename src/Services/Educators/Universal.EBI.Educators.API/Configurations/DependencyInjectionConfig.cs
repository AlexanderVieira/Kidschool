using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Universal.EBI.Core.Mediator;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Educators.API.Application.Commands;
using Universal.EBI.Educators.API.Application.Events;
using Universal.EBI.Educators.API.Application.Queries;
using Universal.EBI.Educators.API.Application.Queries.Interfaces;
using Universal.EBI.Educators.API.Data;
using Universal.EBI.Educators.API.Data.Repository;
using Universal.EBI.Educators.API.Models.Interfaces;
using Universal.EBI.WebAPI.Core.AspNetUser;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;

namespace Universal.EBI.Educators.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {            
            // API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            // Commands
            services.AddScoped<IRequestHandler<RegisterEducatorCommand, ValidationResult>, RegisterEducatorCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateEducatorCommand, ValidationResult>, UpdateEducatorCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteEducatorCommand, ValidationResult>, DeleteEducatorCommandHandler>();

            // Events
            services.AddScoped<INotificationHandler<RegisteredEducatorEvent>, RegisterEducatorEventHandler>();
            services.AddScoped<INotificationHandler<UpdatedEducatorEvent>, UpdateEducatorEventHandler>();
            services.AddScoped<INotificationHandler<DeletedEducatorEvent>, DeleteEducatorEventHandler>();

            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();            
            services.AddScoped<IEducatorQueries, EducatorQueries>();

            // Data
            services.AddScoped<IEducatorRepository, EducatorRepository>();            
            services.AddScoped<EducatorContext>();
        }
    }
}