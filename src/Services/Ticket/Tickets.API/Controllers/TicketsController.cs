using AndreAirLines.Domain.Entities;
using AndreAirLines.WebAPI.Core.Controllers;
using AndreAirLines.WebAPI.Core.Notifications;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(string id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            await _ticketsService.UpdateTicketAsync(ticket);

            return NoContent();
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            return await CustomResponseAsync(await _ticketsService.AddTicketAsync(ticket));
        }

        [Authorize(Roles = "Admin, User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(string id)
        {
            var ticket = await GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            await _ticketsService.RemoveTicketAsync(id);

            return NoContent();
        }

    }
}
