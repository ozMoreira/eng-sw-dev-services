# Stage 1: Base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8082
EXPOSE 8083

# Stage 2: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file from the build context root to the WORKDIR (/src)
# Since the context is the project folder, the .csproj is at the root.
COPY ["orders-microservice.csproj", "."]

# Restore dependencies for the project
RUN dotnet restore "./orders-microservice.csproj"

# Copy the rest of the source code from the build context root to the WORKDIR
# This copies Program.cs, Controllers/, etc.
COPY . .

# Build the project
RUN dotnet build "./orders-microservice.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./orders-microservice.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "orders-microservice.dll"]