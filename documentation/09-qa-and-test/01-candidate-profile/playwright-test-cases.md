---
doc_id: QAT-CP-05
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

| ฟิลด์           | ค่า                                                   |
| --------------- | ----------------------------------------------------- |
| First name      | `Grace`                                               |
| Last name       | `Hopper`                                              |
| Email           | `grace.hopper.{unique}@example.com`                   |
| Phone           | `+66 89 456 7890`                                     |
| Profile         | ไฟล์ PNG ขนาด 1x1 พิกเซลที่ชุดทดสอบสร้างในหน่วยความจำ |
| Birth date      | `09/12/1906`                                          |
| Occupation name | `Software Engineer`                                   |
| Occupation code | `software-engineer`                                   |
| Sex             | `Female`                                              |

## กรณีทดสอบ

| Test Case ID    | ประเภทหลัก  | ประเภทรอง                   |
| --------------- | ----------- | --------------------------- |
| `TC-CP-E2E-001` | Functional  | UI / Information hierarchy  |
| `TC-CP-E2E-002` | Negative    | Validation                  |
| `TC-CP-E2E-003` | Negative    | Validation                  |
| `TC-CP-E2E-004` | Functional  | State management            |
| `TC-CP-E2E-005` | Positive    | End-to-end / Persistence    |
| `TC-CP-E2E-006` | Negative    | API contract                |
| `TC-CP-E2E-007` | Responsive  | Visual                      |
| `TC-CP-E2E-008` | Master data | Integration / Display order |
| `SEC-CP-001`    | Security    | Negative / File size        |
| `SEC-CP-002`    | Security    | Negative / File signature   |
| `SEC-CP-003`    | Security    | Negative / Request limit    |
| `SEC-CP-004`    | Security    | Abuse / Rate limit          |
| `SEC-CP-008`    | Security    | Configuration / Headers     |

### TC-CP-E2E-001 — แสดงหมวดข้อมูล ฟิลด์ และปุ่มตามข้อกำหนด

- **ประเภท:** Functional / UI / Information hierarchy
- **เป้าหมาย:** พิสูจน์ว่าฟอร์มแบ่งหมวดและมีตัวควบคุมครบตาม `UIS-CP-01`
- **ขั้นตอน:** เปิดหน้า `/` ตรวจว่า stylesheet loader เปลี่ยน `media` เป็น `all` ตรวจข้อความ `Fields marked * are required` ตรวจ 4 หมวดข้อมูล ฟอร์ม 8 ฟิลด์ ปุ่ม Upload photo, Clear form และ Save profile
- **ผลที่คาดหวัง:** stylesheet ทำงาน ข้อความใช้ดอกจันตรงกับ label และทุกหมวด/องค์ประกอบมองเห็นได้ด้วย `data-testid`
- **สืบย้อน:** `FR-CP-01`, `UIS-CP-01`, `UIX-CP-01`

### TC-CP-E2E-002 — ปฏิเสธฟอร์มว่างโดยไม่เรียก API

- **ประเภท:** Negative / Validation
- **เป้าหมาย:** พิสูจน์กฎ required และการหยุดคำขอที่ฝั่ง Client
- **ขั้นตอน:** เปิดหน้า กด Save โดยไม่กรอกข้อมูล และนับคำขอ `POST /api/candidate-profiles`
- **ผลที่คาดหวัง:** แสดง `This field is required.` ครบ 8 จุด และจำนวนคำขอ API เท่ากับ 0
- **สืบย้อน:** `BR-CP-01`, `UIX-CP-01`

### TC-CP-E2E-003 — แสดงข้อผิดพลาดรูปแบบข้อมูล

- **ประเภท:** Negative / Validation
- **เป้าหมาย:** พิสูจน์กฎอีเมล โทรศัพท์ และวันเกิด
- **ขั้นตอน:** กรอกอีเมล `not-an-email`, โทรศัพท์ `12345`, วันเกิดอนาคต แล้วกด Save
- **ผลที่คาดหวัง:** แสดงข้อความผิดพลาดเฉพาะฟิลด์ทั้งสามตามกฎ
- **สืบย้อน:** `BR-CP-01`, `UIX-CP-01`

### TC-CP-E2E-004 — ปุ่ม Clear ล้างข้อมูลทั้งหมด

- **ประเภท:** Functional / State management
- **เป้าหมาย:** พิสูจน์การล้างค่า รูปตัวอย่าง และตัวเลือกเพศ
- **ขั้นตอน:** กรอกชุดข้อมูลมาตรฐาน อัปโหลดรูป ตรวจรูปตัวอย่าง แล้วกด Clear
- **ผลที่คาดหวัง:** ช่องข้อความว่าง ไม่มีรูปตัวอย่าง แสดง `No file selected` และไม่มี radio ที่ถูกเลือก
- **สืบย้อน:** `AC-CP-01`, `UIX-CP-01`

### TC-CP-E2E-005 — บันทึกโปรไฟล์สำเร็จผ่านระบบจริง

- **ประเภท:** Positive / End-to-end / Persistence
- **เป้าหมาย:** พิสูจน์เส้นทาง Angular → Nginx → API → PostgreSQL
- **ขั้นตอน:** กรอกชุดข้อมูลมาตรฐานโดยใช้อีเมลไม่ซ้ำ กด Save รอคำตอบ API และอ่าน payload
- **ผลที่คาดหวัง:** ได้ `201 Created`, payload ส่ง `occupationCode = software-engineer`, `id > 0`, `message = save data success`, แสดงข้อความพร้อม ID และฟอร์มกลับสู่สถานะว่างที่ไม่มี invalid field
- **สืบย้อน:** `FR-CP-01`, `AC-CP-01`, `API-CP-01`, `RV-CP-01`

