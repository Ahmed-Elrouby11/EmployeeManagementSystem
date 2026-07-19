# ==========================
# Build Stage
# ==========================

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy csproj files
COPY EmployeeManagementSystem.API/EmployeeManagementSystem.API.csproj EmployeeManagementSystem.API/
COPY EmployeeManagementSystem.Application/EmployeeManagementSystem.Application.csproj EmployeeManagementSystem.Application/
COPY EmployeeManagementSystem.Domain/EmployeeManagementSystem.Domain.csproj EmployeeManagementSystem.Domain/
COPY EmployeeManagementSystem.Infrastructure/EmployeeManagementSystem.Infrastructure.csproj EmployeeManagementSystem.Infrastructure/
COPY EmployeeManagementSystem.Shared/EmployeeManagementSystem.Shared.csproj EmployeeManagementSystem.Shared/

# Restore packages
RUN dotnet restore EmployeeManagementSystem.API/EmployeeManagementSystem.API.csproj

# Copy source code
COPY . .

WORKDIR /src/EmployeeManagementSystem.API

# Publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ==========================
# Runtime Stage
# ==========================

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet","EmployeeManagementSystem.API.dll"]