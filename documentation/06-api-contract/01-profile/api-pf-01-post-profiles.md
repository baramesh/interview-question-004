---
doc_id: API-PF-01
api_key: pf-profile-create
print_id: API-PF-01
module: PROFILE
name_th: สร้างโปรไฟล์ผู้กรอกแบบฟอร์ม
name_en: post-profiles
http_method: POST
route: /api/profiles
runtime_view: RV-PF-01
security_view: SV-PF-01
caller_kind: ui-interaction
caller_refs:
  - UIX-PF-01
traces_up:
  - FR-PF-01
  - BR-PF-01
data_refs:
  - DDC-PF-01
  - DDC-PF-02
traces_down:
  - RV-PF-01
  - UIX-PF-01
  - QAT-PF-01
---

# API-PF-01 — POST /api/profiles

## สรุป

รับข้อมูลโปรไฟล์จากหน้าสร้างโปรไฟล์ ตรวจ payload จับคู่ `occupationCode` กับข้อมูลหลักอาชีพที่ใช้งาน แล้วสร้าง `Profile` หนึ่งระเบียนใน PostgreSQL เมื่อสำเร็จจะคืนรหัส `id` ที่ฐานข้อมูลสร้างพร้อมข้อความ `save data success`

## Caller / Consumer

| caller kind      | caller refs | trigger                                                                 | use                                 |
| ---------------- | ----------- | ----------------------------------------------------------------------- | ----------------------------------- |
| `ui-interaction` | `UIX-PF-01` | ผู้กรอกแบบฟอร์มกดปุ่ม `save-button` เมื่อแบบฟอร์มผ่านการตรวจฝั่ง Client | สร้างโปรไฟล์และแสดงผลสำเร็จพร้อม ID |

ไม่มี scheduler, runtime ภายใน หรือระบบภายนอกเรียก endpoint นี้ในขอบเขต Test 1 ข้อ 4

## Security

Endpoint เป็น public สำหรับข้อสอบตาม `SV-PF-01` จึงไม่รับ access token และไม่มี permission guard การยืนยันตัวตนและการกำหนดสิทธิ์เป็น `OUT OF SCOPE` ส่วนมาตรการใน `QAR-PF-01` ยังต้องบังคับ ได้แก่ file signature, request-size limit, rate limit, security headers, safe error และการปิดพอร์ตไว้ที่ loopback

## Request Headers

| header         | type   | required | description         | rule                        |
| -------------- | ------ | -------- | ------------------- | --------------------------- |
| `Content-Type` | string | yes      | ชนิดเนื้อหา request | ต้องเป็น `application/json` |

## Path Parameters

ไม่มี

## Query String

ไม่มี

## Request Body

### Media Type

`application/json`

### Schema

`CreateProfileRequest`

| field            | type   | required | source        | persistence mapping                                      | description                   | rule                                                                                                                         |
| ---------------- | ------ | -------- | ------------- | -------------------------------------------------------- | ----------------------------- | ---------------------------------------------------------------------------------------------------------------------------- |
| `firstName`      | string | yes      | command input | `Profile.first_name`                                     | ชื่อผู้กรอกแบบฟอร์ม           | trim ก่อนบันทึก; ความยาว 1–100 ตัวอักษร                                                                                      |
| `lastName`       | string | yes      | command input | `Profile.last_name`                                      | นามสกุลผู้กรอกแบบฟอร์ม        | trim ก่อนบันทึก; ความยาว 1–100 ตัวอักษร                                                                                      |
| `email`          | string | yes      | command input | `Profile.email`                                          | อีเมลติดต่อ                   | ต้องผ่านรูปแบบอีเมล ยาวไม่เกิน 254 ตัวอักษร; trim และแปลงเป็นอักษรเล็กก่อนบันทึก                                             |
| `phone`          | string | yes      | command input | `Profile.phone`                                          | หมายเลขโทรศัพท์               | trim ก่อนบันทึก; ยาวไม่เกิน 30 ตัวอักษรและต้องตรงรูปแบบโทรศัพท์จาก `BR-PF-01`                                                |
| `profileBase64`  | string | yes      | command input | `Profile.profile_base64`                                 | รูปโปรไฟล์แบบ Base64 data URL | MIME ต้องเป็น `image/png` หรือ `image/jpeg`; Base64 ต้องถอดรหัสได้ มีขนาด 1 byte ถึง 2 MB และ byte signature ต้องตรงกับ MIME |
| `birthDate`      | string | yes      | command input | แปลงเป็น `Profile.birth_date` ชนิด `date`                | วันเกิด                       | ต้องเป็นวันที่อดีตในรูปแบบ `DD/MM/YYYY`                                                                                      |
| `occupationCode` | string | yes      | command input | ค้น `Occupation.code` แล้วบันทึก `Profile.occupation_id` | รหัสอาชีพจากข้อมูลหลัก        | trim และแปลงเป็นอักษรเล็ก; ยาวไม่เกิน 50 ตัวอักษร; ต้องตรงกับรายการที่ `is_active = true`                                    |
| `sex`            | string | yes      | command input | `Profile.sex`                                            | เพศ                           | รับเฉพาะ `Male` หรือ `Female`                                                                                                |

