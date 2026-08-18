---
doc_id: QAT-CP-01
module: CANDIDATE_PROFILE
type: test-strategy
---

# QAT-CP-01 — กลยุทธ์ทดสอบ Candidate Profile

| ระดับ            | สิ่งที่พิสูจน์                                                                                     |
| ---------------- | -------------------------------------------------------------------------------------------------- |
| Unit ฝั่ง API    | กฎ payload การกรองข้อมูลหลักอาชีพ การปฏิเสธ code ผิด และการจับคู่เป็น foreign key                  |
| Unit ฝั่ง Client | หน้าแสดงแบบฟอร์ม ไม่ส่งเมื่อข้อมูลว่าง ล้างสถานะผิด และอ่านข้อมูลหลักผ่าน API                      |
| Playwright       | ตรวจการแสดงฟอร์ม กฎตรวจข้อมูล Clear การบันทึกจริงผ่าน API สัญญา `400` และหน้าจอมือถือด้วย Chromium |
| build            | .NET และ Angular สร้างชิ้นงาน Release สำเร็จ                                                       |
| Container        | PostgreSQL healthy, API migration สำเร็จ, Nginx ส่งคำขอ `/api` ได้                                 |
| End-to-end       | กรอกครบ อัปโหลดรูป บันทึก ได้ ID ข้อความสำเร็จ และฐานข้อมูลมีระเบียน                               |
| Visual           | จอ desktop และ mobile ไม่มีส่วนล้นหรือองค์ประกอบซ้อน                                               |
| Dependency       | `npm audit` และ NuGet vulnerability scan ไม่มีรายการระดับที่ต้องแก้                                |

## Exit condition

- ทุกการทดสอบอัตโนมัติผ่าน
- `docker compose ps` แสดงสามบริการทำงานและ PostgreSQL healthy
- บันทึกผ่าน `http://localhost:4204` ได้จริงและตรวจระเบียนใน PostgreSQL ได้
- `npm run test:e2e` ผ่านครบและสร้าง `playwright-test-result.md` จากตัวรายงานอัตโนมัติ

## คำสั่งทดสอบ Playwright

```bash
cd src/client
npm run test:e2e
```

- นิยามกรณีทดสอบ: `playwright-test-cases.md`
- ผลรันล่าสุด: `playwright-test-result.md`

## เอกสาร Unit Test

- นิยามกรณีทดสอบ: `unit-test-cases.md`
- ผลรันล่าสุด: `unit-test-result.md`
