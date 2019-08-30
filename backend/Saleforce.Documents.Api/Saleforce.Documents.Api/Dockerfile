FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Saleforce.Documents.Api/Saleforce.Documents.Api.csproj", "Saleforce.Documents.Api/"]
RUN dotnet restore "Saleforce.Documents.Api/Saleforce.Documents.Api.csproj"
COPY . .
WORKDIR "/src/Saleforce.Documents.Api"
RUN dotnet build "Saleforce.Documents.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Saleforce.Documents.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Saleforce.Documents.Api.dll"]