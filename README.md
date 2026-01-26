# AeroAssist Ticketing System

AeroAssist is a versatile ticketing system designed for efficient ticket management.
- Easily create, view, update, and delete tickets
- Comprehensive API documentation
- Streamline ticket management and organization

## Table of Contents

- [Screenshots](#screenshots)
- [Quick Start with Docker](#quick-start-with-docker)
- [Manual Setup](#manual-setup)
- [Configuration](#configuration)
- [API Documentation](#api-documentation)

## Screenshots
![features](https://github.com/user-attachments/assets/34687b06-eefe-477c-8b60-4b3dc3abcbb7)
![app](https://github.com/user-attachments/assets/6328701c-554f-4f8b-91c8-d0bb97053bc4)

## Quick Start with Docker

The fastest way to run AeroAssist is with Docker. This works on Linux, Windows (Docker Desktop), and macOS.

### Prerequisites

- [Docker](https://docs.docker.com/get-docker/) and Docker Compose installed

### Run with Docker Compose

1. Clone the repository:
   ```bash
   git clone https://github.com/lh1207/AeroAssist.git
   cd AeroAssist
   ```

2. Start the application:
   ```bash
   docker compose up -d
   ```

3. Access the application at http://localhost:8080

4. View API documentation at http://localhost:8080/swagger

### Run with Docker only

If you have an external SQL Server:

1. Build the image:
   ```bash
   docker build -t aeroassist .
   ```

2. Run the container:
   ```bash
   docker run -p 8080:8080 \
     -e "ConnectionStrings__DefaultConnection=Server=your-server;Database=AeroAssist;User Id=sa;Password=YourPassword;TrustServerCertificate=True" \
     -e "Database__AutoMigrate=true" \
     aeroassist
   ```

### Stop the Application

```bash
docker compose down
```

To also remove the database volume:
```bash
docker compose down -v
```

## Manual Setup

![getting-started](https://github.com/user-attachments/assets/36c1c12c-637e-46fa-bb9d-9c49a1eabee4)

For local development without Docker:

1. Clone the repository
2. Open the solution in Visual Studio or JetBrains Rider
3. Restore NuGet packages
4. Build the solution
5. Create a database called `AeroAssist` in SQL Server
6. Install Entity Framework Core tools:
   ```powershell
   dotnet tool install --global dotnet-ef
   ```
7. Apply migrations:
   ```powershell
   dotnet ef database update
   ```
8. Configure the connection string in `appsettings.json`
9. Run the application

## Configuration

### Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `ConnectionStrings__DefaultConnection` | SQL Server connection string | LocalDB |
| `Database__AutoMigrate` | Apply migrations on startup | `false` |
| `Swagger__Enabled` | Enable Swagger in production | `false` |
| `ReverseProxy__Enabled` | Disable HTTPS redirect (use behind proxy) | `false` |
| `HttpClient__BaseAddress` | Internal API base URL | `https://localhost:7223/` |
| `Authentication__Microsoft__ClientId` | Microsoft OAuth Client ID | (optional) |
| `Authentication__Microsoft__ClientSecret` | Microsoft OAuth Client Secret | (optional) |

### Docker Compose Environment

Create a `.env` file based on `.env.example`:
```bash
cp .env.example .env
```

Edit `.env` to set your SQL Server password:
```
SA_PASSWORD=YourSecurePassword123!
```

## API Documentation

![api](https://github.com/user-attachments/assets/1eeb8358-821b-444f-90d2-38b3c85e3146)

OpenAPI documentation is available at `/swagger`. Use it to explore and test the API endpoints.

## Tech Stack

- ASP.NET Core 8.0
- Entity Framework Core 8.0
- SQL Server
- Bootstrap 5
- Chart.js
