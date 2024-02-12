# AeroAssist Ticketing System

AeroAssist is a simple ticketing system built using ASP.NET Core, Razor Pages, Swagger OpenAPI, and upcoming integration with Microsoft SQL Server database. The system provides basic functionalities for managing tickets, including viewing all tickets, viewing a single ticket, updating a ticket, and deleting a ticket.

## Table of Contents

- [Controllers](#controllers)
- [Services](#services)
- [Classes](#classes)
- [HTML Pages](#html-pages)
- [Getting Started](#getting-started)
- [Swagger Documentation](#swagger-documentation)

## Controllers

### IndexController

- Path: `AeroAssist.Controllers.IndexController`
- Action: `Index`
- Description: Controller for the home page.

### LoginController

- Path: `AeroAssist.Controllers.LoginController`
- Action: `Login`
- Description: Controller for the login page.

### TicketController

- Path: `AeroAssist.Controllers.TicketController`
- Actions:
    - `Ticket`: Returns the view for creating a new ticket.
    - `Get`: Retrieves all tickets.
    - `Get/{id}`: Retrieves a specific ticket by ID.
    - `Post`: Creates a new ticket.
    - `Put/{id}`: Updates an existing ticket.
    - `Delete/{id}`: Deletes a ticket by ID.

## Services

### ITicketService

- Path: `AeroAssist.Services.ITicketService`
- Description: Interface defining the operations for ticket management.

### TicketService

- Path: `AeroAssist.Services.TicketService`
- Description: Implementation of `ITicketService` with in-memory storage for tickets.

## Classes

### Ticket

- Path: `AeroAssist.Ticket`
- Description: Represents a ticket in the ticketing system. Includes properties and methods for ticket management.

### SystemConfig

- Path: `AeroAssist.SystemConfig`
- Description: Represents a system configuration in a one-to-many relationship with the `Ticket` class.

## HTML Pages

### AboutModel

- Path: `AeroAssist.Pages.AboutModel`
- Description: Razor page for the about section of the website.

### IndexModel

- Path: `AeroAssist.Pages.IndexModel`
- Description: Razor page for the home section of the website.

### LoginModel

- Path: `AeroAssist.Pages.LoginModel`
- Description: Razor page for the login section of the website.

### TicketModel

- Path: `AeroAssist.Pages.TicketModel`
- Description: Razor page for the ticket queue section of the website.

## Getting Started

To run the AeroAssist ticketing system, follow these steps:

1. Clone the repository.
2. Open the solution in Visual Studio or JetBrains Rider.
3. Configure Swagger OpenAPI if needed.
4. Run the application.

## Swagger Documentation

Swagger OpenAPI documentation is available at `/swagger`. Use it to explore and test the API endpoints.
