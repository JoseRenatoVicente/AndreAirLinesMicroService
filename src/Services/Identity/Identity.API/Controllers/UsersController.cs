using AndreAirLines.Domain.Entities;
using AndreAirLines.WebAPI.Core.Controllers;
using AndreAirLines.WebAPI.Core.Notifications;
using Identity.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [SecurityHeaders]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService, INotifier notifier) : base(notifier)
        {
            _userService = userService;
        }

        [SecurityHeaders]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await _userService.UpdateUserAsync(user);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            return await CustomResponseAsync(await _userService.AddUserAsync(user));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.RemoveUserAsync(id);

            return NoContent();
        }
    }
}
