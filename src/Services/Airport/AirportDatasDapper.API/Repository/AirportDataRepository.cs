using AirportDatasDapper.Config;
using AndreAirLines.Domain.DTO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AirportDatasDapper.API.Repository
{
    public class AirportDataRepository : IAirportDataRepository
    {
        private readonly string _connetion;

        public AirportDataRepository()
        {
            _connetion = DatabaseConfiguration.Get();
        }
        public async Task<IEnumerable<AirportData>> GetAllAirportAsync()
        {
            using (var sqlConnection = new SqlConnection(_connetion))
            {
                await sqlConnection.OpenAsync();
                return await sqlConnection.QueryAsync<AirportData>
                    ("SELECT Id, City, Country, Code, Continent FROM AirportData");
            }
        }

        public async Task<AirportData> GetAirportByIdAsync(string code)
        {
            using (var sqlConnection = new SqlConnection(_connetion))
            {
                await sqlConnection.OpenAsync();
                string query = "SELECT Id, City, Country, Code, Continent FROM AirportData WHERE Code = @Code";

                return await sqlConnection.QueryFirstOrDefaultAsync<AirportData>
                    (query, new { Code = code });
            }
        }

        public async Task<bool> AddAirportAsync(AirportData airport)
        {
            using (var sqlConnection = new SqlConnection(_connetion))
            {
                try
                {
                    await sqlConnection.OpenAsync();
                    string query = "Insert Into " +
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

        public async Task<bool> UpdateAirportAsync(AirportData airport)
        {
            using (var sqlConnection = new SqlConnection(_connetion))
            {
                try
                {
                    await sqlConnection.OpenAsync();
                    string query = "UPDATE SET " +
                                "City = @City, Country = @Country, Continent = @Continent " +
                                "WHERE Code = @Code";
                    await sqlConnection.ExecuteAsync(query, airport);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteAirportAsync(string code)
        {
            using (var sqlConnection = new SqlConnection(_connetion))
            {
                try
                {
                    await sqlConnection.OpenAsync();
                    string query = "DELETE FROM AirportData WHERE Code = @code";
                    await sqlConnection.QueryFirstOrDefaultAsync<AirportData>
                        (query, new { code = code });

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
