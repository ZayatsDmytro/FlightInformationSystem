using FlightStorageService.Models;

namespace FlightStorageService.Repositories
{
    public interface IFlightRepository
    {
        Task<Flight?> GetFlightByNumberAsync(string flightNumber);
        Task<IEnumerable<Flight>> GetFlightsByDateAsync(DateOnly date);
        Task<IEnumerable<Flight>> GetFlightsByDepartureCityAndDateAsync(string departureCity, DateOnly date);
        Task<IEnumerable<Flight>> GetFlightsByArrivalCityAndDateAsync(string arrivalCity, DateOnly date);
    }
}
