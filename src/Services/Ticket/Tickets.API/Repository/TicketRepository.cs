using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Repository;
using MongoDB.Driver;

namespace Tickets.API.Repository
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
