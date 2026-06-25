# MyMusicPracticeTracker

Music Practice Tracker is a personal full-stack starter to track daily music practice across multiple instruments, practice sessions, a lightweight timer, dashboard statistics, and simple goals.

## Architecture overview

### Repository structure

```text
.
├── backend/
│   └── MusicPracticeTracker.Api/
│       ├── Configuration/
│       ├── Controllers/
│       ├── Data/
│       ├── DTOs/
│       ├── Entities/
│       ├── Repositories/
│       ├── Services/
│       ├── Dockerfile
│       └── Program.cs
├── frontend/
│   └── music-practice-tracker-ui/
│       ├── src/app/core/models/
│       ├── src/app/core/services/
│       ├── src/app/pages/
│       ├── proxy.conf.json
│       └── angular.json
├── docker-compose.yml
└── .env.example
```

### Backend

- **ASP.NET Core Web API** on **.NET 10** (compatible with the requirement of .NET 8 or higher)
- **Entity Framework Core** with **MySQL** persistence
- Clean but lightweight layering:
  - `Controllers` for REST endpoints
  - `Services` for application logic
  - `Repositories` for database access
  - `DTOs` for API contracts
  - `Entities` and `ApplicationDbContext` for EF Core
- **Swagger UI** enabled at `/swagger`
- MySQL configuration supports joowee-style environment variables:
  - `DB_HOST`
  - `DB_PORT`
  - `DB_USERNAME`
  - `DB_PASSWORD`
  - `DB_DATABASE`

### Frontend

- **Angular standalone** application with routing
- Feature pages:
  - `Dashboard`
  - `Instruments`
  - `Sessions`
  - `Settings / Goals`
- HTTP services call the backend through `/api/*`
- Angular dev server proxies `/api` to `http://localhost:5081`
- Simple responsive UI to keep V1 clean and evolvable

## V1 features included

### Instruments

- Create an instrument
- Edit an instrument
- Delete an instrument
- List instruments
- Fields: `id`, `name`, `color`, `createdAt`

### Practice sessions

- Create, edit, delete, and list sessions
- Filter by instrument and date range
- Fields: `id`, `instrumentId`, `startTime`, `endTime`, `durationMinutes`, `notes`, `createdAt`
- Includes a simple live timer in the Sessions page that saves a practice session when stopped

### Dashboard

- Practiced time today
- Practiced time this week
- Practiced time this month
- Total practiced time
- Breakdown by instrument
- Goal progress indicators

### Goals

- Daily target in minutes
- Weekly target in minutes
- Progress versus the configured goals

## Run the project

### 1. Start MySQL

```bash
docker compose up -d mysql
```

The default local database matches `.env.example` and the joowee-style variables:

- host: `127.0.0.1`
- port: `3307`
- user: `root`
- password: `root`
- database: `music_practice_tracker`

### 2. Apply EF Core migrations

If `dotnet-ef` is not installed yet:

```bash
dotnet tool install --global dotnet-ef --version 10.0.9
```

Then run:

```bash
cd backend/MusicPracticeTracker.Api
dotnet ef database update
```

### 3. Run the backend API

```bash
cd backend/MusicPracticeTracker.Api
dotnet run
```

Backend URLs:

- `http://localhost:5081`
- `https://localhost:7081`
- Swagger UI: `http://localhost:5081/swagger`

### 4. Run the Angular frontend

```bash
cd frontend/music-practice-tracker-ui
npm install
npm start
```

Frontend URL:

- `http://localhost:4200`

## Validation commands

### Backend

```bash
cd backend/MusicPracticeTracker.Api
dotnet build
```

### Frontend

```bash
cd frontend/music-practice-tracker-ui
npm run build
npm test -- --watch=false
```

## Notes for V2

This starter intentionally avoids over-engineering, but it is ready to evolve toward:

- authentication and multi-tenant user ownership
- richer session analytics
- advanced goal types
- notifications and reminders
- richer SaaS billing or subscription features later
