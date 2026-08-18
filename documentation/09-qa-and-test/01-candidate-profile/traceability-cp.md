---
doc_id: QAT-CP-02
module: CANDIDATE_PROFILE
type: traceability
---

# QAT-CP-02 — ตารางสืบย้อน

| ข้อกำหนด  | กระบวนการ | ข้อมูล               | UI                               | API                  | การทดสอบ                                                                                        |
| --------- | --------- | -------------------- | -------------------------------- | -------------------- | ----------------------------------------------------------------------------------------------- |
| FR-CP-01  | FLW-CP-01 | DDC-CP-01            | UIS-CP-01, UIX-CP-01, A11Y-CP-01 | API-CP-01            | `UT-API-CP-001`–`UT-API-CP-008`, `UT-UI-CP-001`–`UT-UI-CP-003`, `TC-CP-E2E-001`–`TC-CP-E2E-007` |
| FR-CP-02  | FLW-CP-01 | DDC-CP-02            | UIS-CP-01, UIX-CP-01             | API-CP-02            | `UT-API-CP-009`–`UT-API-CP-011`, `UT-UI-CP-004`–`UT-UI-CP-005`, `TC-CP-E2E-008`                 |
| BR-CP-01  | FLW-CP-01 | DDC-CP-01, DDC-CP-02 | UIX-CP-01                        | API-CP-01, API-CP-02 | `UT-API-CP-002`–`UT-API-CP-011`, `TC-CP-E2E-002`, `TC-CP-E2E-003`, `TC-CP-E2E-006`              |
| AC-CP-01  | FLW-CP-01 | DDC-CP-01            | UIS-CP-01                        | API-CP-01            | `TC-CP-E2E-004`, `TC-CP-E2E-005`, `TC-CP-E2E-007`                                               |
| QAR-CP-01 | FLW-CP-01 | DDC-CP-01            | ไม่เปลี่ยน UI                    | API-CP-01            | `UT-API-CP-012`–`UT-API-CP-013`, `SEC-CP-001`–`SEC-CP-004`, `SEC-CP-008`                        |
