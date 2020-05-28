#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["GamesProvider/GamesProvider.csproj", "GamesProvider/"]
COPY ["DBAccess/DBAccess.csproj", "DBAccess/"]
COPY ["Models/Models.csproj", "Models/"]
COPY ["GamesProvider.Services/GamesProvider.Services.csproj", "GamesProvider.Services/"]
RUN dotnet restore "GamesProvider/GamesProvider.csproj"
COPY . .
WORKDIR "/src/GamesProvider"
RUN dotnet build "GamesProvider.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GamesProvider.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GamesProvider.dll"]