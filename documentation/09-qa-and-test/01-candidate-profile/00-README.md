---
doc_id: QAT-CP-INDEX
module: CANDIDATE_PROFILE
type: qa-index
---

# Candidate Profile — Test Documentation

## เอกสารที่ใช้ตรวจ

| ระดับ      | Test Case และ Test Step                                                                     | ผลทดสอบ                                                                  | หลักฐาน                                                         |
| ---------- | ------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------ | --------------------------------------------------------------- |
| End-to-end | [`playwright-test-cases.md`](playwright-test-cases.md)                                      | [`playwright-test-result.md`](playwright-test-result.md)                 | ภาพฝังในผลทดสอบและไฟล์ต้นฉบับใต้ [`screenshots/`](screenshots/) |
| Unit Test  | [`unit-test-cases.md`](unit-test-cases.md)                                                  | [`unit-test-result.md`](unit-test-result.md)                             | ชื่อกรณีตรงกับ xUnit และ Vitest ในรหัสโปรแกรม                   |
| Security   | [`security-test-plan.md`](security-test-plan.md)                                            | ยังไม่ผ่าน production gate                                               | อ้าง `SV-CP-02`; รายการ GAP ต้องมีหลักฐานก่อนปิด                |
| สรุปรวม    | [`test-strategy-cp.md`](test-strategy-cp.md) และ [`traceability-cp.md`](traceability-cp.md) | [`verification-report-2026-08-18.md`](verification-report-2026-08-18.md) | ผล build, container, API และ PostgreSQL                         |

## ตำแหน่ง Test Step

- Playwright: แต่ละหัวข้อ `TC-CP-E2E-*` มีเป้าหมาย ขั้นตอน ผลที่คาดหวัง และการสืบย้อน
- Unit Test: หัวข้อ “ขั้นตอน Unit Test” ใช้รูปแบบ Arrange → Act → Assert และชี้ไปยัง Test Case ID ที่เกี่ยวข้อง

## การจำแนกประเภท Test Case

| ประเภท       | ความหมาย                                                                    |
| ------------ | --------------------------------------------------------------------------- |
| Positive     | ยืนยันเส้นทางสำเร็จด้วยข้อมูลถูกต้อง                                        |
| Negative     | ยืนยันการปฏิเสธข้อมูลหรือสถานการณ์ผิดกฎ                                     |
| Functional   | ยืนยันพฤติกรรม ฟังก์ชัน และสถานะของหน้า                                     |
| API contract | ยืนยันสถานะ HTTP และโครงสร้าง payload/response                              |
| Master data  | ยืนยันการดึง การเรียง และการส่ง code ของข้อมูลหลัก                          |
| Responsive   | ยืนยันการแสดงผลตามขนาดหน้าจอและไม่มีการล้น                                  |
| Security     | ยืนยันการป้องกันไฟล์ ทรัพยากร ข้อมูล การตั้งค่า และการใช้งานผิดวัตถุประสงค์ |
| Conditional  | ใช้เมื่อรูปแบบ production เปิดเงื่อนไขนั้น; ไม่นับเป็นข้อสอบที่ล้มเหลว      |

Test Case ทุกกรณีระบุประเภทหลักหรือประเภทประกอบในเอกสารของตน กรณีการยืนยันตัวตนและการกำหนดสิทธิ์เป็น `Conditional / OUT OF SCOPE` สำหรับข้อสอบนี้

## คำสั่งสร้างผลและภาพใหม่

```bash
cd src/client
npm run test:e2e
```

ตัวรายงานจะเขียน `playwright-test-result.md` และภาพ PNG ใต้ `screenshots/` อัตโนมัติ
