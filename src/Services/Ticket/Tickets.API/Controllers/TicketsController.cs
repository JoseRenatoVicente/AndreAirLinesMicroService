using AndreAirLines.Domain.Controllers.Base;
using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tickets.API.Services;

namespace Tickets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : BaseController
    {
        private readonly TicketService _ticketsService;
        public TicketsController(TicketService ticketsService, INotifier notifier) : base(notifier)
        {
            _ticketsService = ticketsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket()
        {
            return Ok(await _ticketsService.GetTicketsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(string id)
        {
            var ticket = await _ticketsService.GetTicketByIdAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(string id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            await _ticketsService.UpdateAsync(id, ticket);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            return await CustomResponseAsync(await _ticketsService.AddAsync(ticket));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(string id)
        {
            var ticket = await GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            await _ticketsService.RemoveAsync(id);

            return NoContent();
        }

    }
}
