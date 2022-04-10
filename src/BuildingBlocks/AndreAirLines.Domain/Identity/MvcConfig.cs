using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json.Serialization;

namespace AndreAirLines.Domain.Identity
{
    public static class MvcConfig
    {
        public static void AddMvcConfiguration(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddControllers();
        }
    }
}