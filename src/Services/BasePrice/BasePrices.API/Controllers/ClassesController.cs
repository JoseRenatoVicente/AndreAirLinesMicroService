using AndreAirLines.Domain.Entities;
using AndreAirLines.WebAPI.Core.Controllers;
using AndreAirLines.WebAPI.Core.Notifications;
using Classs.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Classs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : BaseController
    {
        private readonly IClassService _classsService;

        public ClassesController(IClassService classsService, INotifier notifier) : base(notifier)
        {
            _classsService = classsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClass()
        {
            return Ok(await _classsService.GetClasssAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(string id)
        {
            var Class = await _classsService.GetClassByIdAsync(id);

            if (Class == null)
            {
                return NotFound();
            }

            return Class;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(string id, Class @class)
        {
            if (id != @class.Id)
            {
                return BadRequest();
            }

            await _classsService.UpdateClassAsync(@class);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class @class)
        {
            return await CustomResponseAsync(await _classsService.AddClassAsync(@class));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(string id)
        {
            var @class = await GetClass(id);
            if (@class == null)
            {
                return NotFound();
            }
            await _classsService.RemoveClassAsync(id);

            return NoContent();
        }

    }
}
