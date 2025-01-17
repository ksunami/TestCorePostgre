FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MarcasAutosAPI/MarcasAutosAPI.csproj", "MarcasAutosAPI/"]
RUN dotnet restore "MarcasAutosAPI/MarcasAutosAPI.csproj"
COPY . .
WORKDIR "/src/MarcasAutosAPI"
RUN dotnet build "MarcasAutosAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarcasAutosAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarcasAutosAPI.dll"]
