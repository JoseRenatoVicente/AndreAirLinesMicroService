using AndreAirLines.Domain.Identity.Extensions;
using AndreAirLines.Domain.Services;
using AndreAirLines.WebAPI.Core.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using Tickets.API.Repository;
using Tickets.API.Services;

namespace Tickets.API.Configuration
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
            services.AddSingleton<ITicketService, TicketService>();
            services.AddSingleton<IAspNetUser, AspNetUser>();

            //repositories
            services.AddSingleton<ITicketRepository, TicketRepository>();

            //notification
            services.AddSingleton<INotifier, Notifier>();

            //Identity
            services.AddSingleton<IAspNetUser, AspNetUser>();
            services.AddHttpContextAccessor();

        }
    }
}