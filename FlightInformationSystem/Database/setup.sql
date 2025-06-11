USE master;
GO

CREATE DATABASE FlightsDb;
GO

USE FlightsDb;
GO

BEGIN
    CREATE TABLE Flights (
        FlightNumber         NVARCHAR(10)  NOT NULL PRIMARY KEY,
        DepartureDateTime    DATETIME2(0)  NOT NULL,
        DepartureAirportCity NVARCHAR(100) NOT NULL,
        ArrivalAirportCity   NVARCHAR(100) NOT NULL,
        DurationMinutes      INT           NOT NULL
    );
END
GO

CREATE PROCEDURE AddFlight
	@FlightNumber NVARCHAR(10),
	@DepartureDateTime DATETIME2(0),
	@DepartureAirportCity NVARCHAR(100),
	@ArrivalAirportCity NVARCHAR(100),
	@DurationMinutes INT
AS 
BEGIN
IF (@DepartureDateTime >= SYSDATETIME() AND @DepartureDateTime < DATEADD(day, 8, CAST(SYSDATETIME() AS DATE)))
	BEGIN
		INSERT INTO Flights (
			FlightNumber,
			DepartureDateTime,
			DepartureAirportCity,
			ArrivalAirportCity,
			DurationMinutes)
		VALUES (
			@FlightNumber,
			@DepartureDateTime,
			@DepartureAirportCity,
			@ArrivalAirportCity,
			@DurationMinutes);
	END
ELSE PRINT('Incorrect date')
END
GO

CREATE PROCEDURE CleanUpOldFlights
AS
BEGIN
	DELETE FROM Flights
	WHERE DepartureDateTime < SYSDATETIME();
END
GO

CREATE PROCEDURE GetFlightsByArrivalCityAndDate
	@City nvarchar(100),
	@Date DATE
AS
BEGIN
	SELECT *
	FROM Flights
	WHERE ArrivalAirportCity = @City AND CAST(DepartureDateTime AS DATE) = @Date;
END
GO

CREATE PROCEDURE GetFlightsByDate
	@Date DATE
AS 
BEGIN
	SELECT * 
	FROM Flights
	WHERE CAST(DepartureDateTime AS DATE) = @Date;
END
GO

CREATE PROCEDURE GetFlightByNumber
	@FlightNumber nvarchar(10)
AS
BEGIN
	SELECT * 
	FROM Flights
	WHERE FlightNumber = @FlightNumber;
END
GO

CREATE PROCEDURE GetFlightsByDepartureCityAndDate
	@City nvarchar(100),
	@Date DATE
AS
BEGIN
	SELECT *
	FROM Flights
	WHERE DepartureAirportCity = @City AND CAST(DepartureDateTime AS DATE) = @Date;
END
GO