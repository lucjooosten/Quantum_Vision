# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Quantum_Vision.Api/Quantum_Vision.Api.csproj", "Quantum_Vision.Api/"]
COPY ["Quantum_Vision.Application/Quantum_Vision.Application.csproj", "Quantum_Vision.Application/"]
COPY ["Quantum_Vision.Domain/Quantum_Vision.Domain.csproj", "Quantum_Vision.Domain/"]
COPY ["Quantum_Vision.Infrastructure/Quantum_Vision.Infrastructure.csproj", "Quantum_Vision.Infrastructure/"]
RUN dotnet restore "./Quantum_Vision.Api/Quantum_Vision.Api.csproj"
COPY . .
WORKDIR "/src/Quantum_Vision.Api"
RUN dotnet build "./Quantum_Vision.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Quantum_Vision.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Quantum_Vision.Api.dll"]