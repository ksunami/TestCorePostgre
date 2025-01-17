# Fase de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MarcasAutosAPI/MarcasAutosAPI.csproj", "MarcasAutosAPI/"]
RUN dotnet restore "MarcasAutosAPI/MarcasAutosAPI.csproj"
COPY . .
WORKDIR "/src/MarcasAutosAPI"
RUN dotnet publish "MarcasAutosAPI.csproj" -c Release -o /app/publish

# Fase de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MarcasAutosAPI.dll"]
