---
doc_id: QAT-PF-02
module: PROFILE
type: traceability
---

# QAT-PF-02 — ตารางสืบย้อน

| ข้อกำหนด  | กระบวนการ | ข้อมูล               | UI                               | API                  | การทดสอบ                                                                                                                                              |
| --------- | --------- | -------------------- | -------------------------------- | -------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------- |
| FR-PF-01  | FLW-PF-01 | DDC-PF-01            | UIS-PF-01, UIX-PF-01, A11Y-PF-01 | API-PF-01            | `UT-API-PF-001`–`UT-API-PF-008`, `UT-UI-PF-001`–`UT-UI-PF-003`, `TC-PF-E2E-001`–`TC-PF-E2E-007`, `TC-PF-E2E-009`–`TC-PF-E2E-011`, `TC-PF-CONTENT-001` |
| FR-PF-02  | FLW-PF-01 | DDC-PF-02            | UIS-PF-01, UIX-PF-01             | API-PF-02            | `UT-API-PF-009`–`UT-API-PF-011`, `UT-UI-PF-004`–`UT-UI-PF-005`, `TC-PF-E2E-008`                                                                       |
| BR-PF-01  | FLW-PF-01 | DDC-PF-01, DDC-PF-02 | UIX-PF-01                        | API-PF-01, API-PF-02 | `UT-API-PF-002`–`UT-API-PF-011`, `TC-PF-E2E-002`, `TC-PF-E2E-003`, `TC-PF-E2E-006`                                                                    |
| AC-PF-01  | FLW-PF-01 | DDC-PF-01            | UIS-PF-01                        | API-PF-01            | `TC-PF-E2E-004`, `TC-PF-E2E-005`, `TC-PF-E2E-007`, `TC-PF-E2E-011`, `TC-PF-CONTENT-001`                                                               |
| QAR-PF-01 | FLW-PF-01 | DDC-PF-01            | ไม่เปลี่ยน UI                    | API-PF-01            | `UT-API-PF-012`–`UT-API-PF-013`, `SEC-PF-001`–`SEC-PF-004`, `SEC-PF-008`                                                                              |
