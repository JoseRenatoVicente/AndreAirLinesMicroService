using AirportDatas.API.Data;
using AndreAirLines.Domain.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportDatasEF.API.Repository
{
    public class AirportDataRepository : IAirportDataRepository
    {
        private readonly AirportDatasAPIContext _context;

        public AirportDataRepository(AirportDatasAPIContext context)
        {
            _context = context;
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public async Task<IEnumerable<AirportData>> GetAirportDatasAsync()
        {
            return await _context.AirportData.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AirportData>> GetAirportDatasRawAsync()
        {
            return await _context.AirportData.FromSqlRaw("SELECT Id, City, Country, Code, Continent FROM AirportData").AsNoTracking().ToListAsync();
        }

        public async Task AddAirportDataAsync(AirportData airportData)
        {
            await _context.AirportData.AddAsync(airportData).ConfigureAwait(false);
            await _context.SaveChangesAsync();
        }

        public async Task AddAirportDataRawAsync(AirportData airportData)
        {
            var query = "Insert Into " +
                               "AirportData(Id, City, Country, Code, Continent) " +
                               "Values(@Id, @City, @Country, @Code, @Continent)";
            await _context.Database.ExecuteSqlRawAsync(query, airportData);
        }

    }
}
