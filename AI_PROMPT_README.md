## AI Prompts Used During Development

### Tool: GitHub Copilot (VS Code, Claude model)

#### 1. Project Setup & Architecture

**Prompt:**

> "Help me build a simple Note App with clean architecture and best practices. Tech stack: Backend: .NET 8 (ASP.NET Core Web API), Database: SQLite, Authentication: JWT, Password Hashing: BCrypt, Frontend: React (TypeScript)"

**Purpose:** Initial project scaffolding, defining the full architecture including models, DTOs, services, controllers, and frontend structure.

#### 2. Authentication (Register & Login)

**Prompt (part of main prompt):**

> "Backend API must include: POST /api/auth/register - register new user, POST /api/auth/login - login and return JWT token. Use JWT authentication. Password hashing with BCrypt."

**Purpose:** Implementing user registration with BCrypt password hashing, login with JWT token generation, and JWT middleware configuration.

#### 3. Notes CRUD API

**Prompt (part of main prompt):**

> "GET /api/notes - get all notes for logged-in user, POST /api/notes - create note (title, content), PUT /api/notes/{id} - update note, DELETE /api/notes/{id} - delete note. Notes must be accessible only by their owner."

**Purpose:** Building the full Notes CRUD service and controller with owner-based authorization via JWT claims.

#### 4. Database & Seed Data

**Prompt (part of main prompt):**

> "Use SQLite (no external DB setup). Include migration and seed data. No hardcoded secrets (use appsettings.json)."

**Purpose:** Setting up EF Core with SQLite, creating the AppDbContext with model configuration, seed data for demo user and notes, and auto-migration on startup.

#### 5. Frontend - Auth Pages

**Prompt (part of main prompt):**

> "Login/Register page (store JWT in localStorage). Show simple error message on failure."

**Purpose:** Creating the LoginPage component with toggle between login/register forms, AuthContext for state management, and Axios interceptor for JWT headers.

#### 6. Frontend - Notes Page

**Prompt (part of main prompt):**

> "Notes page: List notes, Create, edit, delete notes in one page. All requests must include JWT token in header."

**Purpose:** Building the NotesPage component with inline create/edit form, delete confirmation, and automatic JWT inclusion via Axios interceptor.

#### 7. Swagger Configuration

**Prompt (part of main prompt):**

> "Swagger must be enabled"

**Purpose:** Configuring Swagger with JWT bearer security definition so tokens can be tested directly in the Swagger UI.
