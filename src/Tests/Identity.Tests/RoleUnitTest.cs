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
    public class RoleUnitTest
    {
        private IRoleService _roleService;


        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }

        public RoleUnitTest()
        {
            var services = new ServiceCollection();

            services.ResolveDependencies(InitConfiguration());

            var serviceProvider = services.BuildServiceProvider();

            _roleService = serviceProvider.GetService<IRoleService>();
        }

        [Fact]
        public async void AddRole()
        {
            var role = new Role
            {
                Description = "Admin"
            };

            var roleResult = await _roleService.AddRoleAsync(role);

            Assert.NotNull(roleResult);
        }

        [Fact]
        public async void GetAllRoles()
        {
            var roles = await _roleService.GetRolesAsync();

            Assert.NotNull(roles);
        }


        [Fact]
        public async void GetRoleByIdAsync()
        {
            var role = await _roleService.GetRoleByIdAsync((await _roleService.GetRolesAsync()).FirstOrDefault().Id);

            Assert.NotNull(role);
        }


        [Fact]
        public async void RemoveRole()
        {
            var roleResult = _roleService.RemoveRoleAsync((await _roleService.GetRolesAsync()).FirstOrDefault());

            Assert.NotNull(roleResult);
        }
    }
}