FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.sln .
COPY SakhaTyla.Core/*.csproj ./SakhaTyla.Core/
COPY SakhaTyla.Data/*.csproj ./SakhaTyla.Data/
COPY SakhaTyla.Infrastructure/*.csproj ./SakhaTyla.Infrastructure/
COPY SakhaTyla.Web.Common/*.csproj ./SakhaTyla.Web.Common/
COPY SakhaTyla.Web.Front/*.csproj ./SakhaTyla.Web.Front/

RUN dotnet restore SakhaTyla.Web/SakhaTyla.Web.csproj

COPY SakhaTyla.Core/. ./SakhaTyla.Core/
COPY SakhaTyla.Data/. ./SakhaTyla.Data/
COPY SakhaTyla.Infrastructure/. ./SakhaTyla.Infrastructure/
COPY SakhaTyla.Web.Common/. ./SakhaTyla.Web.Common/
COPY SakhaTyla.Web.Front/. ./SakhaTyla.Web.Front/

RUN dotnet publish SakhaTyla.Web.Front/SakhaTyla.Web.Front.csproj -c Release -o ./out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "SakhaTyla.Web.Front.dll"]