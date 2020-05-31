FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NintendoParser/NintendoParser.csproj", "NintendoParser/"]
COPY ["Parsers.Core/Parsers.Core.csproj", "Parsers.Core/"]
COPY ["Parsers.Infrastructure/Parsers.Infrastructure.csproj", "Parsers.Infrastructure/"]
RUN dotnet restore "NintendoParser/NintendoParser.csproj"
COPY . .
WORKDIR "/src/NintendoParser"
RUN dotnet build "NintendoParser.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NintendoParser.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NintendoParser.dll"]