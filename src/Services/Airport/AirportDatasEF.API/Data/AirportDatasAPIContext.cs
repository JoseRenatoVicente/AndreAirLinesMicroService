using AndreAirLines.Domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace AirportDatas.API.Data
{
    public class AirportDatasAPIContext : DbContext
    {
        public AirportDatasAPIContext (DbContextOptions<AirportDatasAPIContext> options)
            : base(options)
        {
        }

        public DbSet<AirportData> AirportData { get; set; }
    }
}
