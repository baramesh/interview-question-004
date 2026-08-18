# Interview Question 004

Full-stack implementation of Test 1, Question 4 for `example.com`.

## Technology

- Frontend: Angular 22, Angular Material 22, Tailwind CSS 4
- Backend: ASP.NET Core 10 Web API (C#)
- Database: PostgreSQL 18 with Entity Framework Core and Npgsql
- Local deployment: OrbStack with Docker Compose and Nginx
- Tests: Playwright, Vitest and xUnit

## Requirements implemented

- Required validation for every field
- Email and phone format validation
- Birth date validation in `DD/MM/YYYY` format
- PostgreSQL-backed occupation master data loaded through `GET /api/occupations`
- Candidate submission sends `occupationCode`; the API resolves it to an internal foreign key
- Image profile stored as a Base64 data URL
- PostgreSQL-generated record ID and success notification
- Form reset after save and a dedicated Clear action
- Server-side validation in addition to browser validation
- Responsive Angular Material controls with Tailwind CSS layout

## Run the complete stack on OrbStack

OrbStack must be running with Docker context `orbstack`.

```bash
cp .env.example .env
docker compose up -d --build
docker compose ps
```

Open [http://localhost:4204](http://localhost:4204). The API health endpoint is available at [http://localhost:5004/health](http://localhost:5004/health), and PostgreSQL is exposed locally on port `5434` for inspection.

Stop the services without removing PostgreSQL data:

```bash
docker compose down
```

## Run in development mode

Start PostgreSQL only:

```bash
docker compose up -d postgres
```

Start the API:

```bash
dotnet run --project src/api --urls http://127.0.0.1:5000
```

Start the Angular client in a second terminal. Its proxy sends `/api` to port `5000`:

```bash
cd src/client
npm ci
npm start
```

Open [http://localhost:4200](http://localhost:4200).

## Verify

```bash
dotnet test
dotnet list package --vulnerable --include-transitive
cd src/client
npm ci
npm test -- --watch=false
npm run build
npm run test:e2e
npm audit
```

## Design documentation

Start with [`design-new/README.md`](design-new/README.md). It traces the attached requirements through business flow, PostgreSQL data design, runtime architecture, UI behavior, API contract, security boundary, and test evidence.

Playwright test cases and the generated latest result are stored in [`design-new/09-qa-and-test/01-candidate-profile`](design-new/09-qa-and-test/01-candidate-profile).
