#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["UplayParser/UplayParser.csproj", "UplayParser/"]
COPY ["Parsers.Core/Parsers.Core.csproj", "Parsers.Core/"]
COPY ["Parsers.Infrastructure/Parsers.Infrastructure.csproj", "Parsers.Infrastructure/"]
RUN dotnet restore "UplayParser/UplayParser.csproj"
COPY . .
WORKDIR "/src/UplayParser"
RUN dotnet build "UplayParser.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UplayParser.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UplayParser.dll"]