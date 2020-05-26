#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["GameSalesApi/GameSalesApi.csproj", "GameSalesApi/"]
COPY ["Model/Model.csproj", "Model/"]
COPY ["ApplicationBlocks/ApplicationBlocks.csproj", "ApplicationBlocks/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["DependencyResolver/DependencyResolver.csproj", "DependencyResolver/"]
RUN dotnet restore "GameSalesApi/GameSalesApi.csproj"
COPY . .
WORKDIR "/src/GameSalesApi"
RUN dotnet build "GameSalesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameSalesApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["/configs/GameSalesApiSettings.json", "appsettings.json"]
ENTRYPOINT ["dotnet", "GameSalesApi.dll"]