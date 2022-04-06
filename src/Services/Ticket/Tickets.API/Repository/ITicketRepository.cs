using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Repository;

namespace Tickets.API.Repository
{
    public interface ITicketRepository : IBaseRepository<Ticket>
    {
    }
}
