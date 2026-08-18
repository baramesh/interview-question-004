---
doc_id: QAT-CP-03
module: CANDIDATE_PROFILE
type: verification-report
verified_at: 2026-08-18
---

# QAT-CP-03 — รายงานผลตรวจวันที่ 18 สิงหาคม 2026

| รายการ                        | ผล                                                                                                   |
| ----------------------------- | ---------------------------------------------------------------------------------------------------- |
| xUnit                         | ผ่าน 16 รายการ; ผลรายกรณีอยู่ใน `unit-test-result.md`                                                |
| Angular Vitest                | ผ่าน 5 รายการ; ผลรายกรณีอยู่ใน `unit-test-result.md`                                                 |
| Playwright Chromium           | ผ่าน 14 รายการ รวม avatar และ security 5 รายการ; มี screenshot ครบใน `playwright-test-result.md`     |
| Angular production build      | ผ่าน ขนาดเริ่มต้น 615.87 kB รวม Google Sans ที่ bundle ในระบบ                                        |
| NuGet vulnerability scan      | ไม่พบแพ็กเกจที่มีช่องโหว่จากแหล่งปัจจุบัน                                                            |
| npm audit                     | ไม่พบช่องโหว่                                                                                        |
| Docker Compose บน OrbStack    | client, api, postgres ทำงานครบ; postgres healthy                                                     |
| API health                    | `GET http://localhost:5004/health` ตอบ `healthy`                                                     |
| End-to-end ผ่าน Nginx         | `POST http://localhost:4204/api/candidate-profiles` ตอบ `201` และข้อความสำเร็จ                       |
| PostgreSQL                    | migration ข้อมูลหลักสำเร็จ ข้อมูลเดิมถูกจับคู่ `occupation_id` และมีอาชีพ 5 รายการ                   |
| Desktop visual                | แบบฟอร์มองค์กรแบ่ง 4 หมวด avatar 96px และดอกจัน required แสดงตรงคำชี้แจง                             |
| Mobile visual                 | viewport 390px เรียงหมวดและปุ่มหนึ่งคอลัมน์ ไม่มี horizontal overflow                                |
| Browser console               | ไม่มี error หรือ warning หลังบันทึก                                                                  |
| OWASP security implementation | file signature, request limit, rate limit, security headers และ loopback ผ่านการตรวจ                 |
| OWASP production gate         | ยังไม่ผ่าน เพราะ TLS, การจัดเก็บรูป, retention, secret manager และ automated scan อยู่นอก local test |

## หลักฐาน runtime ล่าสุด

- การส่งผ่านหน้าเว็บได้ `save data success` พร้อม ID ที่ PostgreSQL สร้าง
- หลังบันทึกจำนวน Angular Material invalid field เท่ากับ `0`
- ชุดบริการยังเปิดไว้ที่ `http://localhost:4204` เพื่อให้ตรวจต่อได้
