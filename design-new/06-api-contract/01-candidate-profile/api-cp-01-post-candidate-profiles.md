---
doc_id: DNEW-API-CP-01
api_key: cpf-candidate-profile-create
print_id: API-CP-01
module: CANDIDATE_PROFILE
name_th: สร้างโปรไฟล์ผู้สมัคร
name_en: post-candidate-profiles
http_method: POST
route: /api/candidate-profiles
runtime_view: DNEW-RV-CP-01
security_view: DNEW-SV-CP-01
caller_kind: ui-interaction
caller_refs:
  - DNEW-UIX-CP-01
traces_up:
  - DNEW-FR-CP-01
  - DNEW-BR-CP-01
data_refs:
  - DNEW-DDC-CP-01
traces_down:
  - DNEW-RV-CP-01
  - DNEW-UIX-CP-01
  - DNEW-QAT-CP-01
---

# DNEW-API-CP-01 — POST /api/candidate-profiles

## สรุป

รับข้อมูลโปรไฟล์จากหน้าสร้างโปรไฟล์ ตรวจ payload แล้วสร้าง `CandidateProfile` หนึ่งระเบียนใน PostgreSQL เมื่อสำเร็จจะคืนรหัส `id` ที่ฐานข้อมูลสร้างพร้อมข้อความ `save data success`

## Caller / Consumer

| caller kind      | caller refs      | trigger                                                          | use                                 |
| ---------------- | ---------------- | ---------------------------------------------------------------- | ----------------------------------- |
| `ui-interaction` | `DNEW-UIX-CP-01` | ผู้สมัครกดปุ่ม `save-button` เมื่อแบบฟอร์มผ่านการตรวจฝั่ง Client | สร้างโปรไฟล์และแสดงผลสำเร็จพร้อม ID |

ไม่มี scheduler, runtime ภายใน หรือระบบภายนอกเรียก endpoint นี้ในขอบเขต Test 1 ข้อ 4

## Security

Endpoint เป็น public เฉพาะสภาพแวดล้อมทดสอบในเครื่องตาม `DNEW-SV-CP-01` จึงไม่รับ access token และไม่มี permission guard การนำไปใช้ภายนอกเครื่องหรือ production ต้องเพิ่ม authentication, authorization, HTTPS และการจัดการข้อมูลส่วนบุคคลก่อน

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

`CreateCandidateProfileRequest`

| field           | type   | required | source        | persistence mapping                                | description                   | rule                                                                                              |
| --------------- | ------ | -------- | ------------- | -------------------------------------------------- | ----------------------------- | ------------------------------------------------------------------------------------------------- |
| `firstName`     | string | yes      | command input | `CandidateProfile.first_name`                      | ชื่อผู้สมัคร                  | trim ก่อนบันทึก; ความยาว 1–100 ตัวอักษร                                                           |
| `lastName`      | string | yes      | command input | `CandidateProfile.last_name`                       | นามสกุลผู้สมัคร               | trim ก่อนบันทึก; ความยาว 1–100 ตัวอักษร                                                           |
| `email`         | string | yes      | command input | `CandidateProfile.email`                           | อีเมลติดต่อ                   | ต้องผ่านรูปแบบอีเมล ยาวไม่เกิน 254 ตัวอักษร; trim และแปลงเป็นอักษรเล็กก่อนบันทึก                  |
| `phone`         | string | yes      | command input | `CandidateProfile.phone`                           | หมายเลขโทรศัพท์               | trim ก่อนบันทึก; ยาวไม่เกิน 30 ตัวอักษรและต้องตรงรูปแบบโทรศัพท์จาก `BR-CP-01`                     |
| `profileBase64` | string | yes      | command input | `CandidateProfile.profile_base64`                  | รูปโปรไฟล์แบบ Base64 data URL | MIME ต้องเป็น PNG, JPEG, GIF หรือ WebP; ส่วนข้อมูล Base64 ต้องถอดรหัสได้และมีขนาด 1 byte ถึง 2 MB |
| `birthDate`     | string | yes      | command input | แปลงเป็น `CandidateProfile.birth_date` ชนิด `date` | วันเกิด                       | ต้องเป็นวันที่อดีตในรูปแบบ `DD/MM/YYYY`                                                           |
| `occupation`    | string | yes      | command input | `CandidateProfile.occupation`                      | อาชีพ                         | ต้องเป็นค่าหนึ่งในรายการอนุญาตด้านล่าง                                                            |
| `sex`           | string | yes      | command input | `CandidateProfile.sex`                             | เพศ                           | รับเฉพาะ `Male` หรือ `Female`                                                                     |

### Allowed Occupation Values

| value               |
| ------------------- |
| `Software Engineer` |
| `Business Analyst`  |
| `Quality Assurance` |
| `UX/UI Designer`    |
| `Project Manager`   |

### Server-derived Fields

| field          | source                     | rule                                   |
| -------------- | -------------------------- | -------------------------------------- |
| `id`           | PostgreSQL identity column | ผู้เรียกห้ามส่ง ระบบสร้างเมื่อ INSERT  |
| `createdAtUtc` | API runtime                | ใช้เวลา UTC ณ ตอนสร้าง ผู้เรียกห้ามส่ง |

### ตัวอย่าง

- Request: `examples/api-cp-01-post-candidate-profiles/create-candidate-profile-request.example.yaml`

## Processing Contract

