FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.sln .
COPY SakhaTyla.Core/*.csproj ./SakhaTyla.Core/
COPY SakhaTyla.Data/*.csproj ./SakhaTyla.Data/
COPY SakhaTyla.Infrastructure/*.csproj ./SakhaTyla.Infrastructure/
COPY SakhaTyla.Web/*.csproj ./SakhaTyla.Web/

RUN dotnet restore SakhaTyla.Web/SakhaTyla.Web.csproj

COPY SakhaTyla.Core/. ./SakhaTyla.Core/
COPY SakhaTyla.Data/. ./SakhaTyla.Data/
COPY SakhaTyla.Infrastructure/. ./SakhaTyla.Infrastructure/
COPY SakhaTyla.Web/. ./SakhaTyla.Web/

RUN dotnet publish SakhaTyla.Web/SakhaTyla.Web.csproj -c Release -o ./out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "SakhaTyla.Web.dll"]