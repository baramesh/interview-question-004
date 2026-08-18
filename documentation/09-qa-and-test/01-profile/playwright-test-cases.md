---
doc_id: QAT-PF-05
module: PROFILE
type: playwright-test-cases
test_source: src/client/e2e/profile.spec.ts
---

# QAT-PF-05 — กรณีทดสอบ Playwright

## ขอบเขต

ทดสอบ Profile ผ่าน Chromium จากหน้า Angular ที่ `http://localhost:4204` ครอบคลุมการแสดงผล กฎตรวจข้อมูล การล้างฟอร์ม การบันทึกผ่าน Nginx ไปยัง ASP.NET Core API และ PostgreSQL สัญญาข้อผิดพลาด `400` และหน้าจอมือถือ

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

| Test Case ID    | ประเภทหลัก  | ประเภทรอง                    |
| --------------- | ----------- | ---------------------------- |
| `TC-PF-E2E-001` | Functional  | UI / Information hierarchy   |
| `TC-PF-E2E-002` | Negative    | Validation                   |
| `TC-PF-E2E-003` | Negative    | Validation                   |
| `TC-PF-E2E-004` | Functional  | State management             |
| `TC-PF-E2E-005` | Positive    | End-to-end / Persistence     |
| `TC-PF-E2E-006` | Negative    | API contract                 |
| `TC-PF-E2E-007` | Responsive  | Visual                       |
| `TC-PF-E2E-008` | Master data | Integration / Display order  |
| `TC-PF-E2E-009` | Functional  | Profile image / Visual       |
| `TC-PF-E2E-010` | Functional  | Datepicker / Library control |
| `SEC-PF-001`    | Security    | Negative / File size         |
| `SEC-PF-002`    | Security    | Negative / File signature    |
| `SEC-PF-003`    | Security    | Negative / Request limit     |
| `SEC-PF-004`    | Security    | Abuse / Rate limit           |
| `SEC-PF-008`    | Security    | Configuration / Headers      |
| `SEC-PF-011`    | Security    | Negative / File type         |

### TC-PF-E2E-001 — แสดงหมวดข้อมูล ฟิลด์ และปุ่มตามข้อกำหนด

- **ประเภท:** Functional / UI / Information hierarchy
- **เป้าหมาย:** พิสูจน์ว่าฟอร์มแบ่งหมวดและมีตัวควบคุมครบตาม `UIS-PF-01`
- **ขั้นตอน:** เปิดหน้า `/` ตรวจว่า stylesheet loader เปลี่ยน `media` เป็น `all` ตรวจข้อความ `Fields marked * are required` ตรวจ 4 หมวดข้อมูล ฟอร์ม 8 ฟิลด์ ปุ่ม Upload photo, Clear form และ Save profile
- **ผลที่คาดหวัง:** stylesheet ทำงาน ข้อความใช้ดอกจันตรงกับ label และทุกหมวด/องค์ประกอบมองเห็นได้ด้วย `data-testid`
- **สืบย้อน:** `FR-PF-01`, `UIS-PF-01`, `UIX-PF-01`

### TC-PF-E2E-002 — ปฏิเสธฟอร์มว่างโดยไม่เรียก API

- **ประเภท:** Negative / Validation
- **เป้าหมาย:** พิสูจน์กฎ required และการหยุดคำขอที่ฝั่ง Client
- **ขั้นตอน:** เปิดหน้า กด Save โดยไม่กรอกข้อมูล และนับคำขอ `POST /api/profiles`
- **ผลที่คาดหวัง:** แสดง `This field is required.` ครบ 8 จุด และจำนวนคำขอ API เท่ากับ 0
- **สืบย้อน:** `BR-PF-01`, `UIX-PF-01`

### TC-PF-E2E-003 — แสดงข้อผิดพลาดรูปแบบข้อมูล

- **ประเภท:** Negative / Validation
- **เป้าหมาย:** พิสูจน์กฎอีเมล โทรศัพท์ และวันเกิด
- **ขั้นตอน:** กรอกอีเมล `not-an-email`, โทรศัพท์ `12345`, วันเกิดอนาคต แล้วกด Save
- **ผลที่คาดหวัง:** แสดงข้อความผิดพลาดเฉพาะฟิลด์ทั้งสามตามกฎ
- **สืบย้อน:** `BR-PF-01`, `UIX-PF-01`

### TC-PF-E2E-004 — ปุ่ม Clear ล้างข้อมูลทั้งหมด

