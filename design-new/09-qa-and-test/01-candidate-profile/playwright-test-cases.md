---
doc_id: DNEW-QAT-CP-05
module: CANDIDATE_PROFILE
type: playwright-test-cases
test_source: src/client/e2e/candidate-profile.spec.ts
---

# QAT-CP-05 — กรณีทดสอบ Playwright

## ขอบเขต

ทดสอบ Candidate Profile ผ่าน Chromium จากหน้า Angular ที่ `http://localhost:4204` ครอบคลุมการแสดงผล กฎตรวจข้อมูล การล้างฟอร์ม การบันทึกผ่าน Nginx ไปยัง ASP.NET Core API และ PostgreSQL สัญญาข้อผิดพลาด `400` และหน้าจอมือถือ

## เงื่อนไขก่อนทดสอบ

1. OrbStack ทำงานด้วย Docker context `orbstack`
2. `docker compose ps` แสดง `client`, `api` และ `postgres`; PostgreSQL มีสถานะ healthy
3. หน้าเว็บตอบที่ `http://localhost:4204` และ API health ตอบที่ `http://localhost:5004/health`
4. ติดตั้งส่วนประกอบด้วย `npm ci` และติดตั้ง Chromium ของ Playwright แล้ว

## ชุดข้อมูลมาตรฐาน

| ฟิลด์      | ค่า                                                   |
| ---------- | ----------------------------------------------------- |
| First name | `Grace`                                               |
| Last name  | `Hopper`                                              |
| Email      | `grace.hopper.{unique}@example.com`                   |
| Phone      | `+66 89 456 7890`                                     |
| Profile    | ไฟล์ PNG ขนาด 1x1 พิกเซลที่ชุดทดสอบสร้างในหน่วยความจำ |
| Birth date | `09/12/1906`                                          |
| Occupation | `Software Engineer`                                   |
| Sex        | `Female`                                              |

## กรณีทดสอบ

### TC-CP-E2E-001 — แสดงฟิลด์และปุ่มตามข้อกำหนด

- **เป้าหมาย:** พิสูจน์ว่าฟอร์มมีตัวควบคุมครบตาม `UIS-CP-01`
- **ขั้นตอน:** เปิดหน้า `/` แล้วตรวจฟอร์ม 8 ฟิลด์ ปุ่มเลือกรูป ปุ่ม Clear และปุ่ม Save
- **ผลที่คาดหวัง:** องค์ประกอบทั้งหมดมองเห็นและระบุตัวได้ด้วย `data-testid`
- **สืบย้อน:** `FR-CP-01`, `UIS-CP-01`, `UIX-CP-01`

### TC-CP-E2E-002 — ปฏิเสธฟอร์มว่างโดยไม่เรียก API

- **เป้าหมาย:** พิสูจน์กฎ required และการหยุดคำขอที่ฝั่ง Client
- **ขั้นตอน:** เปิดหน้า กด Save โดยไม่กรอกข้อมูล และนับคำขอ `POST /api/candidate-profiles`
- **ผลที่คาดหวัง:** แสดง `This field is required.` ครบ 8 จุด และจำนวนคำขอ API เท่ากับ 0
- **สืบย้อน:** `BR-CP-01`, `UIX-CP-01`

### TC-CP-E2E-003 — แสดงข้อผิดพลาดรูปแบบข้อมูล

- **เป้าหมาย:** พิสูจน์กฎอีเมล โทรศัพท์ และวันเกิด
- **ขั้นตอน:** กรอกอีเมล `not-an-email`, โทรศัพท์ `12345`, วันเกิดอนาคต แล้วกด Save
- **ผลที่คาดหวัง:** แสดงข้อความผิดพลาดเฉพาะฟิลด์ทั้งสามตามกฎ
- **สืบย้อน:** `BR-CP-01`, `UIX-CP-01`

### TC-CP-E2E-004 — ปุ่ม Clear ล้างข้อมูลทั้งหมด

- **เป้าหมาย:** พิสูจน์การล้างค่า รูปตัวอย่าง และตัวเลือกเพศ
- **ขั้นตอน:** กรอกชุดข้อมูลมาตรฐาน อัปโหลดรูป ตรวจรูปตัวอย่าง แล้วกด Clear
- **ผลที่คาดหวัง:** ช่องข้อความว่าง ไม่มีรูปตัวอย่าง แสดง `No file selected` และไม่มี radio ที่ถูกเลือก
- **สืบย้อน:** `AC-CP-01`, `UIX-CP-01`

### TC-CP-E2E-005 — บันทึกโปรไฟล์สำเร็จผ่านระบบจริง

- **เป้าหมาย:** พิสูจน์เส้นทาง Angular → Nginx → API → PostgreSQL
- **ขั้นตอน:** กรอกชุดข้อมูลมาตรฐานโดยใช้อีเมลไม่ซ้ำ กด Save รอคำตอบ API และอ่าน payload
- **ผลที่คาดหวัง:** ได้ `201 Created`, `id > 0`, `message = save data success`, แสดงข้อความพร้อม ID และฟอร์มกลับสู่สถานะว่างที่ไม่มี invalid field
- **สืบย้อน:** `FR-CP-01`, `AC-CP-01`, `API-CP-01`, `RV-CP-01`

### TC-CP-E2E-006 — API ปฏิเสธ payload ไม่ถูกต้อง

- **เป้าหมาย:** พิสูจน์การตรวจฝั่ง Server และสัญญา `ValidationProblemDetails`
- **ขั้นตอน:** ส่ง `POST /api/candidate-profiles` ด้วยข้อมูลว่างและค่าที่อยู่นอกรายการอนุญาตผ่าน Playwright request context
- **ผลที่คาดหวัง:** ได้สถานะ `400`; body มี `title`, `status = 400`, `errors` อย่างน้อยหนึ่งรายการ และ `traceId`
- **สืบย้อน:** `BR-CP-01`, `API-CP-01`

### TC-CP-E2E-007 — หน้าจอมือถือไม่มีการล้นแนวนอน

- **เป้าหมาย:** พิสูจน์การตอบสนองที่ viewport 390x844
- **ขั้นตอน:** ตั้ง viewport เป็น 390x844 โหลดหน้าใหม่ เปรียบเทียบ `scrollWidth` ของเอกสารและ body กับความกว้าง viewport
- **ผลที่คาดหวัง:** ความกว้างทั้งสองเท่ากับ viewport และฟอร์มยังมองเห็น
- **สืบย้อน:** `AC-CP-01`, `UIS-CP-01`, `A11Y-CP-01`

## การรันและหลักฐาน

```bash
cd src/client
npm run test:e2e
```

ตัวรายงานจะเขียนผลล่าสุดลง `playwright-test-result.md` โดยตรง รหัสในผลต้องตรงกับรหัสในเอกสารนี้และชื่อ `test()` ใน `candidate-profile.spec.ts`
