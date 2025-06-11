using FlightStorageService.Controllers;
using FlightStorageService.Models;
using FlightStorageService.Repositories;

namespace FlightStorageService.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _repository;
        private readonly ILogger<FlightService> _logger;

        public FlightService(IFlightRepository repository, ILogger<FlightService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<Flight?> GetFlightByNumberAsync(string? flightNumber)
        {
            if (string.IsNullOrWhiteSpace(flightNumber))
            {
                _logger.LogWarning("Validation failed: FlightNumber is null or empty.");
                throw new ArgumentException("Flight number cannot be empty.");
            }
            else if (flightNumber.Length > 10)
            {
                _logger.LogWarning("Validation failed: FlightNumber '{FlightNumber}' is longer than 10 characters.", flightNumber);
                throw new ArgumentException("Flight number cannot be longer than ten characters.");
            }

            return _repository.GetFlightByNumberAsync(flightNumber);
        }

        public Task<IEnumerable<Flight>> GetFlightsByArrivalCityAndDateAsync(string? city, DateOnly date)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                _logger.LogWarning("Validation failed: City parameter for arrival search is null or empty.");
                throw new ArgumentException("City parameter cannot be empty.", nameof(city));
            }
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (date < today || date > today.AddDays(7))
            {
                _logger.LogWarning("Business rule violation: Requested date {Date} is out of the allowed 7-day range.", date);
                return Task.FromResult(Enumerable.Empty<Flight>());
            }

            return _repository.GetFlightsByArrivalCityAndDateAsync(city, date);
        }

        public Task<IEnumerable<Flight>> GetFlightsByDateAsync(DateOnly date)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (date < today || date > today.AddDays(7))
            {
                _logger.LogWarning("Business rule violation: Requested date {Date} is out of the allowed 7-day range.", date);
                return Task.FromResult(Enumerable.Empty<Flight>());
            }

            return _repository.GetFlightsByDateAsync(date);
        }



        public Task<IEnumerable<Flight>> GetFlightsByDepartureCityAndDateAsync(string? city, DateOnly date)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                _logger.LogWarning("Validation failed: City parameter for departure search is null or empty.");
                throw new ArgumentException("City parameter cannot be empty.", nameof(city));
            }
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (date < today || date > today.AddDays(7))
            {
                _logger.LogWarning("Business rule violation: Requested date {Date} is out of the allowed 7-day range.", date);
                return Task.FromResult(Enumerable.Empty<Flight>());
            }

            return _repository.GetFlightsByDepartureCityAndDateAsync(city, date);
        }
    }
}