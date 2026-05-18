FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.sln .

COPY WorkBoard.API/*.csproj WorkBoard.API/
COPY WorkBoard.Application/*.csproj WorkBoard.Application/
COPY WorkBoard.Domain/*.csproj WorkBoard.Domain/
COPY WorkBoard.Infrastructure/*.csproj WorkBoard.Infrastructure/

RUN dotnet restore

COPY . .

RUN dotnet publish WorkBoard.API/WorkBoard.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "WorkBoard.API.dll"]