# AeroAssist Ticketing System

AeroAssist is a versatile ticketing system designed for efficient ticket management. 
- Easily create, view, update, and delete tickets, 
- Comprehensive API documentation. 
- Streamline ticket management and organization.

## Table of Contents

- [Screenshots](#screenshots)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)

## Screenshots
![features](https://github.com/user-attachments/assets/34687b06-eefe-477c-8b60-4b3dc3abcbb7)
![app](https://github.com/user-attachments/assets/6328701c-554f-4f8b-91c8-d0bb97053bc4)

## Getting Started

![getting-started](https://github.com/user-attachments/assets/36c1c12c-637e-46fa-bb9d-9c49a1eabee4)

To run the AeroAssist ticketing system, follow these steps:

1. Clone the repository.
2. Open the solution in Visual Studio or JetBrains Rider.
3. Restore the NuGet packages.
4. Build the solution.
5. Create a new database called `AeroAssist` in SQL Server.
6. Install the Entity Framework Core tools by running the following command in the Package Manager Console:
   ```powershell
   Install-Package Microsoft.EntityFrameworkCore.Tools
   ```
   Alternatively, you can install the tools globally by running the following command:
   ```powershell
    dotnet tool install --global dotnet-ef
    ```
7. Apply the Entity Framework migrations to the database by running the following command in the Package Manager Console:
   ```powershell
   Update-Database
   ```
8. Configure the connection string in the `appsettings.json` file.
9. Run the application.

## API Documentation

![api](https://github.com/user-attachments/assets/1eeb8358-821b-444f-90d2-38b3c85e3146)

OpenAPI documentation is available at `/swagger`. Use it to explore and test the API endpoints.
