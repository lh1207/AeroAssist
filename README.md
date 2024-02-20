# AeroAssist Ticketing System

AeroAssist is a versatile ticketing system designed for efficient ticket management. Easily create, view, update, and 
delete tickets, with comprehensive API documentation provided by Swagger OpenAPI. Streamline your ticketing processes 
effortlessly.

## Table of Contents

- [Storyboard](#storyboard)
- [UML Diagram](#uml-diagram)
- [Controllers](#controllers)
- [Services](#services)
- [Classes](#classes)
- [HTML Pages](#html-pages)
- [Getting Started](#getting-started)
- [Swagger Documentation](#swagger-documentation)

## Storyboard
![image](https://github.com/lh1207/AeroAssist/assets/100445409/18926c52-fe08-4d06-9f79-7c43d018a098)
![image](https://github.com/lh1207/AeroAssist/assets/100445409/445446aa-09e8-4b4b-bf9c-cbacdcf33293)

## UML Diagram
![image](https://github.com/lh1207/AeroAssist/assets/100445409/816ec27f-fba0-4873-a001-3008dffe3b39)

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
