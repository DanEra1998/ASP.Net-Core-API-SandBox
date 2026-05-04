# ASP.Net-Core-API-SandBox

A RESTful Web API built with C# and ASP.NET Core that connects to a PostgreSQL database using Entity Framework Core. This project serves as a sandbox for learning and experimenting with backend API development concepts.

## Tech Stack
- **C#** / **ASP.NET Core** — Web API framework
- **Entity Framework Core** — ORM for database interaction
- **PostgreSQL** — Relational database
- **Swagger** — API documentation and testing UI

## Project Structure
User/
├── User.Api/
│   ├── Controllers/      # API endpoints (HTTP routes)
│   ├── Data/             # DbContext — database connection manager
│   ├── Models/           # Database table definitions
│   ├── Migrations/       # EF Core auto-generated database migrations
│   ├── Program.cs        # App configuration and startup
│   └── appsettings.json  # App settings (no secrets)
└── .gitignore

## Endpoints
| Method | URL | Description |
|--------|-----|-------------|
| GET | `/api/users` | Get all users |
| GET | `/api/users/{id}` | Get user by ID |
| POST | `/api/users` | Create a new user |
| DELETE | `/api/users/{id}` | Delete a user |

## Getting Started

### Prerequisites
- .NET 10 SDK
- PostgreSQL
- dotnet-ef CLI tool

### Setup
1. Clone the repo
```bash
git clone git@github.com:DanEra1998/ASP.Net-Core-API-SandBox.git
```

2. Set up your database connection using .NET User Secrets
```bash
cd User.Api
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=userdb;Username=YOUR_USERNAME;Password=YOUR_PASSWORD"
```

3. Run migrations
```bash
dotnet ef database update
```

4. Run the API
```bash
dotnet run
```

5. Open Swagger UI at `http://localhost:5059/swagger`
