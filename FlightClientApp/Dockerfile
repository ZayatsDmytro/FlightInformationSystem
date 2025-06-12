# --- Етап 1: Збірка ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копіюємо .csproj файли
COPY ["FlightInformationSystem/FlightStorageService.csproj", "FlightInformationSystem/"]
COPY ["FlightClientApp/FlightClientApp.csproj", "FlightClientApp/"]
RUN dotnet restore "FlightClientApp/FlightClientApp.csproj"

# Копіюємо весь інший код
COPY . .
WORKDIR "/src/FlightClientApp"
RUN dotnet publish "FlightClientApp.csproj" -c Release -o /app/publish

# --- Етап 2: Фінальний образ ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "FlightClientApp.dll"]