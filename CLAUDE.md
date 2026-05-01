# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Restore dependencies
dotnet restore

# Build
dotnet build

# Run (development)
dotnet run

# Run tests
dotnet test --verbosity normal

# Apply EF Core migrations to the database
dotnet ef database update

# Create a new migration after model changes
dotnet ef migrations add <MigrationName>
```

The app has two launch profiles in `Properties/launchSettings.json`:
- `https-website` — serves the UI at `https://localhost:5001`
- `https-api` — serves with Swagger at `https://localhost:7223/swagger`

## Architecture

AeroAssist is a hybrid ASP.NET Core 8.0 app that exposes both a **REST API** (via `[ApiController]` controllers) and a **Razor Pages UI** that consumes that same API internally via `HttpClient`.

### Request Flow

```
Browser → Razor Page (PageModel) → HttpClient → REST API Controller → TicketService → EF Core → SQL Server
```

The Razor Pages do **not** call `TicketService` directly. `TicketModel` in `Data/Models/TicketModel.cs` serializes form data and issues HTTP requests to the internal API (configured via `HttpClient:BaseAddress`). In Docker the base address is set to `http://aeroassist:8080/`; locally it defaults to `https://localhost:7223/`.

### Key Components

- **`Ticket.cs`** — Core domain entity; maps directly to the `Tickets` SQL table via EF Core.
- **`Services/TicketService.cs`** — Implements `ITicketService` (interface is nested inside the class). Registered as scoped in `Program.cs`. All DB writes call `SaveChanges()` synchronously.
- **`Data/AeroAssistContext.cs`** — `IdentityDbContext<IdentityUser>`, holds the `Tickets` DbSet and ASP.NET Identity tables.
- **`Controllers/TicketController.cs`** — REST API at `api/Ticket`. Handles GET (all + by-id), POST, PUT, DELETE.
- **`Controllers/GraphController.cs`** — Serves graph data by grouping tickets by `Status`; used by Chart.js in `wwwroot/js/graphs/`.
- **`Data/Models/TicketModel.cs`** — Razor PageModel shared by ticket-related pages. Handles HTML form method spoofing: PUT and DELETE are tunneled as POST with a `_method` hidden field checked in `OnPost()`.

### Form Method Spoofing

HTML forms only support GET/POST. Edit/delete actions post with a hidden `_method=put` or `_method=delete` field plus an `id` field. `TicketModel.OnPost()` reads these and dispatches to `OnPut()` or `OnDelete()`.

### Configuration & Environment Variables

| Key | Purpose | Default |
|-----|---------|---------|
| `ConnectionStrings__DefaultConnection` | SQL Server connection string | LocalDB |
| `Database__AutoMigrate` | Run pending EF migrations on startup | `false` |
| `Swagger__Enabled` | Enable Swagger outside Development | `false` |
| `ReverseProxy__Enabled` | Skip HTTPS redirect and HSTS (use behind proxy) | `false` |
| `HttpClient__BaseAddress` | Base URL for internal API calls from Razor Pages | `https://localhost:7223/` |
| `HttpClient__AllowInsecureCertificates` | Skip TLS validation on the internal HttpClient | `false` |
| `Authentication__Microsoft__ClientId/Secret` | Enables optional Microsoft OAuth login | (unset = disabled) |
| `Cors__Origins` | Array of allowed CORS origins | localhost variants |

### Database & Migrations

EF Core targets SQL Server. Migrations live in `Migrations/`. The `Database:AutoMigrate` flag (used in Docker) triggers `db.Database.Migrate()` at startup. For local dev, run `dotnet ef database update` manually after pulling new migrations.

### Docker

`docker-compose.yml` starts the app container plus an SQL Server 2022 container on the same bridge network. Copy `.env.example` → `.env` and set `SA_PASSWORD` before running `docker compose up -d`. The app is available at `http://localhost:8080` and Swagger at `http://localhost:8080/swagger`.
