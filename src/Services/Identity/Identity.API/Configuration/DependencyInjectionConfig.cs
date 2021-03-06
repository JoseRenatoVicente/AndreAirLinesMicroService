using AndreAirLines.Domain.Identity.Extensions;
using AndreAirLines.Domain.Services;
using AndreAirLines.WebAPI.Core.Notifications;
using Identity.API.Repository;
using Identity.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;

namespace Identity.API.Configuration
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
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IRoleService, RoleService>();
            services.AddSingleton<AuthenticationService>();
            services.AddSingleton<ViaCepService>();

            //repositories
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IRoleRepository, RoleRepository>();

            //notification
            services.AddSingleton<INotifier, Notifier>();

            //identity
            services.AddSingleton<IAspNetUser, AspNetUser>();
            services.AddHttpContextAccessor();
        }
    }
}