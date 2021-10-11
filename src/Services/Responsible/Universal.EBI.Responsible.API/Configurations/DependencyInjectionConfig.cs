using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Universal.EBI.Responsibles.API.Application.Commands;
using Universal.EBI.Responsibles.API.Application.Events;
using Universal.EBI.Responsibles.API.Application.Queries;
using Universal.EBI.Responsibles.API.Application.Queries.Interfaces;
using Universal.EBI.Responsibles.API.Data;
using Universal.EBI.Responsibles.API.Data.Repository;
using Universal.EBI.Responsibles.API.Models.Interfaces;
using Universal.EBI.Core.Mediator;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.WebAPI.Core.AspNetUser;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;

namespace Universal.EBI.Responsibles.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {            
            // API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            // Commands
            services.AddScoped<IRequestHandler<RegisterResponsibleCommand, ValidationResult>, RegisterResponsibleCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateResponsibleCommand, ValidationResult>, UpdateResponsibleCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteResponsibleCommand, ValidationResult>, DeleteResponsibleCommandHandler>();

            //// Events
            services.AddScoped<INotificationHandler<RegisteredResponsibleEvent>, RegisterResponsibleEventHandler>();
            services.AddScoped<INotificationHandler<UpdatedResponsibleEvent>, UpdateResponsibleEventHandler>();
            services.AddScoped<INotificationHandler<DeletedResponsibleEvent>, DeleteResponsibleEventHandler>();

            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();            
            services.AddScoped<IResponsibleQueries, ResponsibleQueries>();

            // Data
            services.AddScoped<IResponsibleRepository, ResponsibleRepository>();            
            services.AddScoped<IResponsibleContext, ResponsibleContext>();
        }
    }
}