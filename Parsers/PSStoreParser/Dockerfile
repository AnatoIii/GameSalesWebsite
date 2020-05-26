#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["PSStoreParser/PSStoreParser.csproj", "PSStoreParser/"]
COPY ["Parsers.Core/Parsers.Core.csproj", "Parsers.Core/"]
COPY ["Parsers.Infrastructure/Parsers.Infrastructure.csproj", "Parsers.Infrastructure/"]
RUN dotnet restore "PSStoreParser/PSStoreParser.csproj"
COPY . .
WORKDIR "/src/PSStoreParser"
RUN dotnet build "PSStoreParser.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PSStoreParser.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PSStoreParser.dll"]