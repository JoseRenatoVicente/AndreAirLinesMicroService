using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services;
using AndreAirLines.Domain.Settings;
using BasePrices.API.Infrastructure.Repository;
using BasePrices.API.Services;
using Classs.API.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace BasePrices.API.Configuration
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
            services.AddHttpClient<GatewayService>();
            services.AddSingleton<BasePriceService>();
            services.AddSingleton<ClassService>();

            //repositories
            services.AddSingleton<IBasePriceRepository, BasePriceRepository>();
            services.AddSingleton<IClassRepository, ClassRepository>();

            //notification
            services.AddSingleton<INotifier, Notifier>();


        }
    }
}