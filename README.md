# Interview Question 004

Full-stack implementation of Test 1, Question 4 for `example.com`.

## Technology

- Frontend: Angular 22 with reactive forms
- Backend: ASP.NET Core 10 Web API (C#)
- Database: SQLite with Entity Framework Core
- Tests: Vitest and xUnit

## Requirements implemented

- Required validation for every field
- Email and phone format validation
- Birth date validation in `DD/MM/YYYY` format
- Mock occupation combo box
- Image profile stored as a Base64 data URL
- Database-generated record ID and success notification
- Form reset after save and a dedicated Clear action
- Server-side validation in addition to browser validation

## Run locally

### API

```bash
dotnet run --project src/api --urls http://127.0.0.1:5000
```

The SQLite database is created automatically on first run.

### Web application

In a second terminal:

```bash
cd src/client
npm ci
npm start
```

Open [http://localhost:4200](http://localhost:4200).

## Verify

```bash
dotnet test
cd src/client
npm ci
npm test -- --watch=false
npm run build
```
