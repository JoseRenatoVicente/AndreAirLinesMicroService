using AndreAirLines.Domain.Identity.Extensions;
using AndreAirLines.WebAPI.Core.Controllers;
using AndreAirLines.WebAPI.Core.Notifications;
using Identity.API.Models;
using Identity.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly AuthenticationService _authService;

        public AuthController(AuthenticationService authService, IAspNetUser aspNetUser, INotifier notifier) : base(notifier)
        {
            _authService = authService;
            _aspNetUser = aspNetUser;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {
            return await CustomResponseAsync(await _authService.CreateAsync(userRegister));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLogin usuarioLogin)
        {
            return await CustomResponseAsync(await _authService.LoginAsync(usuarioLogin));
        }

        [HttpPost("Checar-Login")]
        public ActionResult CheckLogin()
        {
            return Ok(_aspNetUser.GetUserId());
        }

    }
}