### Master Data Dependency

Client ต้องอ่าน `code` และ `name` จาก `GET /api/occupations` ตาม `API-PF-02` โดยแสดง `name` ต่อผู้ใช้และส่ง `code` ใน `occupationCode` ห้ามเขียนรายการอาชีพตายตัวไว้ใน Client

### Server-derived Fields

| field          | source                     | rule                                   |
| -------------- | -------------------------- | -------------------------------------- |
| `id`           | PostgreSQL identity column | ผู้เรียกห้ามส่ง ระบบสร้างเมื่อ INSERT  |
| `createdAtUtc` | API runtime                | ใช้เวลา UTC ณ ตอนสร้าง ผู้เรียกห้ามส่ง |

### ตัวอย่าง

- Request: `examples/api-pf-01-post-profiles/create-profile-request.example.yaml`

## Processing Contract

| ลำดับ | contract behavior                                                              | owner        |
| ----: | ------------------------------------------------------------------------------ | ------------ |
|     1 | ASP.NET Core binding และ DataAnnotations ตรวจ required, length และ email       | API contract |
|     2 | API ค้น `Occupation` ที่ `code` ตรงกับ `occupationCode` และ `is_active = true` | `DDC-PF-02`  |
|     3 | `IValidatableObject` ตรวจ phone, birthDate, sex และ Base64 image               | API contract |
|     4 | API ตรวจ byte signature ให้ตรงกับ MIME ที่ประกาศ                               | `QAR-PF-01`  |
|     5 | API trim ชื่อ นามสกุล อีเมล โทรศัพท์ และแปลงอีเมลเป็นอักษรเล็ก                 | API contract |
|     6 | EF Core เพิ่ม `Profile` พร้อม `occupation_id` และบันทึกหนึ่ง transaction       | `RV-PF-01`   |
|     7 | PostgreSQL สร้าง `id`; API คืน `201 Created`                                   | API contract |

## Idempotency

Endpoint นี้ไม่มี idempotency key การส่ง payload เดิมซ้ำถือเป็นคำสั่งสร้างใหม่และจะได้ระเบียนกับ `id` ใหม่ทุกครั้ง Client ต้องปิดปุ่มระหว่างรอผลเพื่อลดการส่งซ้ำจากการกดหลายครั้ง

## Responses

### 201 Created

#### Schema

`CreateProfileResponse`

| field     | type    | required | source                            | description                  | rule                                                                                 |
| --------- | ------- | -------- | --------------------------------- | ---------------------------- | ------------------------------------------------------------------------------------ |
| `id`      | integer | yes      | `Profile.id` ที่ PostgreSQL สร้าง | รหัสระเบียนที่แสดงหลังบันทึก | ต้องมากกว่า 0; การส่งค่านี้เป็นข้อกำหนดเฉพาะของ `No4.docx` แม้เป็น database identity |
| `message` | string  | yes      | transport response                | ข้อความผลสำเร็จ              | ค่าคงที่ `save data success`                                                         |

ตัวอย่าง: `examples/api-pf-01-post-profiles/profile-created-response.example.yaml`

## Error Responses

| status                      | meaning                                                                          | error_code               | body                       | description                                                         |
| --------------------------- | -------------------------------------------------------------------------------- | ------------------------ | -------------------------- | ------------------------------------------------------------------- |
| `400 Bad Request`           | JSON binding, ข้อมูลไม่ผ่านกฎ หรือ `occupationCode` ไม่ตรงกับข้อมูลหลักที่ใช้งาน | ไม่มีใน payload ปัจจุบัน | `ValidationProblemDetails` | ไม่เขียนระเบียนและคืนรายการข้อความแยกตามฟิลด์                       |
| `413 Content Too Large`     | request body เกิน 3 MiB                                                          | ไม่มี                    | reverse-proxy/API response | ปฏิเสธก่อนตรรกะธุรกิจและไม่เขียนระเบียน                             |
| `429 Too Many Requests`     | IP เดียวเรียก POST เกิน 20 ครั้งใน 1 นาที                                        | ไม่มี                    | empty response             | ไม่เข้าคิวและไม่เขียนระเบียน                                        |
| `500 Internal Server Error` | API หรือ PostgreSQL ล้มเหลวโดยไม่คาดหมาย                                         | ไม่มี                    | `ProblemDetails`           | มี `traceId`; ไม่มี detail, stack trace, SQL หรือ connection string |

