﻿namespace FlightClientApp.Models
{
    public class Flight
    {
        public string FlightNumber { get; set; } 
        public DateTime DepartureDateTime { get; set; }
        public string DepartureAirportCity { get; set; } 
        public string ArrivalAirportCity { get; set; } 
        public int DurationMinutes { get; set; }
    }
}
