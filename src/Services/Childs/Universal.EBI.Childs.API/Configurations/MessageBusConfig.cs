using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Universal.EBI.Childs.API.Configurations
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            //        .AddHostedService<RegisterChildIntegrationHandler>()
            //        .AddHostedService<UpdateChildIntegrationHandler>()
            //        .AddHostedService<DeleteChildIntegrationHandler>();
        }
    }
}
