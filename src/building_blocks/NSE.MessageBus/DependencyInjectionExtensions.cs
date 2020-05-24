
using Microsoft.Extensions.DependencyInjection;
using System;

namespace NSE.MessageBus
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
        {
            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException("Connection String is null for MessageBus");

            services.AddSingleton<IMessageBus>(new MessageBus(connection));

            return services;
        }
    }
}
