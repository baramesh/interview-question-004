---
doc_id: QAT-CP-07
module: CANDIDATE_PROFILE
type: unit-test-result
verified_at: 2026-08-18
---

# QAT-CP-07 — ผล Unit Test

## สรุปผล

| ชุดทดสอบ          | เครื่องมือ                            | ทั้งหมด | ผ่าน | ไม่ผ่าน | สถานะ |
| ----------------- | ------------------------------------- | ------: | ---: | ------: | ----- |
| API และกฎข้อมูล   | xUnit + EF Core InMemory              |      16 |   16 |       0 | PASS  |
| Angular component | Vitest + Angular HTTP testing backend |       5 |    5 |       0 | PASS  |
| รวม               | —                                     |      21 |   21 |       0 | PASS  |

## ผลตามกลุ่ม

| กลุ่ม                                      | Test Case ID                    | ผล   |
| ------------------------------------------ | ------------------------------- | ---- |
| Request validation                         | `UT-API-CP-001`–`UT-API-CP-008` | PASS |
| Occupation master data และ foreign key     | `UT-API-CP-009`–`UT-API-CP-011` | PASS |
| MIME และ file signature                    | `UT-API-CP-012`–`UT-API-CP-013` | PASS |
| Angular form การอ่านข้อมูลหลัก และ payload | `UT-UI-CP-001`–`UT-UI-CP-005`   | PASS |

## หลักฐานคำสั่ง

- `dotnet test`: Passed 16, Failed 0, Skipped 0
- `npm test -- --watch=false`: Test Files 1 passed, Tests 5 passed
- รายละเอียดและผลที่คาดหวังของแต่ละรายการอยู่ใน `unit-test-cases.md`
