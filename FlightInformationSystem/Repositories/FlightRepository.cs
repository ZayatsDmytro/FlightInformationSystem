using FlightStorageService.Controllers;
using FlightStorageService.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.WebSockets;

namespace FlightStorageService.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<FlightRepository> _logger;

        public FlightRepository(IConfiguration configuration, ILogger<FlightRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            _logger = logger;
        }
        public async Task<Flight?> GetFlightByNumberAsync(string? flightNumber)
        {
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                await using var command = new SqlCommand("dbo.GetFlightByNumber", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@FlightNumber", flightNumber);

                await connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return MapReaderToFlight(reader);
                }
                return null;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "A database error occurred while executing GetFlightByNumber for {FlightNumber}.", flightNumber);
                throw;
            }
        }

        public async Task<IEnumerable<Flight>> GetFlightsByArrivalCityAndDateAsync(string arrivalCity, DateOnly date)
        {
            try
            {
                var flights = new List<Flight>();
                await using var connection = new SqlConnection(_connectionString);
                await using var command = new SqlCommand("dbo.GetFlightsByArrivalCityAndDate", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue(@"City", arrivalCity);
                command.Parameters.AddWithValue(@"Date", date);

                await connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    flights.Add(MapReaderToFlight(reader));
                }

                return flights;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "A database error occurred while executing GetFlightsByArrivalCityAndDate for {City} and {Date}.", arrivalCity, date);
                throw;
            }
        }

        public async Task<IEnumerable<Flight>> GetFlightsByDateAsync(DateOnly date)
        {
            try
            {
                var flights = new List<Flight>();

                await using var connection = new SqlConnection(_connectionString);
                await using var command = new SqlCommand("dbo.GetFlightsByDate", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Date", date);

                await connection.OpenAsync();

                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    flights.Add(MapReaderToFlight(reader));
                }

                return flights;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "A database error occurred while executing GetFlightsByDate for {Date}.", date);
                throw;
            }
        }

        public async Task<IEnumerable<Flight>> GetFlightsByDepartureCityAndDateAsync(string departureCity, DateOnly date)
        {
            try
            {
                var flights = new List<Flight>();
                await using var connection = new SqlConnection(_connectionString);
                await using var command = new SqlCommand("dbo.GetFlightsByDepartureCityAndDate", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue(@"City", departureCity);
                command.Parameters.AddWithValue(@"Date", date);

                await connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    flights.Add(MapReaderToFlight(reader));
                }

                return flights;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "A database error occurred while executing GetFlightsByDepartureCityAndDate for {City} and {Date}.", departureCity, date);
                throw;
            }
        }

        private Flight MapReaderToFlight(SqlDataReader reader)
        {
            return new Flight
            {
                FlightNumber = reader.GetString(reader.GetOrdinal("FlightNumber")),
                DepartureDateTime = reader.GetDateTime(reader.GetOrdinal("DepartureDateTime")),
                DepartureAirportCity = reader.GetString(reader.GetOrdinal("DepartureAirportCity")),
                ArrivalAirportCity = reader.GetString(reader.GetOrdinal("ArrivalAirportCity")),
                DurationMinutes = reader.GetInt32(reader.GetOrdinal("DurationMinutes"))
            };
        }
    }
}