Endpoint public นี้ไม่มี `401` และ `403`; ไม่มี uniqueness rule จึงไม่มี `409` ในขอบเขตปัจจุบัน

> ข้อจำกัดปัจจุบัน: API ยังไม่มี machine-readable `errorCode`; `413` ที่ Nginx ปฏิเสธและ `429` ไม่มี schema แบบ JSON

### ValidationProblemDetails

| field     | type                          | required | source                        | description                                                          |
| --------- | ----------------------------- | -------- | ----------------------------- | -------------------------------------------------------------------- |
| `type`    | string                        | yes      | ASP.NET Core                  | URI อ้างอิงประเภท HTTP error                                         |
| `title`   | string                        | yes      | ASP.NET Core                  | ชื่อข้อผิดพลาด โดยปกติเป็น `One or more validation errors occurred.` |
| `status`  | integer                       | yes      | ASP.NET Core                  | ค่า `400`                                                            |
| `errors`  | object<string, array[string]> | yes      | model validation              | key เป็นชื่อฟิลด์และ value เป็นรายการข้อความผิดพลาด                  |
| `traceId` | string                        | yes      | ASP.NET Core request activity | รหัสช่วยติดตามคำขอ ห้ามมีข้อมูลส่วนบุคคลหรือ Base64 image            |

ตัวอย่าง: `examples/api-pf-01-post-profiles/validation-problem-response.example.yaml`

### ProblemDetails สำหรับ 500

| field     | type    | required | rule                                                                   |
| --------- | ------- | -------- | ---------------------------------------------------------------------- |
| `type`    | string  | yes      | URI อ้างอิงประเภท HTTP error                                           |
| `title`   | string  | yes      | ค่า `An unexpected error occurred.`                                    |
| `status`  | integer | yes      | ค่า `500`                                                              |
| `traceId` | string  | yes      | ใช้ติดตาม log; ห้ามมีข้อมูลส่วนบุคคล Base64 SQL หรือ connection string |

## Observability and Data Handling

- `profileBase64` เป็นข้อมูลขนาดใหญ่และอาจเป็นข้อมูลส่วนบุคคล ห้ามบันทึก request body ทั้งก้อนลง log
- `traceId` ใช้เชื่อมเหตุการณ์ผิดพลาดกับ log ของ API โดยไม่เปิดเผย payload
- การเก็บและอายุข้อมูลรูปอยู่นอกขอบเขตโจทย์ทดสอบและต้องกำหนดเพิ่มก่อน production

## Contract Tests

| Test Case ID    | สิ่งที่พิสูจน์                                      |
| --------------- | --------------------------------------------------- |
| `TC-PF-E2E-005` | payload ถูกต้องตอบ `201`, คืน `id` และข้อความสำเร็จ |
| `TC-PF-E2E-006` | payload ผิดตอบ `400 ValidationProblemDetails`       |
| `UT-API-PF-010` | code อาชีพไม่ถูกต้องถูกปฏิเสธและไม่สร้างโปรไฟล์     |
| `UT-API-PF-011` | code อาชีพถูกจับคู่เป็น foreign key ก่อนบันทึก      |
| `UT-API-PF-012` | MIME และ byte signature ไม่ตรงกันถูกปฏิเสธ          |
| `SEC-PF-001`    | รูปที่ถอดรหัสเกิน 2 MiB ถูกปฏิเสธ                   |
| `SEC-PF-002`    | MIME และ byte signature ไม่ตรงกันถูกปฏิเสธ          |
| `SEC-PF-003`    | request body เกิน 3 MiB ถูกปฏิเสธ                   |
| `SEC-PF-004`    | คำขอเกิน 20 ครั้งต่อนาทีตอบ `429`                   |
| `SEC-PF-011`    | GIF และ WebP ถูกปฏิเสธด้วย `400`                    |

## Changelog

| version | date       | change                                                                                                                                                              |
| ------- | ---------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 2.2.0   | 2026-08-18 | เพิ่ม file-signature validation, request-size limit, rate limit และการสืบย้อนไป `QAR-PF-01`                                                                         |
| 2.1.0   | 2026-08-18 | เปลี่ยน `occupation` เป็น `occupationCode` และอ้างข้อมูลหลักจาก `API-PF-02`                                                                                         |
| 2.0.0   | 2026-08-18 | ปรับเป็นรูปแบบ endpoint-per-file เพิ่ม frontmatter, caller, security, schema tables, validation, persistence mapping, idempotency, error contract และ YAML examples |
| 1.0.0   | 2026-08-18 | สร้าง contract ฉบับย่อเริ่มต้น                                                                                                                                      |
