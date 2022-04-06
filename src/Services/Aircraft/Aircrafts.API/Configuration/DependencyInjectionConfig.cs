using Aircrafts.API.Repository;
using Aircrafts.API.Services;
using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace Aircrafts.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton<IAppSettings>(sp =>
                sp.GetRequiredService<IOptions<AppSettings>>().Value);
            services.AddScoped(c =>
                c.GetService<IMongoClient>().StartSession());
            
            //services
            services.AddSingleton<AircraftService>();

            //repositories
            services.AddSingleton<IAircraftRepository, AircraftRepository>();

            //notification
            services.AddSingleton<INotifier, Notifier>();

        }
    }
}