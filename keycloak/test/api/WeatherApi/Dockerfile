# syntax=docker/dockerfile:1.7-labs

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS publish

COPY --parents test/api/WeatherApi/WeatherApi.csproj .
RUN dotnet restore test/api/WeatherApi/WeatherApi.csproj

COPY . .
RUN dotnet publish test/api/WeatherApi/WeatherApi.csproj -c Release -o /app/publish
FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
CMD dotnet WeatherApi.dll