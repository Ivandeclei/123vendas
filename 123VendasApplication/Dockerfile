# Use the ASP.NET runtime image as the base for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project files for all projects in the solution
COPY ["123VendasApplication/123VendasApplication.csproj", "123VendasApplication/"]
COPY ["123Vendas.Application/123Vendas.Application.csproj", "123Vendas.Application/"]
COPY ["123Vendas.DbAdapter/123Vendas.DbAdapter.csproj", "123Vendas.DbAdapter/"]
COPY ["123Vendas.Domain/123Vendas.Domain.csproj", "123Vendas.Domain/"]
COPY ["PublishQueueAdapter/PublishQueueAdapter.csproj", "PublishQueueAdapter/"]

# Restore dependencies for all projects
RUN dotnet restore "./123VendasApplication/123VendasApplication.csproj"

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR "/src/123VendasApplication"
RUN dotnet build "./123VendasApplication.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "./123VendasApplication.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "123VendasApplication.dll"]
