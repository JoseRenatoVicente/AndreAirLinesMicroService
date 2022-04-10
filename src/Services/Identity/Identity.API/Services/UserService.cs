using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services;
using AndreAirLines.Domain.Services.Base;
using AndreAirLines.Domain.Validations.Identity;
using Identity.API.Models;
using Identity.API.Repository;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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

        public async Task<User> AddAsync(User user)
        {
            if (await _userRepository.FindAsync(c => c.Login == user.Login || c.Email == user.Login || c.Cpf == user.Cpf) != null)
            {
                Notification("This username cannot be used");
                return null;
            }

            Address address = await _viaCepService.ConsultarCEP(user.Address);
            if (address is not null) user.Address = address;

            user.Role = await _roleRepository.FindAsync(c => c.Id == user.Role.Id);

            if (!ExecuteValidation(new UserValidation(), user)) return null;

            string passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            user.Password = passwordHash;
            user.PasswordSalt = passwordSalt;

            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> PasswordSignInAsync(UserLogin userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.Login) || string.IsNullOrEmpty(userLogin.Password))
                return false;

            var user = await _userRepository.FindAsync(c => c.Login == userLogin.Login);

            // check if username exists
            if (user == null)
                return false;

            // check if password is correct
            if (!await VerifyPasswordHash(userLogin.Password, Encoding.UTF8.GetBytes(user.Password), Encoding.UTF8.GetBytes(user.PasswordSalt)))
                return false;

            // authentication successful

            return true;

        }

        public async Task<User> UpdateAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }
        public async Task RemoveAsync(string id) =>
            await _userRepository.RemoveAsync(id);


        private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = Encoding.UTF8.GetString(hmac.Key);
                passwordHash = Encoding.UTF8.GetString(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        private async Task<bool> VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = await hmac.ComputeHashAsync(new MemoryStream(Encoding.UTF8.GetBytes(password)));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
