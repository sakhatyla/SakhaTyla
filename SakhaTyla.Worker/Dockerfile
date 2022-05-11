FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.sln .
COPY SakhaTyla.Core/*.csproj ./SakhaTyla.Core/
COPY SakhaTyla.Data/*.csproj ./SakhaTyla.Data/
COPY SakhaTyla.Infrastructure/*.csproj ./SakhaTyla.Infrastructure/
COPY SakhaTyla.Worker/*.csproj ./SakhaTyla.Worker/

RUN dotnet restore SakhaTyla.Worker/SakhaTyla.Worker.csproj

COPY SakhaTyla.Core/. ./SakhaTyla.Core/
COPY SakhaTyla.Data/. ./SakhaTyla.Data/
COPY SakhaTyla.Infrastructure/. ./SakhaTyla.Infrastructure/
COPY SakhaTyla.Worker/. ./SakhaTyla.Worker/

RUN dotnet publish SakhaTyla.Worker/SakhaTyla.Worker.csproj -c Release -o ./out

FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "SakhaTyla.Worker.dll"]