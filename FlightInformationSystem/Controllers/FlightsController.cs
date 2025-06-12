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
        private readonly ILogger<FlightsController> _logger;

        public FlightsController(IFlightService flightService, ILogger<FlightsController> logger)
        {
            _flightService = flightService;
            _logger = logger;
        }

        [HttpGet("{flightNumber}")]
        [ProducesResponseType(typeof(Flight), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetByFlightNumber([FromRoute] string? flightNumber)
        {
            _logger.LogInformation("Processing request: GET /api/flights/{FlightNumber}", flightNumber);
            try
            {
                var flight = await _flightService.GetFlightByNumberAsync(flightNumber);
                if (flight == null)
                {
                    _logger.LogInformation("Request finished: GET /api/flights/{FlightNumber} - 404 Not Found", flightNumber);
                    return NotFound("There is no a flight with such a number.");
                }
                _logger.LogInformation("Request finished: GET /api/flights/{FlightNumber} - 200 OK", flightNumber);
                return Ok(flight);
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation("Request finished: GET /api/flights/{FlightNumber} - 400 Bad Request", flightNumber);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred processing request: GET /api/flights/{FlightNumber}", flightNumber);
                return StatusCode(500);
            }
        }

        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<Flight>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetByDate([FromQuery] DateOnly date)
        {
            _logger.LogInformation("Processing request: GET /api/flights?date={Date}", date);
            try
            {
                var flights = await _flightService.GetFlightsByDateAsync(date);
                _logger.LogInformation("Request finished: GET /api/flights?date={Date} - 200 OK, found {Count} items", date, flights.Count());
                return Ok(flights);
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation("Request finished: GET /api/flights?date={Date} - 400 Bad Request", date);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred processing request: GET /api/flights?date={Date}", date);
                return StatusCode(500);
            }
        }

        [HttpGet("departure")]
        [ProducesResponseType(typeof(IEnumerable<Flight>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetByDepartureAndDate([FromQuery] string? city, [FromQuery] DateOnly date)
        {
            _logger.LogInformation("Processing request: GET /api/flights/departure?city={City}&date={Date}", city, date);
            try
            {
                var flights = await _flightService.GetFlightsByDepartureCityAndDateAsync(city, date);
                _logger.LogInformation("Request finished: GET /api/flights/departure - 200 OK, found {Count} items", flights.Count());
                return Ok(flights);
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation("Request finished: GET /api/flights/departure - 400 Bad Request");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred processing request: GET /api/flights/departure");
                return StatusCode(500);
            }
        }


        [HttpGet("arrival")]
        [ProducesResponseType(typeof(IEnumerable<Flight>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetByArrivalAndDate([FromQuery] string? city, [FromQuery] DateOnly date)
        {
            _logger.LogInformation("Processing request: GET /api/flights/arrival?city={City}&date={Date}", city, date);
            try
            {
                var flights = await _flightService.GetFlightsByArrivalCityAndDateAsync(city, date);
                _logger.LogInformation("Request finished: GET /api/flights/arrival - 200 OK, found {Count} items", flights.Count());
                return Ok(flights);
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation("Request finished: GET /api/flights/arrival - 400 Bad Request");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred processing request: GET /api/flights/arrival");
                return StatusCode(500);
            }
        }
    }
}