using AndreAirLines.Domain.Services;
using AndreAirLines.WebAPI.Core.Notifications;
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