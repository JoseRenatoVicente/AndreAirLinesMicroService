using AndreAirLines.Domain.Controllers.Base;
using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using Identity.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly UserService _userService;

        public UsersController(UserService userService, INotifier notifier) : base(notifier)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var User = await _userService.GetUserByIdAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User User)
        {
            if (id != User.Id)
            {
                return BadRequest();
            }

            await _userService.UpdateAsync(User);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User User)
        {
            return await CustomResponseAsync(await _userService.AddAsync(User));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.RemoveAsync(id);

            return NoContent();
        }
    }
}
