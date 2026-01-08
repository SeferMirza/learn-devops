# syntax=docker/dockerfile:1.7-labs

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS publish

COPY --parents weather-api/WeatherApi.csproj .
RUN dotnet restore weather-api/WeatherApi.csproj

COPY . .
RUN dotnet publish weather-api/WeatherApi.csproj -c Release -o /app/publish
FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
CMD dotnet WeatherApi.dll