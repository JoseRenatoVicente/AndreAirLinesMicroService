using AirportIngestion.Config;
using AndreAirLines.Domain.DTO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportIngestion.Repository
{
    public class AirportRepository : IAirportRepository
    {
        private readonly string _connetion;

        public AirportRepository()
        {
            _connetion = DatabaseConfiguration.Get();
        }

        public async Task<bool> AddAirport(AirportData airport)
        {
            using (var sqlConnection = new SqlConnection(_connetion))
            {
                try
                {
                    await sqlConnection.OpenAsync();
                    var query = "Insert Into " +
                                "AirportData(Id, City, Country, Code, Continent) " +
                                "Values(@Id, @City, @Country, @Code, @Continent)";
                    await sqlConnection.ExecuteAsync(query, airport);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }


            }
        }

        public async Task<IEnumerable<AirportData>> GetAllAirport()
        {
            using (var sqlConnection = new SqlConnection(_connetion))
            {
                await sqlConnection.OpenAsync();
                return await sqlConnection.QueryAsync<AirportData>
                    ("SELECT Id, City, Country, Code, Continent FROM AirportData");
            }
        }
    }
}
