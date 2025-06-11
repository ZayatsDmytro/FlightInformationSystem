using FlightClientApp.Models;

namespace FlightClientApp.Services
{
    public interface IFlightApiClient
    {
        Task<Flight?> GetFlightByNumberAsync(string flightNumber);
        Task<IEnumerable<Flight>> GetFlightsByDateAsync(DateOnly date);
        Task<IEnumerable<Flight>> GetFlightsByDepartureAsync(string? city, DateOnly date);
        Task<IEnumerable<Flight>> GetFlightsByArrivalAsync(string? city, DateOnly date);
    }
}
