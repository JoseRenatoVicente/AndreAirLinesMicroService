using Microsoft.Extensions.Configuration;
using System.IO;

namespace AirportIngestion.Config
{
    public class DatabaseConfiguration
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static string Get()
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            return Configuration["ConnectionStrings:DefaultConnection"];

        }
    }
}
