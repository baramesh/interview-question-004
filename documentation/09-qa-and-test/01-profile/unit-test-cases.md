---
doc_id: QAT-PF-06
module: PROFILE
type: unit-test-cases
api_test_source:
  - tests/api-tests/CreateProfileRequestTests.cs
  - tests/api-tests/OccupationControllerTests.cs
client_test_source:
  - src/client/src/app/app.spec.ts
---

# QAT-PF-06 — กรณี Unit Test

## ขอบเขต

ตรวจตรรกะที่แยกจาก browser จริง แบ่งเป็น xUnit สำหรับ C#/EF Core InMemory และ Vitest สำหรับ Angular component กับ HTTP testing backend การเดินระบบทั้งเส้นทางยังเป็นหน้าที่ของ Playwright ตาม `QAT-PF-05`

## API — xUnit

| Test Case ID    | ประเภท                         | ชื่อในรหัสโปรแกรม                                         | สิ่งที่ตรวจ                 | ผลที่คาดหวัง                                     |
| --------------- | ------------------------------ | --------------------------------------------------------- | --------------------------- | ------------------------------------------------ |
| `UT-API-PF-001` | Positive / Validation          | `Valid_request_passes_validation`                         | payload มาตรฐาน             | ไม่มี validation result                          |
| `UT-API-PF-002` | Negative / Validation          | `Invalid_birth_date_fails_validation(31/02/2000)`         | วันที่ไม่มีจริง             | ผิดที่ `BirthDate`                               |
| `UT-API-PF-003` | Negative / Validation          | `Invalid_birth_date_fails_validation(2000-01-31)`         | รูปแบบวันที่ผิด             | ผิดที่ `BirthDate`                               |
| `UT-API-PF-004` | Negative / Validation          | `Invalid_birth_date_fails_validation(01/01/2999)`         | วันที่อนาคต                 | ผิดที่ `BirthDate`                               |
| `UT-API-PF-005` | Negative / Validation          | `Invalid_phone_fails_validation(abc)`                     | โทรศัพท์ไม่มีรูปแบบตัวเลข   | ผิดที่ `Phone`                                   |
| `UT-API-PF-006` | Negative / Validation          | `Invalid_phone_fails_validation(12345)`                   | โทรศัพท์สั้นเกินไป          | ผิดที่ `Phone`                                   |
| `UT-API-PF-007` | Negative / File validation     | `Invalid_profile_data_fails_validation`                   | รูปไม่ใช่ Base64 data URL   | ผิดที่ `ProfileBase64`                           |
| `UT-API-PF-008` | Negative / Validation          | `Missing_occupation_code_fails_validation`                | ไม่ส่ง `occupationCode`     | ผิดที่ `OccupationCode`                          |
| `UT-API-PF-009` | Master data / Query            | `GetAll_returns_only_active_occupations_in_display_order` | กรองสถานะและลำดับข้อมูลหลัก | คืนเฉพาะ active ตาม `displayOrder`               |
| `UT-API-PF-010` | Negative / Business rule       | `Create_rejects_unknown_occupation_code`                  | code ไม่อยู่ในข้อมูลหลัก    | มีข้อผิดพลาด `OccupationCode` และไม่สร้างโปรไฟล์ |
| `UT-API-PF-011` | Positive / Persistence mapping | `Create_resolves_occupation_code_to_foreign_key`          | code ที่ถูกต้อง             | สร้างโปรไฟล์ด้วย `occupation_id` ที่จับคู่ได้    |
| `UT-API-PF-012` | Negative / File signature      | `Mismatched_image_signature_fails_validation`             | MIME ไม่ตรงกับ byte         | ผิดที่ `ProfileBase64` และไม่ผ่าน validation     |
| `UT-API-PF-013` | Positive / File signature      | `Supported_image_signatures_pass_validation`              | signature ของรูปที่รองรับ   | PNG และ JPEG ผ่าน validation                     |
| `UT-API-PF-014` | Negative / File type           | `Unsupported_image_types_fail_validation`                 | GIF และ WebP                | ผิดที่ `ProfileBase64` และไม่ผ่าน validation     |

