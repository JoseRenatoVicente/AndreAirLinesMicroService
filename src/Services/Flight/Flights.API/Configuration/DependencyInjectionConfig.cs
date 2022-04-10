using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services;
using Flights.API.Repository;
using Flights.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;

namespace Flights.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton(s =>
            new MongoClient(configuration.GetConnectionString("MongoDb"))
            .GetDatabase(configuration["ConnectionStrings:DatabaseName"]));

            //services
            services.AddHttpClient<GatewayService>();
            services.AddSingleton<FlightService>();

            //repositories
            services.AddSingleton<IFlightRepository, FlightRepository>();

            //notification
            services.AddSingleton<INotifier, Notifier>();

        }
    }
}