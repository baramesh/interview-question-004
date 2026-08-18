---
doc_id: API-CP-02
api_key: cpf-occupation-list
print_id: API-CP-02
module: CANDIDATE_PROFILE
name_th: อ่านข้อมูลหลักอาชีพ
name_en: get-occupations
http_method: GET
route: /api/occupations
runtime_view: RV-CP-01
security_view: SV-CP-01
caller_kind: ui-interaction
caller_refs:
  - UIX-CP-01
traces_up:
  - FR-CP-02
data_refs:
  - DDC-CP-02
traces_down:
  - RV-CP-01
  - UIX-CP-01
  - QAT-CP-01
---

# API-CP-02 — GET /api/occupations

## สรุป

ส่งรายการข้อมูลหลักอาชีพที่ใช้งานให้หน้า Angular สำหรับสร้างตัวเลือก โดยเรียงตาม `display_order` แล้วตาม `name` หน้าเว็บแสดง `name` และเก็บ `code` เป็นค่าที่ส่งกลับใน `POST /api/candidate-profiles`

## Caller / Consumer

| caller kind      | caller refs | trigger              | use                                                  |
| ---------------- | ----------- | -------------------- | ---------------------------------------------------- |
| `ui-interaction` | `UIX-CP-01` | เปิดหน้าสร้างโปรไฟล์ | เติม combo box อาชีพโดยไม่เขียนรายการตายตัวใน Client |

## Security

Endpoint เป็น public เฉพาะสภาพแวดล้อมทดสอบในเครื่องตาม `SV-CP-01` และไม่รับ access token

## Request Headers

ไม่มี header เฉพาะนอกเหนือจาก HTTP มาตรฐาน

## Path Parameters

ไม่มี

## Query String

ไม่มี

## Request Body

ไม่มี

## Processing Contract

1. อ่าน `occupations` แบบไม่ติดตามการเปลี่ยนแปลง
2. เลือกเฉพาะ `is_active = true`
3. เรียง `display_order` จากน้อยไปมาก แล้วเรียง `name`
4. คืนเฉพาะ `code` และ `name`; ไม่เปิดเผย `id`, `displayOrder` หรือ `isActive`

## Responses

### 200 OK

Media type: `application/json`

Schema: `array<OccupationOptionResponse>`

| field  | type   | required | source            | description                                | rule                             |
| ------ | ------ | -------- | ----------------- | ------------------------------------------ | -------------------------------- |
| `code` | string | yes      | `Occupation.code` | ค่าที่ Client ส่งกลับเป็น `occupationCode` | ยาวไม่เกิน 50 ตัวอักษรและ unique |
| `name` | string | yes      | `Occupation.name` | ข้อความที่แสดงใน combo box                 | ยาวไม่เกิน 100 ตัวอักษร          |

ตัวอย่าง: `examples/api-cp-02-get-occupations/occupation-list-response.example.yaml`

## Error Responses

| status                      | meaning                                     | body                  | Client behavior                                     |
| --------------------------- | ------------------------------------------- | --------------------- | --------------------------------------------------- |
| `500 Internal Server Error` | API หรือ PostgreSQL อ่านข้อมูลหลักไม่สำเร็จ | server error response | แสดง `occupation-load-error` และไม่สร้างรายการสำรอง |

Endpoint public นี้ไม่มี `401`, `403` หรือ `404`; ถ้าไม่มีรายการที่ใช้งานจะตอบ `200` พร้อม array ว่าง

## Observability and Data Handling

- payload ไม่มีข้อมูลส่วนบุคคล
- ห้ามเปิดเผย primary key `id` ผ่าน response
- ความผิดพลาดใช้ trace ของ ASP.NET Core โดยไม่บันทึกข้อมูลรับรองฐานข้อมูล

## Contract Tests

| Test Case ID    | สิ่งที่พิสูจน์                                              |
| --------------- | ----------------------------------------------------------- |
| `UT-API-CP-009` | ส่งเฉพาะข้อมูลหลักที่ใช้งานและเรียงตาม `display_order`      |
| `TC-CP-E2E-008` | API ส่งรายการตามลำดับและหน้าเว็บแสดง `name` ตรงกับ response |

## Changelog

| version | date       | change                                           |
| ------- | ---------- | ------------------------------------------------ |
| 1.0.0   | 2026-08-18 | เพิ่ม endpoint ข้อมูลหลักอาชีพสำหรับหน้า Angular |