### TC-CP-E2E-006 — API ปฏิเสธ payload ไม่ถูกต้อง

- **ประเภท:** Negative / API contract
- **เป้าหมาย:** พิสูจน์การตรวจฝั่ง Server และสัญญา `ValidationProblemDetails`
- **ขั้นตอน:** ส่ง `POST /api/candidate-profiles` ด้วยข้อมูลว่างและ `occupationCode` ที่ไม่อยู่ในข้อมูลหลักผ่าน Playwright request context
- **ผลที่คาดหวัง:** ได้สถานะ `400`; body มี `title`, `status = 400`, `errors` อย่างน้อยหนึ่งรายการ และ `traceId`
- **สืบย้อน:** `BR-CP-01`, `API-CP-01`

### TC-CP-E2E-007 — หน้าจอมือถือไม่มีการล้นแนวนอน

- **ประเภท:** Responsive / Visual
- **เป้าหมาย:** พิสูจน์การตอบสนองที่ viewport 390x844
- **ขั้นตอน:** ตั้ง viewport เป็น 390x844 โหลดหน้าใหม่ เปรียบเทียบ `scrollWidth` ของเอกสารและ body กับความกว้าง viewport
- **ผลที่คาดหวัง:** ความกว้างทั้งสองเท่ากับ viewport และฟอร์มยังมองเห็น
- **สืบย้อน:** `AC-CP-01`, `UIS-CP-01`, `A11Y-CP-01`

### TC-CP-E2E-008 — แสดงข้อมูลหลักอาชีพจาก API ตามลำดับ

- **ประเภท:** Master data / Integration / Display order
- **เป้าหมาย:** พิสูจน์ว่ารายการอาชีพมาจาก API และหน้าเว็บแสดงชื่อครบตามลำดับข้อมูลหลัก
- **ขั้นตอน:** เรียก `GET /api/occupations` ตรวจ `code`/`name` แล้วเปิด combo box อาชีพบนหน้าเว็บ
- **ผลที่คาดหวัง:** API ตอบ `200` พร้อม 5 รายการตาม `displayOrder`; ตัวเลือกบนหน้าแสดง `name` ตรงกับ response
- **สืบย้อน:** `FR-CP-02`, `DDC-CP-02`, `API-CP-02`, `UIX-CP-01`

### SEC-CP-001 — ปฏิเสธรูปที่ถอดรหัสแล้วเกิน 2 MiB

- **ประเภท:** Security / Negative / File size
- **ขั้นตอน:** ส่ง Base64 ที่ถอดรหัสแล้วเกิน 2 MiB แต่ request รวมไม่เกิน 3 MiB
- **ผลที่คาดหวัง:** API ตอบ `400` และระบุข้อผิดพลาดที่ `ProfileBase64`
- **สืบย้อน:** `QAR-CP-01-01`, `API-CP-01`

### SEC-CP-002 — ปฏิเสธ MIME ที่ไม่ตรงกับ byte signature

- **ประเภท:** Security / Negative / File signature
- **ขั้นตอน:** ประกาศ `image/png` แต่ส่ง byte signature ของ JPEG
- **ผลที่คาดหวัง:** API ตอบ `400` และไม่บันทึกข้อมูล
- **สืบย้อน:** `QAR-CP-01-01`, `BR-CP-01-07`, `API-CP-01`

### SEC-CP-003 — ปฏิเสธ request body เกิน 3 MiB

- **ประเภท:** Security / Negative / Resource limit
- **ขั้นตอน:** ส่ง JSON ผ่าน Nginx ที่มีขนาดเกิน 3 MiB
- **ผลที่คาดหวัง:** ตอบ `413` ก่อนเข้าสู่ตรรกะธุรกิจ
- **สืบย้อน:** `QAR-CP-01-02`, `API-CP-01`, `SV-CP-02`

### SEC-CP-004 — จำกัดอัตราคำขอสร้างข้อมูล

- **ประเภท:** Security / Abuse / Rate limit
- **ขั้นตอน:** ส่ง POST จาก IP เดียวซ้ำจนเกิน 20 คำขอภายใน 1 นาที
- **ผลที่คาดหวัง:** อย่างน้อยหนึ่งคำขอตอบ `429` และคำขอที่เกินไม่ถูกเข้าคิว
- **สืบย้อน:** `QAR-CP-01-03`, `API-CP-01`, `SV-CP-02`

### SEC-CP-008 — ส่ง security headers ผ่าน Nginx

- **ประเภท:** Security / Configuration / Browser
- **ขั้นตอน:** เปิดหน้า `/` และเรียก `/api/occupations` ผ่านพอร์ต `4204` แล้วอ่าน response headers
- **ผลที่คาดหวัง:** ทั้งสองคำตอบมี CSP, `X-Content-Type-Options`, frame protection, Referrer Policy และ Permissions Policy
- **สืบย้อน:** `QAR-CP-01-04`, `SV-CP-02`, `DEP-CP-01`

## การรันและหลักฐาน

```bash
cd src/client
npm run test:e2e
```

ตัวรายงานจะเขียนผลล่าสุดลง `playwright-test-result.md` และบันทึก screenshot ของทุกกรณีใต้ `screenshots/` โดยตรง รหัสในผล ชื่อภาพ และชื่อ `test()` ใน `candidate-profile.spec.ts` ต้องตรงกัน
