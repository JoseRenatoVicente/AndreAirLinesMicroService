using AndreAirLines.Domain.Entities;
using Identity.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public interface IUserService
    {
        Task<User> AddUserAsync(User user);
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByLoginAsync(string login);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<bool> PasswordSignInAsync(UserLogin userLogin);
        Task RemoveUserAsync(string id);
        Task<User> UpdateUserAsync(User user);
    }
}