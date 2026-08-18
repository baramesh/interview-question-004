---
doc_id: DNEW-QAT-CP-01
module: CANDIDATE_PROFILE
type: test-strategy
---

# QAT-CP-01 — กลยุทธ์ทดสอบ Candidate Profile

| ระดับ | สิ่งที่พิสูจน์ |
|---|---|
| Unit ฝั่ง API | วันเกิด โทรศัพท์ รูป Base64 อาชีพและเพศผ่านกฎ |
| Unit ฝั่ง Client | หน้าแสดงแบบฟอร์มและไม่ส่งเมื่อข้อมูลว่าง |
| build | .NET และ Angular สร้างชิ้นงาน Release สำเร็จ |
| Container | PostgreSQL healthy, API migration สำเร็จ, Nginx ส่งคำขอ `/api` ได้ |
| End-to-end | กรอกครบ อัปโหลดรูป บันทึก ได้ ID ข้อความสำเร็จ และฐานข้อมูลมีระเบียน |
| Visual | จอ desktop และ mobile ไม่มีส่วนล้นหรือองค์ประกอบซ้อน |
| Dependency | `npm audit` และ NuGet vulnerability scan ไม่มีรายการระดับที่ต้องแก้ |

## Exit condition

- ทุกการทดสอบอัตโนมัติผ่าน
- `docker compose ps` แสดงสามบริการทำงานและ PostgreSQL healthy
- บันทึกผ่าน `http://localhost:4204` ได้จริงและตรวจระเบียนใน PostgreSQL ได้
