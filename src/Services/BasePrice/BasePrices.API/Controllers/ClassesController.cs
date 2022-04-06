using AndreAirLines.Domain.Controllers.Base;
using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using Classs.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Classs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : BaseController
    {
        private readonly ClassService _classsService;

        public ClassesController(ClassService classsService, INotifier notifier) : base(notifier)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(string id, Class @class)
        {
            if (id != @class.Id)
            {
                return BadRequest();
            }

            await _classsService.UpdateAsync(id, @class);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class @class)
        {
            return await CustomResponseAsync(await _classsService.AddAsync(@class));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(string id)
        {
            var @class = await GetClass(id);
            if (@class == null)
            {
                return NotFound();
            }
            await _classsService.RemoveAsync(id);

            return NoContent();
        }

    }
}
