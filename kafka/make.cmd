@echo off
setlocal enabledelayedexpansion
title Project Runner

if "%1"=="" (
    echo Usage: %0 ^<command^>
    echo.
    echo Available commands: run
    exit /b 1
)

set CMD=%1

if /i "%CMD%"=="run" goto run

echo Invalid command: %CMD%
exit /b 1

:run
echo (1) server
echo (2) producer
echo (3) producer-api
echo (4) consumer
set /p APP=Please select 1-4:

if /i "%APP%"=="1" goto server
if /i "%APP%"=="2" goto producer
if /i "%APP%"=="3" goto producer-api
if /i "%APP%"=="4" goto consumer

:server
docker compose -p learn-devops up --build
goto end

:producer
cd src\Producer
dotnet run
cd ..\..
goto end

:producer-api
cd src\ProducerApi
dotnet run
cd ..\..
goto end

:consumer
cd src\Consumer
dotnet run
cd ..\..
goto end

:end
echo End
