using FlightClientApp.Models;

namespace FlightClientApp.Services
{
    public class FlightApiClient : IFlightApiClient
    {
        private readonly HttpClient _httpClient;

        public FlightApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Flight?> GetFlightByNumberAsync(string? flightNumber)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Flight>($"api/flights/{flightNumber}");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Flight>> GetFlightsByArrivalCityAndDateAsync(string? city, DateOnly date)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Flight>> GetFlightsByDateAsync(DateOnly date)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Flight>> GetFlightsByDepartureCityAndDateAsync(string? city, DateOnly date)
        {
            throw new NotImplementedException();
        }
    }
}
