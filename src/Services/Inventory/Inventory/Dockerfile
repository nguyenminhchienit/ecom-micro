FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
 
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Inventory/Inventory/Inventory.API.csproj", "src/Services/Inventory/Inventory/"]
COPY ["src/BuildingBlocks/Common.Logging/Common.Logging.csproj", "src/BuildingBlocks/Common.Logging/"]
COPY ["src/BuildingBlocks/Contract/Contract.csproj", "src/BuildingBlocks/Contract/"]
COPY ["src/BuildingBlocks/Infrastructure/Infrastructure.csproj", "src/BuildingBlocks/Infrastructure/"]
COPY ["src/BuildingBlocks/Shared/Shared.csproj", "src/BuildingBlocks/Shared/"]
RUN dotnet restore "src/Services/Inventory/Inventory/Inventory.API.csproj"
COPY . .

WORKDIR "/src/src/Services/Inventory/Inventory"
RUN dotnet build "Inventory.API.csproj" -c Release -o /app/build
 
FROM build AS publish
RUN dotnet publish "Inventory.API.csproj" -c Release -o /app/publish
 
FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventory.API.dll"]