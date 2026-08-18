# Documentation — Interview Question 004

เอกสารชุดนี้เป็นแหล่งอ้างอิงหลักสำหรับ Test 1 ข้อ 4 โดยใช้ลำดับอ่านดังนี้

1. `00-intake` — แหล่งโจทย์และคำสั่งส่งมอบ
2. `01-requirements` — ความสามารถและเกณฑ์ยอมรับ
3. `02-bu-process` — ลำดับงานของผู้สมัคร
4. `03-domain-data` — แบบจำลองข้อมูล PostgreSQL
5. `04-arch-desc` — สถาปัตยกรรม runtime และการ deploy บน OrbStack
6. `05-ui-desc` — หน้าจอ พฤติกรรม และการเข้าถึง
7. `06-api-contract` — สัญญา API
8. `07-integ-contract` — ขอบเขตระบบภายนอก
9. `08-security-arch` — ขอบเขตและ OWASP security baseline; เริ่มที่ [`08-security-arch/01-candidate-profile/00-README.md`](08-security-arch/01-candidate-profile/00-README.md)
10. `09-qa-and-test` — กลยุทธ์ Test Case, Test Step, ผลทดสอบ และภาพหลักฐาน; เริ่มที่ [`09-qa-and-test/01-candidate-profile/00-README.md`](09-qa-and-test/01-candidate-profile/00-README.md)

## ขอบเขต

- ระบบนี้ครอบคลุม Test 1 ข้อ 4 เท่านั้น
- รหัสโปรแกรมอยู่ใน `src/client` และ `src/api`
- โครงสร้างเอกสารดัดแปลงจาก `pea-docs/design-new` ให้เหมาะกับโครงการทดสอบขนาดเล็ก
