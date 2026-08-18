---
doc_id: DNEW-UI-DERIVE-CP-01
module: CANDIDATE_PROFILE
type: screen-derivation
relates_to:
  - DNEW-FR-CP-01
  - DNEW-FLW-CP-01
---

# การได้มาของหน้าจอ Candidate Profile

| ความสามารถจากข้อกำหนด | state ที่ผู้ใช้เห็น | เจ้าของหน้าจอ | เหตุผล |
|---|---|---|---|
| สร้างโปรไฟล์ | empty, editing, validation-error, saving, saved, save-error | `UIS-CP-01` | ทุก state เกิดในแบบฟอร์มเดียวและไม่มีเส้นทางอื่น |

ระบบมีหนึ่งหน้าจอ ไม่มีหน้า list/detail เพราะโจทย์กำหนดเฉพาะการสร้างระเบียน
