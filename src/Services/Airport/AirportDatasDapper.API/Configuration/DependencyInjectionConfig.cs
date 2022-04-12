using AirportDatasDapper.API.Repository;
using AirportDatasDapper.Services;
using AndreAirLines.Domain.Services;
using AndreAirLines.WebAPI.Core.Notifications;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AirportDatasDapper.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //services
            services.AddScoped<AirportDataService>();
            
            services.AddSingleton<ViaCepService>();
            services.AddHttpClient<GatewayService>();

            //repositories
            services.AddScoped<IAirportDataRepository, AirportDataRepository>();

            //notification
            services.AddSingleton<INotifier, Notifier>();


        }
    }
}