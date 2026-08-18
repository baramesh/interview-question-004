---
doc_id: QAT-CP-04
module: CANDIDATE_PROFILE
type: playwright-test-result
generated_at: 2026-08-18T07:27:27.013Z
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

| Test Case ID  | ชื่อกรณีทดสอบ                                             | Project  | ผล   | เวลา (ms) | Screenshot                               |
| ------------- | --------------------------------------------------------- | -------- | ---- | --------: | ---------------------------------------- |
| TC-CP-E2E-001 | แสดงฟิลด์และปุ่มตามข้อกำหนด                               | chromium | PASS |       212 | [เปิดภาพ](screenshots/tc-cp-e2e-001.png) |
| TC-CP-E2E-002 | ปฏิเสธการส่งแบบฟอร์มว่างโดยไม่เรียก API                   | chromium | PASS |       278 | [เปิดภาพ](screenshots/tc-cp-e2e-002.png) |
| TC-CP-E2E-003 | แสดงข้อผิดพลาดของอีเมล โทรศัพท์ และวันเกิด                | chromium | PASS |       283 | [เปิดภาพ](screenshots/tc-cp-e2e-003.png) |
| TC-CP-E2E-004 | ปุ่ม Clear ล้างข้อมูลและรูปตัวอย่าง                       | chromium | PASS |       489 | [เปิดภาพ](screenshots/tc-cp-e2e-004.png) |
| TC-CP-E2E-005 | บันทึกโปรไฟล์ผ่าน API และล้างสถานะแบบฟอร์ม                | chromium | PASS |       520 | [เปิดภาพ](screenshots/tc-cp-e2e-005.png) |
| TC-CP-E2E-006 | API ตอบ ValidationProblemDetails เมื่อ payload ไม่ถูกต้อง | chromium | PASS |       213 | [เปิดภาพ](screenshots/tc-cp-e2e-006.png) |
| TC-CP-E2E-007 | หน้าจอมือถือไม่มีการล้นแนวนอน                             | chromium | PASS |       216 | [เปิดภาพ](screenshots/tc-cp-e2e-007.png) |
| TC-CP-E2E-008 | แสดงข้อมูลหลักอาชีพจาก API ตามลำดับ                       | chromium | PASS |       261 | [เปิดภาพ](screenshots/tc-cp-e2e-008.png) |

## ภาพหลักฐาน

### TC-CP-E2E-001 — แสดงฟิลด์และปุ่มตามข้อกำหนด

![TC-CP-E2E-001 — แสดงฟิลด์และปุ่มตามข้อกำหนด](screenshots/tc-cp-e2e-001.png)

### TC-CP-E2E-002 — ปฏิเสธการส่งแบบฟอร์มว่างโดยไม่เรียก API

![TC-CP-E2E-002 — ปฏิเสธการส่งแบบฟอร์มว่างโดยไม่เรียก API](screenshots/tc-cp-e2e-002.png)

### TC-CP-E2E-003 — แสดงข้อผิดพลาดของอีเมล โทรศัพท์ และวันเกิด

![TC-CP-E2E-003 — แสดงข้อผิดพลาดของอีเมล โทรศัพท์ และวันเกิด](screenshots/tc-cp-e2e-003.png)

### TC-CP-E2E-004 — ปุ่ม Clear ล้างข้อมูลและรูปตัวอย่าง

![TC-CP-E2E-004 — ปุ่ม Clear ล้างข้อมูลและรูปตัวอย่าง](screenshots/tc-cp-e2e-004.png)

### TC-CP-E2E-005 — บันทึกโปรไฟล์ผ่าน API และล้างสถานะแบบฟอร์ม

![TC-CP-E2E-005 — บันทึกโปรไฟล์ผ่าน API และล้างสถานะแบบฟอร์ม](screenshots/tc-cp-e2e-005.png)

### TC-CP-E2E-006 — API ตอบ ValidationProblemDetails เมื่อ payload ไม่ถูกต้อง

![TC-CP-E2E-006 — API ตอบ ValidationProblemDetails เมื่อ payload ไม่ถูกต้อง](screenshots/tc-cp-e2e-006.png)

### TC-CP-E2E-007 — หน้าจอมือถือไม่มีการล้นแนวนอน

![TC-CP-E2E-007 — หน้าจอมือถือไม่มีการล้นแนวนอน](screenshots/tc-cp-e2e-007.png)

### TC-CP-E2E-008 — แสดงข้อมูลหลักอาชีพจาก API ตามลำดับ

![TC-CP-E2E-008 — แสดงข้อมูลหลักอาชีพจาก API ตามลำดับ](screenshots/tc-cp-e2e-008.png)

## การสืบย้อน

- รายละเอียดขั้นตอนและผลที่คาดหวัง: `playwright-test-cases.md`
- รหัสทดสอบในรายงานตรงกับชื่อ `test()` ใน `src/client/e2e/candidate-profile.spec.ts`
