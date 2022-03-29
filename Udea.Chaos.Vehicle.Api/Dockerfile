#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Udea.Chaos.Vehicle.Api/Udea.Chaos.Vehicle.Api.csproj", "Udea.Chaos.Vehicle.Api/"]
COPY ["Udea.Chaos.Vehicle.Infrastructure/Udea.Chaos.Vehicle.Infrastructure.csproj", "Udea.Chaos.Vehicle.Infrastructure/"]
COPY ["Udea.Chaos.Vehicle.Domain/Udea.Chaos.Vehicle.Domain.csproj", "Udea.Chaos.Vehicle.Domain/"]
COPY ["Udea.Chaos.Vehicle.Application/Udea.Chaos.Vehicle.Application.csproj", "Udea.Chaos.Vehicle.Application/"]
RUN dotnet restore "Udea.Chaos.Vehicle.Api/Udea.Chaos.Vehicle.Api.csproj"
COPY . .
WORKDIR "/src/Udea.Chaos.Vehicle.Api"
RUN dotnet build "Udea.Chaos.Vehicle.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Udea.Chaos.Vehicle.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Udea.Chaos.Vehicle.Api.dll"]