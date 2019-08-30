FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Saleforce.Customers.Api/Saleforce.Customers.Api.csproj", "Saleforce.Customers.Api/"]
RUN dotnet restore "Saleforce.Customers.Api/Saleforce.Customers.Api.csproj"
COPY . .
WORKDIR "/src/Saleforce.Customers.Api"
RUN dotnet build "Saleforce.Customers.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Saleforce.Customers.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Saleforce.Customers.Api.dll"]