---
doc_id: API-PF-INDEX
print_id: API-PF-INDEX
module: PROFILE
name_th: สารบัญ API Contract ของ Profile
name_en: profile-api-index
---

# Profile — API Contract

## ภาพรวม

โฟลเดอร์นี้เก็บ API Contract แบบหนึ่งไฟล์ต่อหนึ่ง route และ method ตามรูปแบบ `pea-docs/design-new/06-api-contract` โดยแยกตัวอย่าง request/response ไปไว้ใต้ `examples/{endpoint-file-stem}/` เนื้อหา API เป็นเจ้าของเฉพาะ interface contract ส่วนลำดับ runtime อ้าง `RV-PF-01` และขอบเขตความมั่นคงปลอดภัยอ้าง `SV-PF-01`

## รายการ Endpoint

| No. | doc_id      | api_key              | Method | Route              | Caller      | Runtime owner | ชื่อ                        |
| --- | ----------- | -------------------- | ------ | ------------------ | ----------- | ------------- | --------------------------- |
| 01  | `API-PF-01` | `pf-profile-create`  | `POST` | `/api/profiles`    | `UIX-PF-01` | `RV-PF-01`    | สร้างโปรไฟล์ผู้กรอกแบบฟอร์ม |
| 02  | `API-PF-02` | `pf-occupation-list` | `GET`  | `/api/occupations` | `UIX-PF-01` | `RV-PF-01`    | อ่านข้อมูลหลักอาชีพ         |

## Contract ร่วมของโมดูล

- JSON ใช้ชื่อฟิลด์แบบ camelCase
- Endpoint นี้เป็น public local-test surface ตาม `SV-PF-01` และไม่มี authentication/authorization
- ข้อผิดพลาดจากการตรวจ payload ใช้ ASP.NET Core `ValidationProblemDetails`
- ไม่มี pagination, query string หรือ path parameter ในขอบเขตปัจจุบัน
- หน้าเว็บอ่านรายการอาชีพจาก `GET /api/occupations` และส่ง `occupationCode` กลับในคำสั่งสร้างโปรไฟล์

## อ้างอิง

| ประเภท         | doc_id      | เนื้อหาเจ้าของ                             |
| -------------- | ----------- | ------------------------------------------ |
| Requirement    | `FR-PF-01`  | ความสามารถสร้างโปรไฟล์                     |
| Business rule  | `BR-PF-01`  | กฎตรวจข้อมูล                               |
| Data           | `DDC-PF-01` | ความหมายและชนิดข้อมูล Profile              |
| Master data    | `DDC-PF-02` | ข้อมูลหลัก Occupation และ code ที่ API ใช้ |
| Runtime        | `RV-PF-01`  | ลำดับบันทึกโปรไฟล์                         |
| UI interaction | `UIX-PF-01` | การเรียก API จากปุ่ม Save profile          |
| Security       | `SV-PF-01`  | ขอบเขต public local test                   |
| QA             | `QAT-PF-01` | ระดับการทดสอบ                              |
