---
doc_id: QAT-CP-08
module: CANDIDATE_PROFILE
type: security-test-plan
security_refs:
  - SV-CP-02
---

# QAT-CP-08 — OWASP Security Test Plan

## สถานะ

แผนนี้เป็น production gate ปัจจุบันระบบผ่านเพียงการตรวจพื้นฐานสำหรับ local test รายการที่สถานะ `GAP` ต้องแก้และรันทดสอบใหม่ก่อน production

| Test Case ID | ขั้นตอน                                             | ผลที่คาดหวัง                                                          | สถานะปัจจุบัน                                   |
| ------------ | --------------------------------------------------- | --------------------------------------------------------------------- | ----------------------------------------------- |
| `SEC-CP-001` | ส่ง payload ที่ decoded image เกิน 2 MB             | API ตอบ `400` และไม่สร้างระเบียน                                      | IMPLEMENTED; ต้องเพิ่ม automated test           |
| `SEC-CP-002` | ส่ง data URL MIME ถูกแต่ byte ไม่ตรง file signature | API ปฏิเสธก่อนบันทึก                                                  | GAP                                             |
| `SEC-CP-003` | ส่ง request body เกินเพดานที่ reverse proxy กำหนด   | ถูกปฏิเสธก่อน model binding                                           | GAP                                             |
| `SEC-CP-004` | ส่งคำขอสร้างซ้ำเกิน rate limit                      | ตอบ `429` และมี log ที่ไม่มี PII                                      | GAP                                             |
| `SEC-CP-005` | เรียก API โดยไม่มี identity                         | production ต้องตอบ `401`                                              | GAP; local test ตั้งใจเปิด public               |
| `SEC-CP-006` | identity ไม่มีสิทธิสร้างโปรไฟล์เรียก POST           | ตอบ `403` และไม่เขียนข้อมูล                                           | GAP                                             |
| `SEC-CP-007` | ทำให้ API เกิด `500` ใน production profile          | response ไม่มี stack trace, SQL หรือ connection string                | PARTIAL; ต้องทดสอบ production configuration     |
| `SEC-CP-008` | ตรวจ response headers ผ่าน HTTPS                    | มี CSP, content-type protection, frame protection และ referrer policy | GAP                                             |
| `SEC-CP-009` | ตรวจ network exposure                               | PostgreSQL ไม่เปิด public; API เข้าผ่าน reverse proxy เท่านั้น        | GAP ใน compose local                            |
| `SEC-CP-010` | รัน dependency, secret, SAST และ DAST scan          | ไม่มีรายการ severity สูงที่ยังไม่จัดการ                               | PARTIAL; ปัจจุบันมีเฉพาะ manual dependency scan |

## หลักฐานที่ต้องแนบเมื่อปิด GAP

- คำสั่งและผลทดสอบที่สร้างซ้ำได้
- response status/header ที่ redact ข้อมูลส่วนบุคคล
- รายงานเครื่องมือตรวจพร้อมรุ่นและวันรัน
- screenshot เฉพาะ browser security state; ห้ามจับภาพ secret, token หรือข้อมูลผู้สมัครจริง
