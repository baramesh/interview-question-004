# Interview Question 004

Full-stack implementation of Test 1, Question 4 for `example.com`.

## Technology

- Frontend: Angular 22, Angular Material 22, Tailwind CSS 4
- Typography: Google Sans bundled locally through Fontsource
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

## วิธีอ่าน documentation

เริ่มที่ [`documentation/README.md`](documentation/README.md) แล้วอ่านตามลำดับเจ้าของข้อมูล ห้ามเริ่มจากหน้าจอหรือรหัสโปรแกรมเมื่อจะตรวจความถูกต้องของระบบ

| ลำดับ | พื้นที่เอกสาร      | ใช้ตอบคำถาม                                               |
| ----: | ------------------ | --------------------------------------------------------- |
|     1 | `00-intake`        | โจทย์ต้นทางกำหนดอะไรและมีขอบเขตเท่าใด                     |
|     2 | `01-requirements`  | ระบบต้องทำอะไร กฎข้อมูลและคุณภาพที่ต้องผ่านคืออะไร        |
|     3 | `02-bu-process`    | ผู้สมัครทำอะไรตามลำดับ                                    |
|     4 | `03-domain-data`   | ข้อมูลใดถูกเก็บ ความสัมพันธ์และข้อมูลหลักเป็นอย่างไร      |
|     5 | `04-arch-desc`     | Angular, Nginx, API และ PostgreSQL ทำงานร่วมกันอย่างไร    |
|     6 | `08-security-arch` | ขอบเขต OWASP และข้อจำกัดของ local/production คืออะไร      |
|     7 | `06-api-contract`  | request, response, validation และ error มีสัญญาอย่างไร    |
|     8 | `05-ui-desc`       | หน้าจอแบ่งหมวด แสดงผล โต้ตอบ และรองรับการเข้าถึงอย่างไร   |
|     9 | `09-qa-and-test`   | Test Case, Test Step, ผลทดสอบ และ screenshot พิสูจน์ข้อใด |

ทางลัดตามบทบาท:

- ผู้ตรวจโจทย์: `00-intake → 01-requirements → 09-qa-and-test`
- ผู้ตรวจ API/ฐานข้อมูล: `03-domain-data → 04-arch-desc → 06-api-contract → 09-qa-and-test`
- ผู้ตรวจหน้าเว็บ: `05-ui-desc → 06-api-contract → 09-qa-and-test`
- ผู้ตรวจความมั่นคงปลอดภัย: `01-requirements/quality-attributes → 08-security-arch → 09-qa-and-test/security-test-plan.md`

ผล Playwright และภาพล่าสุดอยู่ที่ [`documentation/09-qa-and-test/01-candidate-profile/playwright-test-result.md`](documentation/09-qa-and-test/01-candidate-profile/playwright-test-result.md) ส่วนผล Unit Test อยู่ที่ [`unit-test-result.md`](documentation/09-qa-and-test/01-candidate-profile/unit-test-result.md)
