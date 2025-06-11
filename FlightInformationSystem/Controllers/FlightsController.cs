using FlightStorageService.Models;
using FlightStorageService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FlightStorageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightsController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet("{flightNumber}")]
        [ProducesResponseType(typeof(Flight), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetByFlightNumber([FromRoute] string flightNumber)
        {
            try
            {
                var flight = await _flightService.GetFlightByNumberAsync(flightNumber);
                return flight == null ? NotFound("There is no a flight with such a number.") : Ok(flight);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Flight>), 200)]
        public async Task<IActionResult> GetByDate([FromQuery] DateOnly date)
        {
            var flights = await _flightService.GetFlightsByDateAsync(date);
            return Ok(flights);
        }

        [HttpGet("departure")]
        [ProducesResponseType(typeof(IEnumerable<Flight>), 200)]
        public async Task<IActionResult> GetByDeparture([FromQuery] string city, [FromQuery] DateOnly date)
        {
            var flights = await _flightService.GetFlightsByDepartureCityAndDateAsync(city, date);
            return Ok(flights);
        }


        [HttpGet("arrival")]
        [ProducesResponseType(typeof(IEnumerable<Flight>), 200)]
        public async Task<IActionResult> GetByArrival([FromQuery] string city, [FromQuery] DateOnly date)
        {
            var flights = await _flightService.GetFlightsByArrivalCityAndDateAsync(city, date);
            return Ok(flights);
        }
    }
}
