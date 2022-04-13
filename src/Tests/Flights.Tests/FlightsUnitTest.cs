using AndreAirLines.Domain.Entities;
using Flights.API.Configuration;
using Flights.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace Flights.Tests
{
    public class FlightsUnitTest
    {
        private readonly IFlightService _flightService;

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }

        public FlightsUnitTest()
        {
            var services = new ServiceCollection();

            services.ResolveDependencies(InitConfiguration());

            var serviceProvider = services.BuildServiceProvider();

            _flightService = serviceProvider.GetService<IFlightService>();
        }


        [Fact]
        public async void AddFlight()
        {
            var flight = new Flight();

            var flightResult = await _flightService.AddFlightAsync(flight);

            Assert.NotNull(flightResult);
        }

        [Fact]
        public async void GetAllRoles()
        {
            var flights = await _flightService.GetFlightsAsync();

            Assert.NotNull(flights);
        }


        [Fact]
        public async void GetFlightByIdAsync()
        {
            var flight = await _flightService.RemoveFlightAsync((await _flightService.GetFlightsAsync()).FirstOrDefault().Id);

            Assert.NotNull(flight);
        }


        [Fact]
        public async void RemoveFlight()
        {
            var flightResult = _flightService.RemoveFlightAsync((await _flightService.GetFlightsAsync()).FirstOrDefault());

            Assert.NotNull(flightResult);
        }
    }
}
