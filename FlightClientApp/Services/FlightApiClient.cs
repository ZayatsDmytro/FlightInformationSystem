using FlightClientApp.Models;
using System.Net;
using System.Text.Json;

namespace FlightClientApp.Services
{
    public class FlightApiClient : IFlightApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public FlightApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task HandleErrors(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                try
                {
                    var errorObject = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent, _jsonOptions);
                    if (errorObject != null && errorObject.TryGetValue("message", out var message))
                    {
                        throw new HttpRequestException(message, null, response.StatusCode);
                    }
                }
                catch (JsonException) { }

                response.EnsureSuccessStatusCode();
            }
        }


        public async Task<Flight?> GetFlightByNumberAsync(string flightNumber)
        {
            var response = await _httpClient.GetAsync($"api/flights/{flightNumber}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            await HandleErrors(response);

            return await response.Content.ReadFromJsonAsync<Flight>(_jsonOptions);
        }

        public async Task<IEnumerable<Flight>> GetFlightsByDateAsync(DateOnly date)
        {
            var dateString = date.ToString("yyyy-MM-dd");
            var response = await _httpClient.GetAsync($"api/flights?date={dateString}");

            await HandleErrors(response);

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Flight>>(_jsonOptions);
            return result ?? Enumerable.Empty<Flight>();
        }

        public async Task<IEnumerable<Flight>> GetFlightsByDepartureAsync(string city, DateOnly date)
        {
            var dateString = date.ToString("yyyy-MM-dd");
            var response = await _httpClient.GetAsync($"api/flights/departure?city={city}&date={dateString}");

            await HandleErrors(response);

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Flight>>(_jsonOptions);
            return result ?? Enumerable.Empty<Flight>();
        }

        public async Task<IEnumerable<Flight>> GetFlightsByArrivalAsync(string city, DateOnly date)
        {
            var dateString = date.ToString("yyyy-MM-dd");
            var response = await _httpClient.GetAsync($"api/flights/arrival?city={city}&date={dateString}");

            await HandleErrors(response);

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Flight>>(_jsonOptions);
            return result ?? Enumerable.Empty<Flight>();
        }
    }
}
