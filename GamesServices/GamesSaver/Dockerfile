#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["GamesSaver/GamesSaver.csproj", "GamesSaver/"]
COPY ["DBAccess/DBAccess.csproj", "DBAccess/"]
COPY ["Models/Models.csproj", "Models/"]
COPY ["ServerBackgroundServices/ServerBackgroundServices.csproj", "ServerBackgroundServices/"]
COPY ["GamesSaver.Services/GamesSaver.Services.csproj", "GamesSaver.Services/"]
RUN dotnet restore "GamesSaver/GamesSaver.csproj"
COPY . .
WORKDIR "/src/GamesSaver"
RUN dotnet build "GamesSaver.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GamesSaver.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GamesSaver.dll"]