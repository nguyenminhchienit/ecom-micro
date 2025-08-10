FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/Inventory/Inventory.Grpc/Inventory.Grpc.csproj", "src/Services/Inventory/Inventory.Grpc/"]
COPY ["src/BuildingBlocks/Common.Logging/Common.Logging.csproj", "src/BuildingBlocks/Common.Logging/"]
COPY ["src/BuildingBlocks/Contract/Contract.csproj", "src/BuildingBlocks/Contract/"]
COPY ["src/BuildingBlocks/Infrastructure/Infrastructure.csproj", "src/BuildingBlocks/Infrastructure/"]
COPY ["src/BuildingBlocks/Shared/Shared.csproj", "src/BuildingBlocks/Shared/"]
RUN dotnet restore "src/Services/Inventory/Inventory.Grpc/Inventory.Grpc.csproj"
COPY . .

WORKDIR "/src/src/Services/Inventory/Inventory.Grpc"
RUN dotnet build "Inventory.Grpc.csproj" -c Release -o /app/build
 
FROM build AS publish
RUN dotnet publish "Inventory.Grpc.csproj" -c Release -o /app/publish
 
FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventory.Grpc.dll"]