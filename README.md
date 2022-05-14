# ReservationApp

Requirements
1. Docker-Linux
2. .Net Core 6.0.5(SDK 6.0.300)

Installation

1. Open project directory and run "docker-compose up" command.
2. Run this command to install EF "dotnet tool install --global dotnet-ef"
3. Go to Infrastructure directory "cd Infrastructure"
4. Run following command to add migration "dotnet ef migrations add First -s ..\API"
5. Run following command to create database "dotnet ef database update -s ..\API"

NOTE: Use ../API for step 4 and 5 if you use linux or mac.