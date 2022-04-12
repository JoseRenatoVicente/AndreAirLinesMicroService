using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services;
using AndreAirLines.Domain.Services.Base;
using AndreAirLines.Domain.Validations.Identity;
using Identity.API.Models;
using Identity.API.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class UserService : BaseService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ViaCepService _viaCepService;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, ViaCepService viaCepService, INotifier notifier) : base(notifier)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _viaCepService = viaCepService;
        }

        public async Task<IEnumerable<User>> GetUsersAsync() =>
            await _userRepository.GetAllAsync();

        public async Task<User> GetUserByLoginAsync(string login) =>
            await _userRepository.FindAsync(c => c.Login == login);

        public async Task<User> GetUserByIdAsync(string id) =>
            await _userRepository.FindAsync(c => c.Id == id);

        public async Task<User> AddUserAsync(User user)
        {
            if (await _userRepository.FindAsync(c => c.Login == user.Login || c.Email == user.Login || c.Cpf == user.Cpf) != null)
            {
                Notification("This username cannot be used");
                return null;
            }

            Address address = await _viaCepService.ConsultarCEP(user.Address);
            if (address != null) user.Address = address;

            user.Role = await _roleRepository.FindAsync(c => c.Id == user.Role.Id);

            if (!ExecuteValidation(new UserValidation(), user)) return null;

            var passwordResult = await CreatePasswordHashAsync(user.Password);

            user.Password = passwordResult.passwordHash;
            user.PasswordSalt = passwordResult.passwordSalt;

            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> PasswordSignInAsync(UserLogin userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.Login) || string.IsNullOrEmpty(userLogin.Password))
                return false;

            var user = await _userRepository.FindAsync(c => c.Login == userLogin.Login);

            return user == null ? false : await VerifyPasswordHashAsync(userLogin.Password, user.Password, user.PasswordSalt);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }
        public async Task RemoveUserAsync(string id) =>
            await _userRepository.RemoveAsync(id);

        private async Task<(string passwordSalt, string passwordHash)> CreatePasswordHashAsync(string password)
        {
            using (var hmac = new HMACSHA256())
            {
                return (Convert.ToBase64String(hmac.Key),
                        Convert.ToBase64String(await hmac.ComputeHashAsync(
                            new MemoryStream(Convert.FromBase64String(password)
                        ))));
            }
        }


        private async Task<bool> VerifyPasswordHashAsync(string password, string storedHash, string storedSalt)
        {
            Console.WriteLine(password);
            using (var hmac = new HMACSHA256(Convert.FromBase64String(storedSalt)))
            {
                string computedHash = Convert.ToBase64String(await hmac.ComputeHashAsync(new MemoryStream(Convert.FromBase64String(password))));

                if (storedHash.Equals(computedHash)) return true;
            }
            return false;
        }
    }
}
