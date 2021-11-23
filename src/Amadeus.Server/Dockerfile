FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Amadeus.Server/Amadeus.Server.csproj", "Amadeus.Server/"]
RUN dotnet restore "src/Amadeus.Server/Amadeus.Server.csproj"
COPY . .
WORKDIR "/src/Amadeus.Server"
RUN dotnet build "Amadeus.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Amadeus.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Amadeus.Server.dll"]
