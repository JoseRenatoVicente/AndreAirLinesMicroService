using AndreAirLines.Domain.Entities;
using Identity.API.Configuration;
using Identity.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Linq;
using Xunit;

namespace Identity.Tests
{

    public class UserUnitTest
    {
        private IUserService _userService;

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }

        public UserUnitTest()
        {
            var services = new ServiceCollection();

            services.ResolveDependencies(InitConfiguration());

            var serviceProvider = services.BuildServiceProvider();

            _userService = serviceProvider.GetService<IUserService>();
        }


        [Fact]
        public async void GetAll()
        {
            var users = await _userService.GetUsersAsync();

            Assert.Equal(0, users.Count());
        }

        [Fact]
        public async void Adduser()
        {

            var user = new User
            {
                Name = "Jose",
                Email = "jose@jose.com",
                Address = new Address { CEP = "15900017" },
                Role = new Role
                {
                    Id = "624f74350f486ff38c2f3fe4"
                }
            };

            var users = await _userService.AddUserAsync(user);

            Assert.NotNull(users);
        }
    }
}