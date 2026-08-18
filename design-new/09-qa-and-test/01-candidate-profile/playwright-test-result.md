---
doc_id: QAT-CP-04
module: CANDIDATE_PROFILE
type: playwright-test-result
generated_at: 2026-08-18T07:12:15.060Z
---

# QAT-CP-04 — ผลทดสอบ Playwright

> ไฟล์นี้สร้างอัตโนมัติจาก `npm run test:e2e` ห้ามแก้ผลด้วยมือ

## สภาพแวดล้อม

| รายการ       | ค่า                                                         |
| ------------ | ----------------------------------------------------------- |
| Base URL     | `http://127.0.0.1:4204`                                     |
| Browser      | Chromium                                                    |
| ระบบที่ทดสอบ | Angular → Nginx → ASP.NET Core API → PostgreSQL บน OrbStack |

## สรุปผล

| ทั้งหมด | ผ่าน | ไม่ผ่าน | สถานะชุดทดสอบ |
| ------: | ---: | ------: | ------------- |
|       8 |    8 |       0 | PASS          |

## ผลรายกรณี

| Test Case ID  | ชื่อกรณีทดสอบ                                             | Project  | ผล   | เวลา (ms) |
| ------------- | --------------------------------------------------------- | -------- | ---- | --------: |
| TC-CP-E2E-001 | แสดงฟิลด์และปุ่มตามข้อกำหนด                               | chromium | PASS |       324 |
| TC-CP-E2E-002 | ปฏิเสธการส่งแบบฟอร์มว่างโดยไม่เรียก API                   | chromium | PASS |       272 |
| TC-CP-E2E-003 | แสดงข้อผิดพลาดของอีเมล โทรศัพท์ และวันเกิด                | chromium | PASS |       279 |
| TC-CP-E2E-004 | ปุ่ม Clear ล้างข้อมูลและรูปตัวอย่าง                       | chromium | PASS |       671 |
| TC-CP-E2E-005 | บันทึกโปรไฟล์ผ่าน API และล้างสถานะแบบฟอร์ม                | chromium | PASS |       684 |
| TC-CP-E2E-006 | API ตอบ ValidationProblemDetails เมื่อ payload ไม่ถูกต้อง | chromium | PASS |       214 |
| TC-CP-E2E-007 | หน้าจอมือถือไม่มีการล้นแนวนอน                             | chromium | PASS |       214 |
| TC-CP-E2E-008 | แสดงข้อมูลหลักอาชีพจาก API ตามลำดับ                       | chromium | PASS |       252 |

## การสืบย้อน

- รายละเอียดขั้นตอนและผลที่คาดหวัง: `playwright-test-cases.md`
- รหัสทดสอบในรายงานตรงกับชื่อ `test()` ใน `src/client/e2e/candidate-profile.spec.ts`
