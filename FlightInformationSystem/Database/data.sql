DECLARE @Today DATETIME2 = CAST(GETDATE() AS DATE);
DECLARE @Tomorrow DATETIME2 = DATEADD(day, 1, @Today);
DECLARE @DayAfterTomorrow DATETIME2 = DATEADD(day, 2, @Today);
DECLARE @ThreeDaysLater DATETIME2 = DATEADD(day, 3, @Today);
DECLARE @FiveDaysLater DATETIME2 = DATEADD(day, 5, @Today);

INSERT INTO dbo.Flights (FlightNumber, DepartureDateTime, DepartureAirportCity, ArrivalAirportCity, DurationMinutes)
VALUES
    ('PS101', DATEADD(hour, 10, @Today), 'Kyiv', 'Lviv', 60),
    ('LH2545', DATEADD(hour, 15, @Today), 'Kyiv', 'Munich', 150),

    ('W6123', DATEADD(hour, 8, @Tomorrow), 'Lviv', 'Warsaw', 75),
    ('TK458', DATEADD(hour, 18, @Tomorrow), 'Kyiv', 'Istanbul', 125),

    ('PS102', DATEADD(hour, 11, @DayAfterTomorrow), 'Lviv', 'Kyiv', 60),
    ('FR3012', DATEADD(hour, 6, @DayAfterTomorrow), 'Kyiv', 'London', 210),

    ('KLM987', DATEADD(hour, 14, @ThreeDaysLater), 'Kyiv', 'Amsterdam', 180),
    ('AAL789', DATEADD(hour, 20, @ThreeDaysLater), 'Warsaw', 'Chicago', 540),

    ('AF1653', DATEADD(hour, 9, @FiveDaysLater), 'Kyiv', 'Paris', 190),
    ('BA833', DATEADD(hour, 12, @FiveDaysLater), 'Lviv', 'London', 195);
GO