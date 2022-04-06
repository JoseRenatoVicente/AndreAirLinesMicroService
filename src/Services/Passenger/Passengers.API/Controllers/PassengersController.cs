﻿using AndreAirLines.Domain.Controllers.Base;
using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;
using Passengers.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleWebAPIMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : BaseController
    {

        private readonly PassengerService _passengerService;

        public PassengersController(PassengerService passengerService, INotifier notifier) : base(notifier)
        {
            _passengerService = passengerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetPassenger()
        {
            return Ok(await _passengerService.GetPassengersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> GetPassenger(string id)
        {
            var passenger = await _passengerService.GetPassengerByIdAsync(id);

            if (passenger == null)
            {
                return NotFound();
            }

            return passenger;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassenger(string id, Passenger passenger)
        {
            if (id != passenger.Cpf)
            {
                return BadRequest();
            }

            await _passengerService.UpdateAsync(passenger);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Passenger>> PostPassenger(Passenger passenger)
        {
            return await CustomResponseAsync(await _passengerService.AddAsync(passenger));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassenger(string id)
        {
            var passenger = await GetPassenger(id);
            if (passenger == null)
            {
                return NotFound();
            }
            await _passengerService.RemoveAsync(id);

            return NoContent();
        }

    }
}