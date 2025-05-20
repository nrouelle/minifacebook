FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copier tout le dossier src
COPY ./src ./src

# Restaurer les dépendances à partir de la solution
WORKDIR /src
RUN dotnet restore ./src/MiniFacebook.sln

# Publier le projet MiniFacebook.Api
RUN dotnet publish ./src/MiniFacebook.API/MiniFacebook.API.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MiniFacebook.API.dll"]
