---
doc_id: QAT-PF-03
module: PROFILE
type: verification-report
verified_at: 2026-08-18
---

# QAT-PF-03 — รายงานผลตรวจวันที่ 18 สิงหาคม 2026

| รายการ                        | ผล                                                                                                                            |
| ----------------------------- | ----------------------------------------------------------------------------------------------------------------------------- |
| xUnit                         | ผ่าน 16 รายการ; ผลรายกรณีอยู่ใน `unit-test-result.md`                                                                         |
| Angular Vitest                | ผ่าน 5 รายการ; ผลรายกรณีอยู่ใน `unit-test-result.md`                                                                          |
| Playwright Chromium           | ผ่าน 16 รายการ รวม Angular Material Datepicker, avatar และ security 6 รายการ; มี screenshot ครบใน `playwright-test-result.md` |
| Angular production build      | ผ่าน ขนาดเริ่มต้น 801.41 kB รวม Google Sans, Angular Material Datepicker และ date-fns ในระบบ                                  |
| NuGet vulnerability scan      | ไม่พบแพ็กเกจที่มีช่องโหว่จากแหล่งปัจจุบัน                                                                                     |
| npm audit                     | ไม่พบช่องโหว่                                                                                                                 |
| Docker Compose บน OrbStack    | client, api, postgres ทำงานครบ; postgres healthy                                                                              |
| API health                    | `GET http://localhost:5004/health` ตอบ `healthy`                                                                              |
| End-to-end ผ่าน Nginx         | `POST http://localhost:4204/api/profiles` ตอบ `201` และข้อความสำเร็จ                                                          |
| PostgreSQL                    | สร้างฐานข้อมูลทดสอบใหม่สำเร็จ มีตาราง `profiles`, `occupations` และ `__EFMigrationsHistory`                                   |
| ความเป็นกลางของคำเรียก        | ตรวจ source, UI, API, schema, ชุดทดสอบ และเอกสารแล้ว ไม่พบคำเรียกบทบาทที่โจทย์ไม่ได้กำหนด                                     |
| Desktop visual                | แบบฟอร์มองค์กรแบ่ง 4 หมวด avatar 96px ดอกจัน required และ Material Datepicker แสดงครบ                                         |
| Birth date control            | ใช้ `MatDatepicker` + `@angular/material-date-fns-adapter`; ไม่มี `input type="date"` ของเบราว์เซอร์                          |
| Mobile visual                 | viewport 390px เรียงหมวดและปุ่มหนึ่งคอลัมน์ ไม่มี horizontal overflow                                                         |
| Browser console               | ไม่มี error หรือ warning หลังบันทึก                                                                                           |
| OWASP security implementation | จำกัดรูปเป็น PNG/JPEG, ตรวจ file signature, request limit, rate limit, security headers และ loopback ผ่านการตรวจ              |
| OWASP production gate         | ยังไม่ผ่าน เพราะ TLS, การจัดเก็บรูป, retention, secret manager และ automated scan อยู่นอก local test                          |

## หลักฐาน runtime ล่าสุด

- การส่งผ่านหน้าเว็บได้ `save data success` พร้อม ID ที่ PostgreSQL สร้าง
- หลังบันทึกจำนวน Angular Material invalid field เท่ากับ `0`
- ชุดบริการยังเปิดไว้ที่ `http://localhost:4204` เพื่อให้ตรวจต่อได้
