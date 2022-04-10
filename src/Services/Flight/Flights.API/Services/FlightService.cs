﻿using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Entities.Enums;
using AndreAirLines.Domain.Notifications;
using AndreAirLines.Domain.Services;
using AndreAirLines.Domain.Services.Base;
using AndreAirLines.Domain.Validations;
using Flights.API.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flights.API.Services
{
    public class FlightService : BaseService
    {
        private readonly GatewayService _gatewayService;
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository, GatewayService gatewayService, INotifier notifier) : base(notifier)
        {
            _gatewayService = gatewayService;
            _flightRepository = flightRepository;
        }

        public async Task<IEnumerable<Flight>> GetFlightsAsync() =>
            await _flightRepository.GetAllAsync();

        public async Task<Flight> GetFlightByIdAsync(string id) =>
            await _flightRepository.FindAsync(c => c.Id == id);

        public async Task<Flight> AddAsync(Flight flight)
        {
            if (flight.Origin.Id == flight.Destination.Id)
            {
                Notification("Origin and Destination cannot be the same");
                return flight;
            }

            Airport origin = await _gatewayService.GetFromJsonAsync<Airport>("Airport/api/Airports/" + flight.Origin.Id);
            if (origin == null)
            {
                Notification("Origin Airport does not exist registered in our database database");
                return flight;
            }

            Airport destination = await _gatewayService.GetFromJsonAsync<Airport>("Airport/api/Airports/" + flight.Destination.Id);
            if (destination == null)
            {
                Notification("Destination Airport does not exist registered in our database database");
                return flight;
            }

            Aircraft aircraft = await _gatewayService.GetFromJsonAsync<Aircraft>("Aircraft/api/Aircrafts/" + flight.Aircraft.Id);
            if (aircraft == null)
            {
                Notification("Aircraft does not exist registered in our database database");
                return flight;
            }

            flight.Origin = origin;
            flight.Destination = destination;
            flight.Aircraft = aircraft;

            if (!ExecuteValidation(new FlightValidation(), flight)) return flight;

            var user = new User { LoginUser = flight.LoginUser };
            await _gatewayService.PostLogAsync(user, null, flight, Operation.Create);

            return await _flightRepository.AddAsync(flight);
        }

        public async Task<Flight> UpdateAsync(Flight flight)
        {

            var flightBefore = await _flightRepository.FindAsync(c => c.Id == flight.Id);


            if (flightBefore == null)
            {
                Notification("Not found");
                return flight;
            }

            var user = new User { LoginUser = flight.LoginUser };
            await _gatewayService.PostLogAsync(user, flightBefore, flight, Operation.Update);

            return await _flightRepository.UpdateAsync(flight);
        }

        public async Task RemoveAsync(Flight flightIn)
        {
            var user = new User { LoginUser = flightIn.LoginUser };
            await _gatewayService.PostLogAsync(user, flightIn, null, Operation.Delete);

            await _flightRepository.RemoveAsync(flightIn);
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var flight = await _flightRepository.FindAsync(c => c.Id == id);

            if (flight == null)
            {
                Notification("Not found");
                return false;
            }

            var user = new User { LoginUser = flight.LoginUser };
            await _gatewayService.PostLogAsync(user, flight, null, Operation.Delete);

            await _flightRepository.RemoveAsync(id);

            return true;
        }
    }
}
