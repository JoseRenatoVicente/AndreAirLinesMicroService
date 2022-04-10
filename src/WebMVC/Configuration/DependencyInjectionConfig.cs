using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebMVC.Configuration
{

    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //services
            services.AddHttpClient<GatewayService>();

            //notification
            services.AddSingleton<INotifier, Notifier>();

        }
    }
}