using FlightStorageService.Models;
using FlightStorageService.Repositories;

namespace FlightStorageService.Services
{
    public interface IFlightService
    {
        Task<Flight?> GetFlightByNumberAsync(string flightNumber);
        Task<IEnumerable<Flight>> GetFlightsByDateAsync(DateOnly date);
        Task<IEnumerable<Flight>> GetFlightsByDepartureCityAndDateAsync(string city, DateOnly date);
        Task<IEnumerable<Flight>> GetFlightsByArrivalCityAndDateAsync(string city, DateOnly date);
    }
}
