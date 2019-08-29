FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Saleforce.Permissions.Api/Saleforce.Permissions.Api.csproj", "Saleforce.Permissions.Api/"]
RUN dotnet restore "Saleforce.Permissions.Api/Saleforce.Permissions.Api.csproj"
COPY . .
WORKDIR "/src/Saleforce.Permissions.Api"
RUN dotnet build "Saleforce.Permissions.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Saleforce.Permissions.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Saleforce.Permissions.Api.dll"]