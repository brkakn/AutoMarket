#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AutoMarket.Api/AutoMarket.Api.csproj", "AutoMarket.Api/"]
RUN dotnet restore "AutoMarket.Api/AutoMarket.Api.csproj"
COPY . .
WORKDIR "/src/AutoMarket.Api"
RUN dotnet build "AutoMarket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutoMarket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoMarket.Api.dll"]