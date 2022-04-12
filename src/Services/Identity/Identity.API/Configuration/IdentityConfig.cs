using AndreAirLines.Domain.Identity;
using AndreAirLines.WebAPI.Core.Identity;
using Identity.API.Certificates;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.API.Configuration
{

    public static class IdentityConfig
    {
        public static void AddIdentityConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddJwtConfiguration(configuration);

        }
    }
}