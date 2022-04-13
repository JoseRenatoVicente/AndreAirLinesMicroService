using AndreAirLines.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tickets.API.Services
{
    public interface ITicketService
    {
        Task<Ticket> AddTicketAsync(Ticket ticket);
        Task<Ticket> GetTicketByIdAsync(string id);
        Task<IEnumerable<Ticket>> GetTicketsAsync();
        Task<bool> RemoveTicketAsync(string id);
        Task RemoveTicketAsync(Ticket ticket);
        Task<Ticket> UpdateTicketAsync(Ticket ticket);
    }
}