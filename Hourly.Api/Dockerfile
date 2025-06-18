# ---------------------------
# Base image
# ---------------------------
    FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
    WORKDIR /app
    EXPOSE 8080
    EXPOSE 8081
    
    # ---------------------------
    # Build stage
    # ---------------------------
    FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
    ARG BUILD_CONFIGURATION=Release
    WORKDIR /src
    
    # Copy the .csproj and restore
    COPY ["Hourly.Api/Hourly.Api.csproj", "Hourly.Api/"]
    RUN dotnet restore "Hourly.Api/Hourly.Api.csproj"
    
    # Copy everything and build
    COPY . .
    WORKDIR "/src/Hourly.Api"
    RUN dotnet build "Hourly.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build
    
    # ---------------------------
    # Publish stage
    # ---------------------------
    FROM build AS publish
    ARG BUILD_CONFIGURATION=Release
    RUN dotnet publish "Hourly.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
    
    # ---------------------------
    # Final runtime image
    # ---------------------------
    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .
    
    ENTRYPOINT ["dotnet", "Hourly.Api.dll"]
    