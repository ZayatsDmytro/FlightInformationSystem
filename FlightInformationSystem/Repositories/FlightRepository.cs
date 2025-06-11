using FlightStorageService.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.WebSockets;

namespace FlightStorageService.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly string _connectionString;
        public FlightRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
        public async Task<Flight?> GetFlightByNumberAsync(string? flightNumber)
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

        public async Task<IEnumerable<Flight>> GetFlightsByArrivalCityAndDateAsync(string arrivalCity, DateOnly date)
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

        

        public async Task<IEnumerable<Flight>> GetFlightsByDateAsync(DateOnly date)
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

        public async Task<IEnumerable<Flight>> GetFlightsByDepartureCityAndDateAsync(string departureCity, DateOnly date)
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