- **ประเภท:** Functional / State management
- **เป้าหมาย:** พิสูจน์การล้างค่า รูปตัวอย่าง และตัวเลือกเพศ
- **ขั้นตอน:** กรอกชุดข้อมูลมาตรฐาน อัปโหลดรูป ตรวจรูปตัวอย่าง แล้วกด Clear
- **ผลที่คาดหวัง:** ช่องข้อความว่าง ไม่มีรูปตัวอย่าง แสดง `No file selected` และไม่มี radio ที่ถูกเลือก
- **สืบย้อน:** `AC-PF-01`, `UIX-PF-01`

### TC-PF-E2E-005 — บันทึกโปรไฟล์และแสดง Toast พร้อม ID

- **ประเภท:** Positive / End-to-end / Persistence
- **เป้าหมาย:** พิสูจน์เส้นทาง Angular → Nginx → API → PostgreSQL
- **ขั้นตอน:** กรอกชุดข้อมูลมาตรฐานโดยใช้อีเมลไม่ซ้ำ กด Save รอคำตอบ API อ่าน `id` แล้วตรวจ Toast กับปุ่ม `Close`
- **ผลที่คาดหวัง:** ได้ `201 Created`, payload ส่ง `occupationCode = software-engineer`, `id > 0`, `message = save data success`, Toast แสดง `save data success · ID: {id}` ตรงกับ API และฟอร์มกลับสู่สถานะว่างที่ไม่มี invalid field
- **สืบย้อน:** `FR-PF-01`, `AC-PF-01`, `API-PF-01`, `RV-PF-01`

### TC-PF-E2E-006 — API ปฏิเสธ payload ไม่ถูกต้อง

- **ประเภท:** Negative / API contract
- **เป้าหมาย:** พิสูจน์การตรวจฝั่ง Server และสัญญา `ValidationProblemDetails`
- **ขั้นตอน:** ส่ง `POST /api/profiles` ด้วยข้อมูลว่างและ `occupationCode` ที่ไม่อยู่ในข้อมูลหลักผ่าน Playwright request context
- **ผลที่คาดหวัง:** ได้สถานะ `400`; body มี `title`, `status = 400`, `errors` อย่างน้อยหนึ่งรายการ และ `traceId`
- **สืบย้อน:** `BR-PF-01`, `API-PF-01`

### TC-PF-E2E-007 — หน้าจอมือถือไม่มีการล้นแนวนอน

- **ประเภท:** Responsive / Visual
- **เป้าหมาย:** พิสูจน์การตอบสนองที่ viewport 390x844
- **ขั้นตอน:** ตั้ง viewport เป็น 390x844 โหลดหน้าใหม่ เปรียบเทียบ `scrollWidth` ของเอกสารและ body กับความกว้าง viewport
- **ผลที่คาดหวัง:** ความกว้างทั้งสองเท่ากับ viewport และฟอร์มยังมองเห็น
- **สืบย้อน:** `AC-PF-01`, `UIS-PF-01`, `A11Y-PF-01`

### TC-PF-E2E-008 — แสดงข้อมูลหลักอาชีพจาก API ตามลำดับ

- **ประเภท:** Master data / Integration / Display order
- **เป้าหมาย:** พิสูจน์ว่ารายการอาชีพมาจาก API และหน้าเว็บแสดงชื่อครบตามลำดับข้อมูลหลัก
- **ขั้นตอน:** เรียก `GET /api/occupations` ตรวจ `code`/`name` แล้วเปิด combo box อาชีพบนหน้าเว็บ
- **ผลที่คาดหวัง:** API ตอบ `200` พร้อม 5 รายการตาม `displayOrder`; ตัวเลือกบนหน้าแสดง `name` ตรงกับ response
- **สืบย้อน:** `FR-PF-02`, `DDC-PF-02`, `API-PF-02`, `UIX-PF-01`

### TC-PF-E2E-009 — แสดงรูปโปรไฟล์ใน avatar แบบองค์กร

- **ประเภท:** Functional / Profile image / Visual
- **เป้าหมาย:** พิสูจน์การแสดงรูปใน avatar และการควบคุมไฟล์ตาม `UIS-PF-01`
- **ขั้นตอน:** อัปโหลด `profile.png` แล้วตรวจรูป ชื่อไฟล์ ปุ่ม Remove ขนาด avatar และวิธีจัดวางรูป
- **ผลที่คาดหวัง:** avatar เป็นวงกลม 96x96px รูปใช้ `object-fit: cover` และมีปุ่ม Remove
- **สืบย้อน:** `FR-PF-01`, `AC-PF-01`, `UIS-PF-01`, `UIX-PF-01`

### TC-PF-E2E-010 — เลือกวันเกิดผ่าน Angular Material Datepicker

