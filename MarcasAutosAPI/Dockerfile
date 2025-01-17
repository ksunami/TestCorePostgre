# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MarcasAutosAPI.csproj", "./"]
RUN dotnet restore "MarcasAutosAPI.csproj"
COPY . .
WORKDIR /src
RUN dotnet publish "MarcasAutosAPI.csproj" -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MarcasAutosAPI.dll"]
