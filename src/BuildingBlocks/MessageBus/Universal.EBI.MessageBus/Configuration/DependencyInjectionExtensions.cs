using Microsoft.Extensions.DependencyInjection;
using Universal.EBI.MessageBus.Interfaces;
using System;

namespace Universal.EBI.MessageBus.Configuration
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
        {
            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();

            services.AddSingleton<IMessageBus>(new MessageBus(connection));

            return services;
        }
    }
}
