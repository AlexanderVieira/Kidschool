using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Universal.EBI.Responsibles.API.Services;
using Universal.EBI.Core.Utils;
using Universal.EBI.MessageBus.Configuration;

namespace Universal.EBI.Responsibles.API.Configurations
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                    .AddHostedService<RegisterResponsibleIntegrationHandler>()
                    .AddHostedService<UpdateResponsibleIntegrationHandler>()
                    .AddHostedService<DeleteResponsibleIntegrationHandler>();
        }
    }
}
