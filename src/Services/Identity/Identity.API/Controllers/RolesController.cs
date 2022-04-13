using AndreAirLines.Domain.Entities;
using Identity.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
        {
            return Ok(await _roleService.GetRolesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(string id)
        {
            var Role = await _roleService.GetRoleByIdAsync(id);

            if (Role == null)
            {
                return NotFound();
            }

            return Role;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(string id, Role Role)
        {
            if (id != Role.Id)
            {
                return BadRequest();
            }

            await _roleService.UpdateRoleAsync(Role);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role Role)
        {
            return await _roleService.AddRoleAsync(Role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await GetRole(id);
            if (role == null)
            {
                return NotFound();
            }
            await _roleService.RemoveRoleAsync(id);

            return NoContent();
        }
    }
}
