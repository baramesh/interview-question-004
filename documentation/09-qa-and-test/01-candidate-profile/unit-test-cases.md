---
doc_id: QAT-CP-06
module: CANDIDATE_PROFILE
type: unit-test-cases
api_test_source:
  - tests/api-tests/CreateCandidateProfileRequestTests.cs
  - tests/api-tests/OccupationControllerTests.cs
client_test_source:
  - src/client/src/app/app.spec.ts
---

# QAT-CP-06 — กรณี Unit Test

## ขอบเขต

ตรวจตรรกะที่แยกจาก browser จริง แบ่งเป็น xUnit สำหรับ C#/EF Core InMemory และ Vitest สำหรับ Angular component กับ HTTP testing backend การเดินระบบทั้งเส้นทางยังเป็นหน้าที่ของ Playwright ตาม `QAT-CP-05`

## API — xUnit

| Test Case ID    | ชื่อในรหัสโปรแกรม                                         | สิ่งที่ตรวจ                 | ผลที่คาดหวัง                                     |
| --------------- | --------------------------------------------------------- | --------------------------- | ------------------------------------------------ |
| `UT-API-CP-001` | `Valid_request_passes_validation`                         | payload มาตรฐาน             | ไม่มี validation result                          |
| `UT-API-CP-002` | `Invalid_birth_date_fails_validation(31/02/2000)`         | วันที่ไม่มีจริง             | ผิดที่ `BirthDate`                               |
| `UT-API-CP-003` | `Invalid_birth_date_fails_validation(2000-01-31)`         | รูปแบบวันที่ผิด             | ผิดที่ `BirthDate`                               |
| `UT-API-CP-004` | `Invalid_birth_date_fails_validation(01/01/2999)`         | วันที่อนาคต                 | ผิดที่ `BirthDate`                               |
| `UT-API-CP-005` | `Invalid_phone_fails_validation(abc)`                     | โทรศัพท์ไม่มีรูปแบบตัวเลข   | ผิดที่ `Phone`                                   |
| `UT-API-CP-006` | `Invalid_phone_fails_validation(12345)`                   | โทรศัพท์สั้นเกินไป          | ผิดที่ `Phone`                                   |
| `UT-API-CP-007` | `Invalid_profile_data_fails_validation`                   | รูปไม่ใช่ Base64 data URL   | ผิดที่ `ProfileBase64`                           |
| `UT-API-CP-008` | `Missing_occupation_code_fails_validation`                | ไม่ส่ง `occupationCode`     | ผิดที่ `OccupationCode`                          |
| `UT-API-CP-009` | `GetAll_returns_only_active_occupations_in_display_order` | กรองสถานะและลำดับข้อมูลหลัก | คืนเฉพาะ active ตาม `displayOrder`               |
| `UT-API-CP-010` | `Create_rejects_unknown_occupation_code`                  | code ไม่อยู่ในข้อมูลหลัก    | มีข้อผิดพลาด `OccupationCode` และไม่สร้างโปรไฟล์ |
| `UT-API-CP-011` | `Create_resolves_occupation_code_to_foreign_key`          | code ที่ถูกต้อง             | สร้างโปรไฟล์ด้วย `occupation_id` ที่จับคู่ได้    |

## Client — Vitest

| Test Case ID   | ชื่อในรหัสโปรแกรม                           | สิ่งที่ตรวจ                            | ผลที่คาดหวัง                                                 |
| -------------- | ------------------------------------------- | -------------------------------------- | ------------------------------------------------------------ |
| `UT-UI-CP-001` | `renders the candidate form`                | การสร้าง component และโครงฟอร์ม        | พบหัวข้อ ฟอร์ม และ input หลัก                                |
| `UT-UI-CP-002` | `does not submit an empty form`             | กดบันทึกเมื่อฟอร์มว่าง                 | แสดง required error และไม่ส่ง POST                           |
| `UT-UI-CP-003` | `clears the submitted error state`          | กด Clear หลังตรวจฟอร์มผิด              | สถานะ Material invalid ถูกล้าง                               |
| `UT-UI-CP-004` | `loads occupation master data from the API` | GET ข้อมูลหลักและเก็บ code ในตัวควบคุม | เรียก `/api/occupations` และเลือกค่า `software-engineer` ได้ |
| `UT-UI-CP-005` | `posts the selected occupation code`        | payload เมื่อบันทึกฟอร์มที่ถูกต้อง     | POST ส่ง `occupationCode = software-engineer`                |

## ขั้นตอน Unit Test

### UT-API-CP-001–UT-API-CP-008 — Request validation

1. Arrange: สร้าง `CreateCandidateProfileRequest` มาตรฐาน แล้วแทนค่าฟิลด์ที่ต้องการทดสอบ
2. Act: เรียก `Validator.TryValidateObject` พร้อมตรวจ property ทั้งหมด
3. Assert: กรณีถูกต้องไม่มีผลผิดพลาด; กรณีผิดต้องมี `MemberNames` ตรงฟิลด์เป้าหมาย

### UT-API-CP-009 — Occupation list

1. Arrange: สร้างฐานข้อมูล InMemory ที่มีข้อมูล active สองรายการต่างลำดับและ inactive หนึ่งรายการ
2. Act: เรียก `OccupationsController.GetAll`
3. Assert: response มีเฉพาะ active และเรียงตาม `displayOrder`

### UT-API-CP-010–UT-API-CP-011 — Occupation code mapping

1. Arrange: สร้างฐานข้อมูล InMemory แล้วเตรียม request ด้วย code ที่ไม่รู้จักหรือ code ที่มีอยู่
2. Act: เรียก `CandidateProfilesController.Create`
3. Assert: code ผิดต้องไม่สร้างระเบียน; code ถูกต้องต้องบันทึก `occupation_id` ที่จับคู่ได้

### UT-UI-CP-001–UT-UI-CP-005 — Angular component

1. Arrange: สร้าง `App` ผ่าน Angular TestBed และใช้ HTTP testing backend
2. Act: จำลอง `GET /api/occupations`, การกด Save/Clear หรือการส่งฟอร์มตามกรณี
3. Assert: ตรวจ DOM, สถานะแบบฟอร์ม, HTTP method, URL และ `occupationCode` ใน request body

## คำสั่ง

```bash
dotnet test
cd src/client
npm test -- --watch=false
```

ผลรันล่าสุดบันทึกใน `unit-test-result.md`
