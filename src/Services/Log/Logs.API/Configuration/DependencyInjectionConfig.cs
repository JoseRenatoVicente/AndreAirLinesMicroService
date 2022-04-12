using AndreAirLines.Domain.Identity.Extensions;
using AndreAirLines.Domain.Services;
using AndreAirLines.WebAPI.Core.Notifications;
using Logs.API.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;

namespace Logs.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton(s =>
            new MongoClient(Configuration.GetConnectionString("MongoDb"))
            .GetDatabase(Configuration["ConnectionStrings:DatabaseName"]));

            //services
            services.AddHttpClient<LogService>();
            services.AddSingleton<IAspNetUser, AspNetUser>();

            //repositories
            services.AddSingleton<ILogRepository, LogRepository>();

            //notification
            services.AddSingleton<INotifier, Notifier>();

            //Identity
            services.AddSingleton<IAspNetUser, AspNetUser>();
            services.AddHttpContextAccessor();

        }
    }
}