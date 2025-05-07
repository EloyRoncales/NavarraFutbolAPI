# Imagen base para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Imagen para construir la app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish "NavarraFutbolAPI.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
# Copiar el archivo BBDD.json al contenedor
COPY Data/BBDD.json ./Data/BBDD.json
ENTRYPOINT ["dotnet", "NavarraFutbolAPI.dll"]
