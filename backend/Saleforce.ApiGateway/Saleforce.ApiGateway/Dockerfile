FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Saleforce.ApiGateway/Saleforce.ApiGateway.csproj", "Saleforce.ApiGateway/"]
RUN dotnet restore "Saleforce.ApiGateway/Saleforce.ApiGateway.csproj"
COPY . .
WORKDIR "/src/Saleforce.ApiGateway"
RUN dotnet build "Saleforce.ApiGateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Saleforce.ApiGateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Saleforce.ApiGateway.dll"]