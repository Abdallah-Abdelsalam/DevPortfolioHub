# DevPortfolioHub API

A professional portfolio management API built with ASP.NET Core 8.0.

## Features
- User authentication with JWT
- Portfolio and project management
- Comments and likes system
- RESTful API with Swagger documentation

## Technologies
- .NET 8.0
- Entity Framework Core
- SQL Server
- AutoMapper
- JWT Authentication

## Getting Started
1. Clone the repository
2. Run `dotnet restore`
3. Update connection string in appsettings.json
4. Run `dotnet ef database update`
5. Run `dotnet run`

## API Endpoints
- `/api/auth` - Register/Login
- `/api/users` - User management
- `/api/portfolios` - Portfolio CRUD
- `/api/projects` - Project CRUD
- `/api/comments` - Comment management

## Swagger
Access Swagger UI at: `/swagger`
