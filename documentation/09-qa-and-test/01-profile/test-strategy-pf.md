---
doc_id: QAT-PF-01
module: PROFILE
type: test-strategy
---

# QAT-PF-01 — กลยุทธ์ทดสอบ Profile

| ระดับ            | สิ่งที่พิสูจน์                                                                                                           |
| ---------------- | ------------------------------------------------------------------------------------------------------------------------ |
| Unit ฝั่ง API    | กฎ payload การกรองข้อมูลหลักอาชีพ การปฏิเสธ code ผิด และการจับคู่เป็น foreign key                                        |
| Unit ฝั่ง Client | หน้าแสดงแบบฟอร์ม ไม่ส่งเมื่อข้อมูลว่าง ล้างสถานะผิด และอ่านข้อมูลหลักผ่าน API                                            |
| Playwright       | ตรวจเส้นทางผู้ใช้ 9 กรณี รวม avatar และ OWASP 5 กรณี ได้แก่ file size/signature, request/rate limit และ security headers |
| build            | .NET และ Angular สร้างชิ้นงาน Release สำเร็จ                                                                             |
| Container        | PostgreSQL healthy, API migration สำเร็จ, Nginx ส่งคำขอ `/api` ได้                                                       |
| End-to-end       | กรอกครบ อัปโหลดรูป บันทึก ได้ ID ข้อความสำเร็จ และฐานข้อมูลมีระเบียน                                                     |
| Visual           | จอ desktop และ mobile ไม่มีส่วนล้นหรือองค์ประกอบซ้อน                                                                     |
| Dependency       | `npm audit` และ NuGet vulnerability scan ไม่มีรายการระดับที่ต้องแก้                                                      |
| Security design  | ตรวจ control matrix และ production gate ตาม `SV-PF-02`; รายการ GAP ติดตามใน `QAT-PF-08`                                  |

## Exit condition

- ทุกการทดสอบอัตโนมัติผ่าน
- `docker compose ps` แสดงสามบริการทำงานและ PostgreSQL healthy
- บันทึกผ่าน `http://localhost:4204` ได้จริงและตรวจระเบียนใน PostgreSQL ได้
- `npm run test:e2e` ผ่านครบและสร้าง `playwright-test-result.md` จากตัวรายงานอัตโนมัติ
- ผล Playwright ต้องมี screenshot ครบทุก Test Case ID และฝังภาพไว้ในรายงาน
- `docker compose config` ต้องยืนยันว่าพอร์ต client, API และ PostgreSQL ผูกกับ `127.0.0.1`

## คำสั่งทดสอบ Playwright

```bash
cd src/client
npm run test:e2e
```

- นิยามกรณีทดสอบ: `playwright-test-cases.md`
- ผลรันล่าสุด: `playwright-test-result.md`
- ภาพหลักฐาน: `screenshots/`

## เอกสาร Unit Test

- นิยามกรณีทดสอบ: `unit-test-cases.md`
- ผลรันล่าสุด: `unit-test-result.md`

## Security gate

- แผนตรวจ OWASP: `security-test-plan.md`
- สถานะปัจจุบัน: ผ่านเฉพาะ local functional test และยังไม่ผ่าน production security gate
