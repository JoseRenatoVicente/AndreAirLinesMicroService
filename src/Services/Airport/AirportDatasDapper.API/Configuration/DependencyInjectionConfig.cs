using AirportDatasDapper.API.Repository;
using AirportDatasDapper.Services;
using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
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