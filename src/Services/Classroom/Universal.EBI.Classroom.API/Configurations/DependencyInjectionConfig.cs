using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Universal.EBI.Classrooms.API.Application.Commands;
using Universal.EBI.Classrooms.API.Application.Events;
using Universal.EBI.Classrooms.API.Application.Queries;
using Universal.EBI.Classrooms.API.Application.Queries.Interfaces;
using Universal.EBI.Classrooms.API.Data;
using Universal.EBI.Classrooms.API.Data.Repository;
using Universal.EBI.Classrooms.API.Models.Interfaces;
using Universal.EBI.Core.Mediator;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.WebAPI.Core.AspNetUser;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;

namespace Universal.EBI.Classrooms.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {            
            // API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            // Commands
            services.AddScoped<IRequestHandler<RegisterClassroomCommand, ValidationResult>, RegisterClassroomCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateClassroomCommand, ValidationResult>, UpdateClassroomCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteClassroomCommand, ValidationResult>, DeleteClassroomCommandHandler>();

            //// Events
            services.AddScoped<INotificationHandler<RegisteredClassroomEvent>, RegisterClassroomEventHandler>();
            services.AddScoped<INotificationHandler<UpdatedClassroomEvent>, UpdateClassroomEventHandler>();
            services.AddScoped<INotificationHandler<DeletedClassroomEvent>, DeleteClassroomEventHandler>();

            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IClassroomQueries, ClassroomQueries>();

            // Data
            services.AddScoped<IClassroomRepository, ClassroomRepository>();
            services.AddScoped<IClassroomContext, ClassroomContext>();
        }
    }
}