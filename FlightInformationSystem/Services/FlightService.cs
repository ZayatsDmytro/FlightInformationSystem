using FlightStorageService.Models;
using FlightStorageService.Repositories;

namespace FlightStorageService.Services
{
    public class FlightService:IFlightService
    {
        private readonly IFlightRepository _repository;

        public FlightService(IFlightRepository repository)
        {
            _repository = repository;
        }

        public Task<Flight?> GetFlightByNumberAsync(string flightNumber)
        {
            return string.IsNullOrWhiteSpace(flightNumber) ? 
                throw new ArgumentException("Flight number cannot be empty.", nameof(flightNumber)) : 
                _repository.GetFlightByNumberAsync(flightNumber);

        }

        public Task<IEnumerable<Flight>> GetFlightsByArrivalCityAndDateAsync(string city, DateOnly date)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("There is no flight to entered city.", city);
            }
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (date < today || date > today.AddDays(7))
            {
                return Task.FromResult(Enumerable.Empty<Flight>());
            }

            return _repository.GetFlightsByArrivalCityAndDateAsync(city, date);
        }

        public Task<IEnumerable<Flight>> GetFlightsByDateAsync(DateOnly date)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (date < today || date > today.AddDays(7))
            {
                return Task.FromResult(Enumerable.Empty<Flight>()); 
            }

            return _repository.GetFlightsByDateAsync(date);
        }

        public Task<IEnumerable<Flight>> GetFlightsByDepartureCityAndDateAsync(string city, DateOnly date)
        {
            if(string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("There is no flight from entered city.",city);
            }
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (date < today || date > today.AddDays(7))
            {
                return Task.FromResult(Enumerable.Empty<Flight>());
            }

            return _repository.GetFlightsByDepartureCityAndDateAsync(city, date);
        }
    }
}
