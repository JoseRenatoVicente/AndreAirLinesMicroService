using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Identity;
using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services.Base;
using Identity.API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class AuthenticationService : BaseService
    {
        private readonly RoleService _roleService;
        private readonly UserService _userService;
        private readonly AppSettings _appSettings;

        public AuthenticationService(IOptions<AppSettings> appSettings, UserService userService, RoleService roleService, INotifier notifier) : base(notifier)
        {
            _appSettings = appSettings.Value;
            _userService = userService;
            _roleService = roleService;
        }



        public async Task<UserResponseLogin> LoginAsync(UserLogin userLogin)
        {
            var userReponse = await _userService.PasswordSignInAsync(userLogin);

            if (!await _userService.PasswordSignInAsync(userLogin))
            {
                Notification("Login and Password incorret");
                return null;
            }

            return await GenerateJwt(userLogin.Login);
        }


        public async Task<UserResponseLogin> CreateAsync(UserRegister userRegister)
        {
            var user = new User
            {
                Address = userRegister.Address,
                Sector = userRegister.Sector,
                Sex = userRegister.Sex,
                BirthDate = userRegister.BirthDate,
                Cpf = userRegister.Cpf,
                Email = userRegister.Email,
                Login = userRegister.Login,
                Name = userRegister.Name,
                Password = userRegister.Password,
                Phone = userRegister.Phone
            };


            user.Role = await _roleService.GetRoleByDescriptionAsync("User");

            var userReponse = await _userService.AddUserAsync(user);
            if (userReponse == null) return null;

            return await GenerateJwt(user.Login);
        }


        public async Task<UserResponseLogin> GenerateJwt(string login)
        {


            var user = await _userService.GetUserByLoginAsync(login);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_appSettings.Secret);


            var claims = new List<System.Security.Claims.Claim>();
            claims.Add(new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new System.Security.Claims.Claim("Email", user.Email));
            claims.Add(new System.Security.Claims.Claim(ClaimTypes.Role, user.Role.Description));

            var claimsIdentity = new ClaimsIdentity(claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserResponseLogin
            {
                AccessToken = tokenHandler.WriteToken(token),
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds
            };
        }
    }
}
