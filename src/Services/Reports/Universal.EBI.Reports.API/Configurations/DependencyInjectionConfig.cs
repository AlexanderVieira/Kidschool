using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Universal.EBI.Core.Mediator;
using Universal.EBI.Core.Mediator.Interfaces;
using Universal.EBI.Reports.API.Data;
using Universal.EBI.Reports.API.Data.Repository;
using Universal.EBI.Reports.API.Models.Interfaces;
using Universal.EBI.WebAPI.Core.AspNetUser;
using Universal.EBI.WebAPI.Core.AspNetUser.Interfaces;

namespace Universal.EBI.Reports.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {            
            // API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();            

            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();           

            // Data
            services.AddScoped<IEducatorRepository, EducatorRepository>();
            services.AddScoped<IClassroomRepository, ClassroomRepository>();
            services.AddScoped<IResponsibleRepository, ResponsibleRepository>();
            services.AddScoped<IChildrenRepository, ChildrenRepository>();
            services.AddScoped<ReportContext>();
        }
    }
}