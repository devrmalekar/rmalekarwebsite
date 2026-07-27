# Portfolio Web Application

A modern developer portfolio solution built with ASP.NET Core, Blazor WebAssembly, Entity Framework Core, and SQLite. The application serves portfolio data such as personal details, certifications, experience, skills, projects, and qualifications through a REST API and displays it in a polished client interface.

## Key Features

- RESTful API endpoints for portfolio data
- Automatic database migration and seed data initialization
- EF Core + SQLite persistence for a lightweight local database setup
- Blazor WebAssembly front end with responsive portfolio pages
- Structured logging, response compression, CORS, and rate limiting

## Project Architecture

The solution is organized into three layers:

- Presentation layer: Portfolio.ClassicMode
  - A Blazor WebAssembly client that renders the portfolio UI.
- Application/API layer: Portfolio.Api
  - An ASP.NET Core Web API that exposes portfolio endpoints and handles business logic.
- Shared layer: Portfolio.Shared
  - Contains shared DTOs and common contracts used by the API and client.

The API also uses EF Core with a SQLite database and seeds demo data automatically when the application starts.

## System Architecture

```text
User Browser
    │
    ▼
Blazor WebAssembly Client
    │
    │ HTTP / HTTPS
    ▼
ASP.NET Core Web API
    │
    ▼
Entity Framework Core
    │
    ▼
SQLite Database (portfolio.db)
```

Additional cross-cutting features include:

- Serilog for request and application logging
- Middleware for exception handling and response capture
- Rate limiting for API protection
- Gzip response compression for faster delivery
- Caching in both the API and the Blazor WebAssembly client, with a 2-minute cache duration for frequently requested portfolio data

## Project Structure

```text
Portfolio/
├── Portfolio.Api/
│   ├── Controllers/
│   ├── Context/
│   ├── Data/
│   ├── Helpers/
│   ├── Middleware/
│   ├── Migrations/
│   ├── Services/
│   ├── Properties/
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── Program.cs
├── Portfolio.ClassicMode/
│   ├── Components/
│   ├── Configuration/
│   ├── Layout/
│   ├── Pages/
│   ├── Services/
│   ├── wwwroot/
│   ├── Program.cs
│   └── appsettings.json
├── Portfolio.Shared/
│   └── Dtos/
├── Portfolio.sln
└── README.md
```

## Technologies Used

| Area				| Technology 				| Version 	|
| ------------- 	| ------------------------- | --------- |
| Runtime 			| .NET 						| net10.0 	|
| API Framework 	| ASP.NET Core Web API 		| 10.0 		|
| Client Framework 	| Blazor WebAssembly 		| 10.0 		|
| ORM 				| Entity Framework Core 	| 9.0.7 	|
| Database 			| SQLite 					| 9.0.7		|
| Logging 			| Serilog 					| 10.0.0  	|
| Mapping 			| AutoMapper 				| 16.2.0 	|
| Browser Storage 	| Blazored.LocalStorage 	| 4.5.0 	|



## Requirements to Run

Before running the application, make sure you have:

- .NET SDK 10.0 or newer
- Visual Studio Code or preferred IDE for .NET Development, or the .NET CLI
- NuGet access to restore packages

No separate database server is required because the project uses a local SQLite file database.

If you want to switch to a different database provider later, you would need to update the EF Core provider and connection string configuration accordingly.

## Steps to Run

Run the following steps in order:

### 1. Restore dependencies

From the repository root, run the following command:

```bash
dotnet restore Portfolio.sln
```

### 2. Apply database migrations

The API uses Entity Framework Core migrations and will create or update the SQLite database when it starts. You can also apply them explicitly with:

```bash
dotnet ef migrations add InitialCreate --project Portfolio.Api/Portfolio.Api.csproj
dotnet ef database update --project Portfolio.Api/Portfolio.Api.csproj
```

If the EF CLI is not installed globally, the app will still attempt to run migrations automatically on startup.

### 3. Run the API

```bash
dotnet run --project Portfolio.Api/Portfolio.Api.csproj
```

The API launch profile uses the following default development URLs:

- HTTP: http://localhost:5197
- HTTPS: https://localhost:7081

### 4. Run the client

Open a second terminal and run:

```bash
dotnet run --project Portfolio.ClassicMode/Portfolio.ClassicMode.csproj
```

The client launch profile uses:

- HTTP: http://localhost:5025
- HTTPS: https://localhost:7096

### 5. Open the application

- Client UI: http://localhost:5025
- API health check: http://localhost:5197/health
- Portfolio API endpoints:
  - GET personal detail: http://localhost:5197/api/portfolio/personaldetail
  - GET projects: http://localhost:5197/api/portfolio/projects
  - GET skills: http://localhost:5197/api/portfolio/skills
  - GET hero data: http://localhost:5197/api/portfolio/hero
  - GET experience: http://localhost:5197/api/portfolio/experience
  - GET certifications: http://localhost:5197/api/portfolio/certifications
  - GET qualifications: http://localhost:5197/api/portfolio/qualifications

## Notes

The client is configured to call the API at https://localhost:7081 in development mode. If you run the API on a different port, update the base URL in the client configuration file at Portfolio.ClassicMode/wwwroot/appsettings.Development.json so the UI points to the correct backend endpoint.

## Author

Created by Rehman Malekar. The updated version of this project is deployed as part of my portfolio site and reflects a personal approach to clean architecture, API-driven content, and a polished web experience.