| ลำดับ | contract behavior                                                            | owner           |
| ----: | ---------------------------------------------------------------------------- | --------------- |
|     1 | ASP.NET Core binding และ DataAnnotations ตรวจ required, length และ email     | API contract    |
|     2 | `IValidatableObject` ตรวจ phone, birthDate, occupation, sex และ Base64 image | API contract    |
|     3 | API trim ชื่อ นามสกุล อีเมล โทรศัพท์ และแปลงอีเมลเป็นอักษรเล็ก               | API contract    |
|     4 | EF Core เพิ่ม `CandidateProfile` และบันทึกหนึ่ง transaction                  | `DNEW-RV-CP-01` |
|     5 | PostgreSQL สร้าง `id`; API คืน `201 Created`                                 | API contract    |

## Idempotency

Endpoint นี้ไม่มี idempotency key การส่ง payload เดิมซ้ำถือเป็นคำสั่งสร้างใหม่และจะได้ระเบียนกับ `id` ใหม่ทุกครั้ง Client ต้องปิดปุ่มระหว่างรอผลเพื่อลดการส่งซ้ำจากการกดหลายครั้ง

## Responses

### 201 Created

#### Schema

`CreateCandidateProfileResponse`

| field     | type    | required | source                                     | description                  | rule                                                                                 |
| --------- | ------- | -------- | ------------------------------------------ | ---------------------------- | ------------------------------------------------------------------------------------ |
| `id`      | integer | yes      | `CandidateProfile.id` ที่ PostgreSQL สร้าง | รหัสระเบียนที่แสดงหลังบันทึก | ต้องมากกว่า 0; การส่งค่านี้เป็นข้อกำหนดเฉพาะของ `No4.docx` แม้เป็น database identity |
| `message` | string  | yes      | transport response                         | ข้อความผลสำเร็จ              | ค่าคงที่ `save data success`                                                         |

ตัวอย่าง: `examples/api-cp-01-post-candidate-profiles/candidate-profile-created-response.example.yaml`

## Error Responses

| status                      | meaning                                  | error_code                    | body                       | description                                                                      |
| --------------------------- | ---------------------------------------- | ----------------------------- | -------------------------- | -------------------------------------------------------------------------------- |
| `400 Bad Request`           | JSON binding หรือข้อมูลไม่ผ่านกฎ         | ไม่มีใน payload ปัจจุบัน      | `ValidationProblemDetails` | ไม่เขียนระเบียนและคืนรายการข้อความแยกตามฟิลด์                                    |
| `500 Internal Server Error` | API หรือ PostgreSQL ล้มเหลวโดยไม่คาดหมาย | ไม่มีสัญญาแบบตายตัวในปัจจุบัน | server error response      | ไม่รับรองว่าบันทึกสำเร็จ; Client แสดงข้อความทั่วไปและห้ามแสดงรายละเอียดฐานข้อมูล |

Endpoint public นี้ไม่มี `401` และ `403`; ไม่มี uniqueness rule จึงไม่มี `409` ในขอบเขตปัจจุบัน

> ข้อจำกัดปัจจุบัน: API ยังไม่มี machine-readable `errorCode` และยังไม่ได้กำหนด schema ของ `500` แบบตายตัว เอกสารจึงไม่สร้างค่าที่ runtime ไม่ได้ส่งจริง

### ValidationProblemDetails

| field     | type                          | required | source                        | description                                                          |
| --------- | ----------------------------- | -------- | ----------------------------- | -------------------------------------------------------------------- |
| `type`    | string                        | yes      | ASP.NET Core                  | URI อ้างอิงประเภท HTTP error                                         |
| `title`   | string                        | yes      | ASP.NET Core                  | ชื่อข้อผิดพลาด โดยปกติเป็น `One or more validation errors occurred.` |
| `status`  | integer                       | yes      | ASP.NET Core                  | ค่า `400`                                                            |
| `errors`  | object<string, array[string]> | yes      | model validation              | key เป็นชื่อฟิลด์และ value เป็นรายการข้อความผิดพลาด                  |
| `traceId` | string                        | yes      | ASP.NET Core request activity | รหัสช่วยติดตามคำขอ ห้ามมีข้อมูลส่วนบุคคลหรือ Base64 image            |

ตัวอย่าง: `examples/api-cp-01-post-candidate-profiles/validation-problem-response.example.yaml`

## Observability and Data Handling

- `profileBase64` เป็นข้อมูลขนาดใหญ่และอาจเป็นข้อมูลส่วนบุคคล ห้ามบันทึก request body ทั้งก้อนลง log
- `traceId` ใช้เชื่อมเหตุการณ์ผิดพลาดกับ log ของ API โดยไม่เปิดเผย payload
- การเก็บและอายุข้อมูลรูปอยู่นอกขอบเขตโจทย์ทดสอบและต้องกำหนดเพิ่มก่อน production

## Contract Tests

| Test Case ID    | สิ่งที่พิสูจน์                                      |
| --------------- | --------------------------------------------------- |
| `TC-CP-E2E-005` | payload ถูกต้องตอบ `201`, คืน `id` และข้อความสำเร็จ |
| `TC-CP-E2E-006` | payload ผิดตอบ `400 ValidationProblemDetails`       |

## Changelog

| version | date       | change                                                                                                                                                                  |
| ------- | ---------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 2.0.0   | 2026-08-18 | ปรับเป็นรูปแบบ PEA endpoint-per-file เพิ่ม frontmatter, caller, security, schema tables, validation, persistence mapping, idempotency, error contract และ YAML examples |
| 1.0.0   | 2026-08-18 | สร้าง contract ฉบับย่อเริ่มต้น                                                                                                                                          |
