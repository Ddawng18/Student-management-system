# Phase 3 - Backend setup completed

## What was set up

- Clean architecture backend scaffold under src
- EF Core SQL Server configured
- DbContext and entity mapping configured
- Initial migration generated with dotnet-ef
- API startup configured with CORS, Swagger, health endpoint

## Project structure

- src/StudentManagement.Api
- src/StudentManagement.Application
- src/StudentManagement.Domain
- src/StudentManagement.Infrastructure

## Key commands used

1. Build API project graph:

   dotnet build src/StudentManagement.Api/StudentManagement.Api.csproj

2. Create migration (already done):

   dotnet tool run dotnet-ef migrations add InitialCreate --project src/StudentManagement.Infrastructure/StudentManagement.Infrastructure.csproj --startup-project src/StudentManagement.Api/StudentManagement.Api.csproj --context AppDbContext --output-dir Persistence/Migrations

3. Apply migration to database:

   dotnet tool run dotnet-ef database update --project src/StudentManagement.Infrastructure/StudentManagement.Infrastructure.csproj --startup-project src/StudentManagement.Api/StudentManagement.Api.csproj --context AppDbContext

4. Run API:

   dotnet run --project src/StudentManagement.Api/StudentManagement.Api.csproj

## Environment notes

- appsettings.Development.json currently points to LocalDB for convenience.
- On macOS, LocalDB is not available. Replace Development connection string with SQL Server container or remote SQL Server connection.

## Verify endpoints

- GET /health
- GET /swagger
