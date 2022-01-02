FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DatabaseTest/DatabaseTest.csproj", "DatabaseTest/"]
RUN dotnet restore "DatabaseTest/DatabaseTest.csproj"
COPY . .
WORKDIR "/src/DatabaseTest"
RUN dotnet build "DatabaseTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DatabaseTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DatabaseTest.dll"]
