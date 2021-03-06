using AndreAirLines.Domain.Identity.Extensions;
using AndreAirLines.Domain.Services;
using AndreAirLines.WebAPI.Core.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Passengers.API.Repository;
using Passengers.API.Services;
using System;

namespace Passengers.API.Configuration
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
            services.AddSingleton<ViaCepService>();
            services.AddHttpClient<GatewayService>();
            services.AddSingleton<IPassengerService, PassengerService>();
            services.AddSingleton<IAspNetUser, AspNetUser>();

            //repositories
            services.AddSingleton<IPassengerRepository, PassengerRepository>();

            //notification
            services.AddSingleton<INotifier, Notifier>();

            //Identity
            services.AddSingleton<IAspNetUser, AspNetUser>();
            services.AddHttpContextAccessor();

        }
    }
}