---
doc_id: DNEW-QAT-CP-03
module: CANDIDATE_PROFILE
type: verification-report
verified_at: 2026-08-18
---

# QAT-CP-03 — รายงานผลตรวจวันที่ 18 สิงหาคม 2026

| รายการ                     | ผล                                                                             |
| -------------------------- | ------------------------------------------------------------------------------ |
| xUnit                      | ผ่าน 7 รายการ                                                                  |
| Angular Vitest             | ผ่าน 3 รายการ                                                                  |
| Playwright Chromium        | ผ่าน 7 รายการ; ผลรายกรณีอยู่ใน `playwright-test-result.md`                     |
| Angular production build   | ผ่าน ขนาดเริ่มต้น 612.38 kB                                                    |
| NuGet vulnerability scan   | ไม่พบแพ็กเกจที่มีช่องโหว่จากแหล่งปัจจุบัน                                      |
| npm audit                  | ไม่พบช่องโหว่                                                                  |
| Docker Compose บน OrbStack | client, api, postgres ทำงานครบ; postgres healthy                               |
| API health                 | `GET http://localhost:5004/health` ตอบ `healthy`                               |
| End-to-end ผ่าน Nginx      | `POST http://localhost:4204/api/candidate-profiles` ตอบ `201` และข้อความสำเร็จ |
| PostgreSQL                 | ตรวจพบระเบียนที่บันทึกและ migration 2 รายการ                                   |
| Desktop visual             | แบบฟอร์มสองคอลัมน์ การแจ้งสำเร็จ และการล้างสถานะถูกต้อง                        |
| Mobile visual              | viewport 390px ไม่มี horizontal overflow (`scrollWidth = 390`)                 |
| Browser console            | ไม่มี error หรือ warning หลังบันทึก                                            |

## หลักฐาน runtime ล่าสุด

- การส่งผ่านหน้าเว็บได้ `save data success · ID: 3`
- หลังบันทึกจำนวน Angular Material invalid field เท่ากับ `0`
- ชุดบริการยังเปิดไว้ที่ `http://localhost:4204` เพื่อให้ตรวจต่อได้