## Client — Vitest

| Test Case ID   | ประเภท                        | ชื่อในรหัสโปรแกรม                                               | สิ่งที่ตรวจ                                      | ผลที่คาดหวัง                                                               |
| -------------- | ----------------------------- | --------------------------------------------------------------- | ------------------------------------------------ | -------------------------------------------------------------------------- |
| `UT-UI-PF-001` | Functional / UI rendering     | `renders the profile form with the Angular Material datepicker` | การสร้าง component, โครงฟอร์ม และตัวเลือกวันเกิด | พบหัวข้อ ฟอร์ม ปุ่มเปิด Material Datepicker และไม่มี `input type="date"`   |
| `UT-UI-PF-002` | Negative / Validation         | `does not submit an empty form`                                 | กดบันทึกเมื่อฟอร์มว่าง                           | แสดง required error และไม่ส่ง POST                                         |
| `UT-UI-PF-003` | Functional / State management | `clears the submitted error state`                              | กด Clear หลังตรวจฟอร์มผิด                        | สถานะ Material invalid ถูกล้าง                                             |
| `UT-UI-PF-004` | Master data / HTTP            | `loads occupation master data from the API`                     | GET ข้อมูลหลักและเก็บ code ในตัวควบคุม           | เรียก `/api/occupations` และเลือกค่า `software-engineer` ได้               |
| `UT-UI-PF-005` | Positive / HTTP / Toast       | `posts the selected occupation code and opens a toast with the saved ID` | payload และผลตอบเมื่อบันทึกฟอร์มที่ถูกต้อง | POST ส่ง code/วันที่ถูกต้อง และ `MatSnackBar.open` ได้ `save data success · ID: 1` |

## ขั้นตอน Unit Test

### UT-API-PF-001–UT-API-PF-008, UT-API-PF-012–UT-API-PF-014 — Request validation

1. Arrange: สร้าง `CreateProfileRequest` มาตรฐาน แล้วแทนค่าฟิลด์ที่ต้องการทดสอบ
2. Act: เรียก `Validator.TryValidateObject` พร้อมตรวจ property ทั้งหมด
3. Assert: กรณีถูกต้องไม่มีผลผิดพลาด; กรณีผิดต้องมี `MemberNames` ตรงฟิลด์เป้าหมาย รับเฉพาะ PNG/JPEG และ byte signature ต้องตรงกับ MIME

### UT-API-PF-009 — Occupation list

1. Arrange: สร้างฐานข้อมูล InMemory ที่มีข้อมูล active สองรายการต่างลำดับและ inactive หนึ่งรายการ
2. Act: เรียก `OccupationsController.GetAll`
3. Assert: response มีเฉพาะ active และเรียงตาม `displayOrder`

### UT-API-PF-010–UT-API-PF-011 — Occupation code mapping

1. Arrange: สร้างฐานข้อมูล InMemory แล้วเตรียม request ด้วย code ที่ไม่รู้จักหรือ code ที่มีอยู่
2. Act: เรียก `ProfilesController.Create`
3. Assert: code ผิดต้องไม่สร้างระเบียน; code ถูกต้องต้องบันทึก `occupation_id` ที่จับคู่ได้

### UT-UI-PF-001–UT-UI-PF-005 — Angular component

1. Arrange: สร้าง `App` ผ่าน Angular TestBed และใช้ HTTP testing backend
2. Act: จำลอง `GET /api/occupations`, การกด Save/Clear หรือการส่งฟอร์มตามกรณี
3. Assert: ตรวจ DOM, สถานะแบบฟอร์ม, HTTP method, URL, `occupationCode` ใน request body และข้อความ Toast ที่มี ID จาก API

## คำสั่ง

```bash
dotnet test
cd src/client
npm test -- --watch=false
```

ผลรันล่าสุดบันทึกใน `unit-test-result.md`
