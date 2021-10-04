using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Universal.EBI.Core.Utils;
using Universal.EBI.Educators.API.Services;
using Universal.EBI.MessageBus.Configuration;

namespace Universal.EBI.Educators.API.Configurations
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                    .AddHostedService<RegisterEducatorIntegrationHandler>()
                    .AddHostedService<UpdateEducatorIntegrationHandler>()
                    .AddHostedService<DeleteEducatorIntegrationHandler>();
        }
    }
}
