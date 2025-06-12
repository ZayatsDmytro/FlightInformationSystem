FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["FlightInformationSystem/FlightStorageService.csproj", "FlightInformationSystem/"]
COPY ["FlightClientApp/FlightClientApp.csproj", "FlightClientApp/"]
RUN dotnet restore "FlightClientApp/FlightClientApp.csproj"

COPY . .
WORKDIR "/src/FlightClientApp"
RUN dotnet publish "FlightClientApp.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "FlightClientApp.dll"]