using FlightClientApp.Models;
using FlightClientApp.Services; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlightClientApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFlightApiClient _apiClient;

        public IndexModel(IFlightApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [BindProperty(SupportsGet = true)]
        public string? FlightNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? City { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchType { get; set; }

        public IEnumerable<Flight> SearchResults { get; set; } = new List<Flight>();
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchType))
            {
                return;
            }

            try
            {
                switch (SearchType)
                {
                    case "byNumber":
                        await SearchByFlightNumber();
                        break;
                    case "byDate":
                    case "byDeparture":
                    case "byArrival":
                        await SearchByDateAndCity();
                        break;
                }
            }
            catch (HttpRequestException ex)
            {
                ErrorMessage = $"{ex.Message}";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred: {ex.Message}";
            }
        }

        private async Task SearchByFlightNumber()
        {
            if (string.IsNullOrWhiteSpace(FlightNumber))
            {
                ErrorMessage = "Flight number is required for this search.";
                return;
            }
            var flight = await _apiClient.GetFlightByNumberAsync(FlightNumber);
            if (flight != null)
            {
                SearchResults = new[] { flight };
            }
        }

        private async Task SearchByDateAndCity()
        {
            if (!DateOnly.TryParse(SearchDate, out var date))
            {
                ErrorMessage = "A valid date is required for this search.";
                return;
            }

            switch (SearchType)
            {
                case "byDate":
                    SearchResults = await _apiClient.GetFlightsByDateAsync(date);
                    break;
                case "byDeparture":
                    if (string.IsNullOrWhiteSpace(City))
                    {
                        ErrorMessage = "City is required for departure search.";
                        return;
                    }
                    SearchResults = await _apiClient.GetFlightsByDepartureAsync(City, date);
                    break;
                case "byArrival":
                    if (string.IsNullOrWhiteSpace(City))
                    {
                        ErrorMessage = "City is required for arrival search.";
                        return;
                    }
                    SearchResults = await _apiClient.GetFlightsByArrivalAsync(City, date);
                    break;
            }
        }
    }
}