using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services;
using AndreAirLines.Domain.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Passengers.API.Repository;
using Passengers.API.Services;
using System;

namespace Passengers.API.Configuration
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
            services.AddSingleton<ViaCepService>();
            services.AddSingleton<PassengerService>();

            //repositories
            services.AddSingleton<IPassengerRepository, PassengerRepository>();

            //notification
            services.AddSingleton<INotifier, Notifier>();

        }
    }
}