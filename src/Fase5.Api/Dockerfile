# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Fase5.Api/Fase5.Api.csproj", "Fase5.Api/"]
COPY ["Fase5.Application/Fase5.Application.csproj", "Fase5.Application/"]
COPY ["Fase5.Domain/Fase5.Domain.csproj", "Fase5.Domain/"]
COPY ["Fase5.Domain.Core/Fase5.Domain.Core.csproj", "Fase5.Domain.Core/"]
COPY ["Fase5.Infra.Security/Fase5.Infra.Security.csproj", "Fase5.Infra.Security/"]
COPY ["Fase5.Infra.Data/Fase5.Infra.Data.csproj", "Fase5.Infra.Data/"]
RUN dotnet restore "./Fase5.Api/Fase5.Api.csproj"
COPY . .
WORKDIR "/src/Fase5.Api"
RUN dotnet build "./Fase5.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fase5.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fase5.Api.dll"]