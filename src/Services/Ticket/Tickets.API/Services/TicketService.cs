using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Entities.Enums;
using AndreAirLines.Domain.Services;
using AndreAirLines.Domain.Services.Base;
using AndreAirLines.Domain.Validations;
using AndreAirLines.WebAPI.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tickets.API.Repository;

namespace Tickets.API.Services
{
    public class TicketService : BaseService, ITicketService
    {
        private readonly GatewayService _gatewayService;
        private readonly ITicketRepository _ticketRepository;
        public TicketService(ITicketRepository ticketRepository, GatewayService gatewayService, INotifier notifier) : base(notifier)
        {
            _gatewayService = gatewayService;
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync() =>
            await _ticketRepository.GetAllAsync();

        public async Task<Ticket> GetTicketByIdAsync(string id) =>
            await _ticketRepository.FindAsync(c => c.Id == id);

        public async Task<Ticket> AddTicketAsync(Ticket ticket)
        {

            Passenger passenger = await _gatewayService.GetFromJsonAsync<Passenger>("Passenger/api/Passengers/" + ticket.Passenger.Id);
            if (passenger == null)
            {
                Notification("Passenger does not exist registered in our database database");
                return ticket;
            }

            Flight flight = await _gatewayService.GetFromJsonAsync<Flight>("Flight/api/Flights/" + ticket.Flight.Id);
            if (flight == null)
            {
                Notification("Flight does not exist registered in our database database");
                return ticket;
            }

            Class @class = await _gatewayService.GetFromJsonAsync<Class>("BasePrice/api/Classes/" + ticket.Class.Id);
            if (@class == null)
            {
                Notification("Promotion cannot be applied");
                return ticket;
            }

            BasePrice basePrice = await _gatewayService.GetFromJsonAsync<BasePrice>("BasePrice/api/BasePrices/" + flight.Origin.Id + "/" + flight.Destination.Id);
            if (basePrice == null)
            {
                Notification("Promotion cannot be applied");
                return ticket;
            }

            ticket.Passenger = passenger;
            ticket.Flight = flight;
            ticket.BasePrice = basePrice;
            ticket.Class = @class;

            ticket.TotalPrice = ticket.PricePromotion();

            if (!ExecuteValidation(new TicketValidation(), ticket)) return ticket;

            await _gatewayService.PostLogAsync(null, ticket, Operation.Create);

            return await _ticketRepository.AddAsync(ticket);
        }

        public async Task<Ticket> UpdateTicketAsync(Ticket ticket)
        {

            var ticketBefore = await _ticketRepository.FindAsync(c => c.Id == ticket.Id);

            if (ticketBefore == null)
            {
                Notification("Not found");
                return ticket;
            }

            await _gatewayService.PostLogAsync(ticketBefore, ticket, Operation.Update);

            return await _ticketRepository.UpdateAsync(ticket);
        }

        public async Task RemoveTicketAsync(Ticket ticket)
        {
            await _gatewayService.PostLogAsync(ticket, null, Operation.Delete);

            await _ticketRepository.RemoveAsync(ticket);
        }

        public async Task<bool> RemoveTicketAsync(string id)
        {
            var ticket = await _ticketRepository.FindAsync(c => c.Id == id);

            if (ticket == null)
            {
                Notification("Not found");
                return false;
            }

            await _gatewayService.PostLogAsync(ticket, null, Operation.Delete);

            await _ticketRepository.RemoveAsync(id);

            return true;
        }
    }
}
