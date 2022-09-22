using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Universal.EBI.Core.Utils;
using Universal.EBI.MessageBus.Configuration;
using Universal.EBI.Reports.API.Services;

namespace Universal.EBI.Reports.API.Configurations
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
                            //.AddHostedService<RegisterClassroomReportIntegrationHandler>();
        }
    }
}
