# SimpleNoteApp

A simple Note-taking application built with **.NET 8 Web API** (Backend) and **React TypeScript** (Frontend), featuring JWT authentication, SQLite database, and clean architecture.

---

## Tech Stack

### Backend

- **.NET 8** (ASP.NET Core Web API)
- **SQLite** (via Entity Framework Core)
- **JWT Authentication** (Microsoft.AspNetCore.Authentication.JwtBearer)
- **BCrypt** (BCrypt.Net-Next) for password hashing
- **Swagger** (Swashbuckle) for API documentation

### Frontend

- **React 19** with **TypeScript**
- **Vite** (build tool)
- **Axios** (HTTP client)
- **React Router DOM** (routing)

---

## Features

### Authentication

- User registration with unique username and email
- User login with JWT token generation
- Password hashing with BCrypt

### Notes Management

- Create, read, update, and delete notes
- Notes are accessible only by their owner
- All requests require JWT token in Authorization header

---

## API Endpoints

| Method | Endpoint             | Description                      | Auth |
| ------ | -------------------- | -------------------------------- | ---- |
| POST   | `/api/auth/register` | Register new user                | No   |
| POST   | `/api/auth/login`    | Login and get JWT token          | No   |
| GET    | `/api/notes`         | Get all notes for logged-in user | Yes  |
| POST   | `/api/notes`         | Create a new note                | Yes  |
| PUT    | `/api/notes/{id}`    | Update a note                    | Yes  |
| DELETE | `/api/notes/{id}`    | Delete a note                    | Yes  |

---

## Project Structure

```
SimpleNoteApp/
+-- Backend/
|   +-- Controllers/
|   |   +-- AuthController.cs        # Auth endpoints
|   |   +-- NotesController.cs       # Notes CRUD endpoints
|   +-- Data/
|   |   +-- AppDbContext.cs           # EF Core DbContext + seed data
|   +-- DTOs/
|   |   +-- Auth/
|   |   |   +-- LoginDto.cs
|   |   |   +-- RegisterDto.cs
|   |   |   +-- AuthResponseDto.cs
|   |   +-- Notes/
|   |       +-- CreateNoteDto.cs
|   |       +-- UpdateNoteDto.cs
|   |       +-- NoteResponseDto.cs
|   +-- Migrations/                   # EF Core migrations
|   +-- Models/
|   |   +-- User.cs
|   |   +-- Note.cs
|   +-- Services/
|   |   +-- IAuthService.cs
|   |   +-- AuthService.cs
|   |   +-- INoteService.cs
|   |   +-- NoteService.cs
|   +-- Program.cs                    # App configuration
|   +-- appsettings.json              # JWT & DB config
+-- Frontend/
|   +-- src/
|   |   +-- api.ts                    # Axios instance with JWT interceptor
|   |   +-- AuthContext.tsx           # React auth context/provider
|   |   +-- types.ts                  # TypeScript interfaces
|   |   +-- App.tsx                   # Routes setup
|   |   +-- pages/
|   |       +-- LoginPage.tsx         # Login/Register page
|   |       +-- NotesPage.tsx         # Notes CRUD page
|   +-- index.html
|   +-- package.json
|   +-- vite.config.ts
+-- README.md
```

---

## How to Run

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (v18+)
- [dotnet-ef CLI tool](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) (`dotnet tool install --global dotnet-ef`)

### Backend

```bash
cd Backend
dotnet restore
dotnet ef database update
dotnet run
```

The API will be available at **http://localhost:5261**
Swagger UI: **http://localhost:5261/swagger**

### Frontend

```bash
cd Frontend
npm install
npm run dev
```

The frontend will be available at **http://localhost:5173**

---

## Seed Data (Demo Account)

| Field    | Value            |
| -------- | ---------------- |
| Email    | demo@example.com |
| Password | demo123          |
| Username | demo             |

Two sample notes are pre-seeded for this account.

---

## Configuration

All secrets are stored in `appsettings.json` (not hardcoded):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=simplenoteapp.db"
  },
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "SimpleNoteApp",
    "Audience": "SimpleNoteAppUsers",
    "ExpirationInHours": "24"
  }
}
```

> **Note:** For production, change the `SecretKey` to a strong, unique value and consider using environment variables or a secrets manager.

---

## Architecture & Best Practices

- **Service Layer Pattern** - Business logic separated in `IAuthService`/`AuthService` and `INoteService`/`NoteService`
- **DTOs** - Request/Response objects separate from domain models
- **JWT Authentication** - Stateless token-based auth with claims
- **Authorization** - Notes filtered by `UserId` from JWT claims (owner-only access)
- **Auto-Migration** - Database is automatically migrated on startup
- **CORS** - Configured to allow frontend origin
- **Input Validation** - Data annotations on DTOs
- **Swagger** - Full API documentation with JWT bearer support

---
