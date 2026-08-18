---
doc_id: QAT-PF-07
module: PROFILE
type: unit-test-result
verified_at: 2026-08-18
---

# QAT-PF-07 — ผล Unit Test

## สรุปผล

| ชุดทดสอบ          | เครื่องมือ                            | ทั้งหมด | ผ่าน | ไม่ผ่าน | สถานะ |
| ----------------- | ------------------------------------- | ------: | ---: | ------: | ----- |
| API และกฎข้อมูล   | xUnit + EF Core InMemory              |      16 |   16 |       0 | PASS  |
| Angular component | Vitest + Angular HTTP testing backend |       5 |    5 |       0 | PASS  |
| รวม               | —                                     |      21 |   21 |       0 | PASS  |

## ผลตามกลุ่ม

| กลุ่ม                                      | Test Case ID                    | ผล   |
| ------------------------------------------ | ------------------------------- | ---- |
| Request validation                         | `UT-API-PF-001`–`UT-API-PF-008` | PASS |
| Occupation master data และ foreign key     | `UT-API-PF-009`–`UT-API-PF-011` | PASS |
| MIME, file signature และชนิดไฟล์           | `UT-API-PF-012`–`UT-API-PF-014` | PASS |
| Angular form การอ่านข้อมูลหลัก และ payload | `UT-UI-PF-001`–`UT-UI-PF-005`   | PASS |

## หลักฐานคำสั่ง

- `dotnet test`: Passed 16, Failed 0, Skipped 0
- `npm test -- --watch=false`: Test Files 1 passed, Tests 5 passed
- รายละเอียดและผลที่คาดหวังของแต่ละรายการอยู่ใน `unit-test-cases.md`
