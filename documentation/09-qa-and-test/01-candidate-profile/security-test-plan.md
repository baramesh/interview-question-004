---
doc_id: QAT-CP-08
module: CANDIDATE_PROFILE
type: security-test-plan
security_refs:
  - SV-CP-02
---

# QAT-CP-08 — OWASP Security Test Plan

## สถานะ

แผนนี้เป็น production gate ปัจจุบันระบบผ่านเพียงการตรวจพื้นฐานสำหรับ local test รายการที่สถานะ `GAP` ต้องแก้และรันทดสอบใหม่ก่อน production การยืนยันตัวตนและการกำหนดสิทธิ์เป็น `OUT OF SCOPE` สำหรับข้อสอบ และจะเปิดเป็นกรณีทดสอบเมื่อรูปแบบการใช้งาน production ต้องผูกตัวตนหรือเจ้าของข้อมูล

| Test Case ID | ประเภท                       | ขั้นตอน                                             | ผลที่คาดหวัง                                                          | สถานะปัจจุบัน                                   |
| ------------ | ---------------------------- | --------------------------------------------------- | --------------------------------------------------------------------- | ----------------------------------------------- |
| `SEC-CP-001` | Negative / File upload       | ส่ง payload ที่ decoded image เกิน 2 MB             | API ตอบ `400` และไม่สร้างระเบียน                                      | IMPLEMENTED; ต้องเพิ่ม automated test           |
| `SEC-CP-002` | Negative / File validation   | ส่ง data URL MIME ถูกแต่ byte ไม่ตรง file signature | API ปฏิเสธก่อนบันทึก                                                  | GAP                                             |
| `SEC-CP-003` | Negative / Resource limit    | ส่ง request body เกินเพดานที่ reverse proxy กำหนด   | ถูกปฏิเสธก่อน model binding                                           | GAP                                             |
| `SEC-CP-004` | Abuse / Rate limit           | ส่งคำขอสร้างซ้ำเกิน rate limit                      | ตอบ `429` และมี log ที่ไม่มี PII                                      | GAP                                             |
| `SEC-CP-005` | Conditional / Authentication | เรียก API โดยไม่มี identity                         | ทดสอบ `401` เฉพาะเมื่อ access model กำหนดให้ต้องยืนยันตัวตน           | OUT OF SCOPE สำหรับข้อสอบ                       |
| `SEC-CP-006` | Conditional / Authorization  | identity ไม่มีสิทธิสร้างโปรไฟล์เรียก POST           | ทดสอบ `403` เฉพาะเมื่อมีสิทธิ์หรือ ownership                          | OUT OF SCOPE สำหรับข้อสอบ                       |
| `SEC-CP-007` | Negative / Error handling    | ทำให้ API เกิด `500` ใน production profile          | response ไม่มี stack trace, SQL หรือ connection string                | PARTIAL; ต้องทดสอบ production configuration     |
| `SEC-CP-008` | Configuration / Browser      | ตรวจ response headers ผ่าน HTTPS                    | มี CSP, content-type protection, frame protection และ referrer policy | GAP                                             |
| `SEC-CP-009` | Configuration / Network      | ตรวจ network exposure                               | PostgreSQL ไม่เปิด public; API เข้าผ่าน reverse proxy เท่านั้น        | GAP ใน compose local                            |
| `SEC-CP-010` | Security scan / Dependency   | รัน dependency, secret, SAST และ DAST scan          | ไม่มีรายการ severity สูงที่ยังไม่จัดการ                               | PARTIAL; ปัจจุบันมีเฉพาะ manual dependency scan |

## หลักฐานที่ต้องแนบเมื่อปิด GAP

- คำสั่งและผลทดสอบที่สร้างซ้ำได้
- response status/header ที่ redact ข้อมูลส่วนบุคคล
- รายงานเครื่องมือตรวจพร้อมรุ่นและวันรัน
- screenshot เฉพาะ browser security state; ห้ามจับภาพ secret, token หรือข้อมูลผู้สมัครจริง