- **ประเภท:** Functional / Datepicker / Library control
- **เป้าหมาย:** พิสูจน์ว่าช่องวันเกิดใช้ตัวเลือกวันที่จาก Angular Material และไม่ใช้ `input type="date"` ของเบราว์เซอร์
- **ขั้นตอน:** เปิดหน้า ตรวจชนิดของช่องวันเกิด กด `birth-date-toggle` แล้วตรวจหน้าต่าง `mat-datepicker-content`, `mat-calendar` และตารางปฏิทิน
- **ผลที่คาดหวัง:** ช่องไม่เป็น `type="date"` และปฏิทิน Angular Material เปิดให้เลือกวันได้
- **สืบย้อน:** `BR-PF-01`, `UIS-PF-01`, `UIX-PF-01`

### SEC-PF-001 — ปฏิเสธรูปที่ถอดรหัสแล้วเกิน 2 MiB

- **ประเภท:** Security / Negative / File size
- **ขั้นตอน:** ส่ง Base64 ที่ถอดรหัสแล้วเกิน 2 MiB แต่ request รวมไม่เกิน 3 MiB
- **ผลที่คาดหวัง:** API ตอบ `400` และระบุข้อผิดพลาดที่ `ProfileBase64`
- **สืบย้อน:** `QAR-PF-01-01`, `API-PF-01`

### SEC-PF-002 — ปฏิเสธ MIME ที่ไม่ตรงกับ byte signature

- **ประเภท:** Security / Negative / File signature
- **ขั้นตอน:** ประกาศ `image/png` แต่ส่ง byte signature ของ JPEG
- **ผลที่คาดหวัง:** API ตอบ `400` และไม่บันทึกข้อมูล
- **สืบย้อน:** `QAR-PF-01-01`, `BR-PF-01-07`, `API-PF-01`

### SEC-PF-003 — ปฏิเสธ request body เกิน 3 MiB

- **ประเภท:** Security / Negative / Resource limit
- **ขั้นตอน:** ส่ง JSON ผ่าน Nginx ที่มีขนาดเกิน 3 MiB
- **ผลที่คาดหวัง:** ตอบ `413` ก่อนเข้าสู่ตรรกะธุรกิจ
- **สืบย้อน:** `QAR-PF-01-02`, `API-PF-01`, `SV-PF-02`

### SEC-PF-011 — ปฏิเสธ GIF และ WebP

- **ประเภท:** Security / Negative / File type
- **ขั้นตอน:** เลือกไฟล์ GIF ผ่านหน้าเว็บ แล้วส่ง Base64 data URL ที่ประกาศชนิด `image/gif` และ `image/webp` ไปยัง `POST /api/profiles`
- **ผลที่คาดหวัง:** หน้าเว็บแสดง `Select a PNG or JPEG image.`; คำขอ API ทั้งสองตอบ `400`, ระบุข้อผิดพลาดที่ `ProfileBase64` และไม่สร้างระเบียน
- **สืบย้อน:** `BR-PF-01-07`, `QAR-PF-01-01`, `API-PF-01`

### SEC-PF-004 — จำกัดอัตราคำขอสร้างข้อมูล

- **ประเภท:** Security / Abuse / Rate limit
- **ขั้นตอน:** ส่ง POST จาก IP เดียวซ้ำจนเกิน 20 คำขอภายใน 1 นาที
- **ผลที่คาดหวัง:** อย่างน้อยหนึ่งคำขอตอบ `429` และคำขอที่เกินไม่ถูกเข้าคิว
- **สืบย้อน:** `QAR-PF-01-03`, `API-PF-01`, `SV-PF-02`

### SEC-PF-008 — ส่ง security headers ผ่าน Nginx

- **ประเภท:** Security / Configuration / Browser
- **ขั้นตอน:** เปิดหน้า `/` และเรียก `/api/occupations` ผ่านพอร์ต `4204` แล้วอ่าน response headers
- **ผลที่คาดหวัง:** ทั้งสองคำตอบมี CSP, `X-Content-Type-Options`, frame protection, Referrer Policy และ Permissions Policy
- **สืบย้อน:** `QAR-PF-01-04`, `SV-PF-02`, `DEP-PF-01`

## การรันและหลักฐาน

```bash
cd src/client
npm run test:e2e
```

ตัวรายงานจะเขียนผลล่าสุดลง `playwright-test-result.md` และบันทึก screenshot ของทุกกรณีใต้ `screenshots/` โดยตรง รหัสในผล ชื่อภาพ และชื่อ `test()` ใน `profile.spec.ts` ต้องตรงกัน
