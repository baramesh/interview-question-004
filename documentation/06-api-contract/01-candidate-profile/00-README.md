---
doc_id: API-CP-INDEX
print_id: API-CP-INDEX
module: CANDIDATE_PROFILE
name_th: สารบัญ API Contract ของ Candidate Profile
name_en: candidate-profile-api-index
---

# Candidate Profile — API Contract

## ภาพรวม

โฟลเดอร์นี้เก็บ API Contract แบบหนึ่งไฟล์ต่อหนึ่ง route และ method ตามรูปแบบ `pea-docs/design-new/06-api-contract` โดยแยกตัวอย่าง request/response ไปไว้ใต้ `examples/{endpoint-file-stem}/` เนื้อหา API เป็นเจ้าของเฉพาะ interface contract ส่วนลำดับ runtime อ้าง `RV-CP-01` และขอบเขตความมั่นคงปลอดภัยอ้าง `SV-CP-01`

## รายการ Endpoint

| No. | doc_id      | api_key                        | Method | Route                     | Caller      | Runtime owner | ชื่อ                 |
| --- | ----------- | ------------------------------ | ------ | ------------------------- | ----------- | ------------- | -------------------- |
| 01  | `API-CP-01` | `cpf-candidate-profile-create` | `POST` | `/api/candidate-profiles` | `UIX-CP-01` | `RV-CP-01`    | สร้างโปรไฟล์ผู้สมัคร |
| 02  | `API-CP-02` | `cpf-occupation-list`          | `GET`  | `/api/occupations`        | `UIX-CP-01` | `RV-CP-01`    | อ่านข้อมูลหลักอาชีพ  |

## Contract ร่วมของโมดูล

- JSON ใช้ชื่อฟิลด์แบบ camelCase
- Endpoint นี้เป็น public local-test surface ตาม `SV-CP-01` และไม่มี authentication/authorization
- ข้อผิดพลาดจากการตรวจ payload ใช้ ASP.NET Core `ValidationProblemDetails`
- ไม่มี pagination, query string หรือ path parameter ในขอบเขตปัจจุบัน
- หน้าเว็บอ่านรายการอาชีพจาก `GET /api/occupations` และส่ง `occupationCode` กลับในคำสั่งสร้างโปรไฟล์

## อ้างอิง

| ประเภท         | doc_id      | เนื้อหาเจ้าของ                             |
| -------------- | ----------- | ------------------------------------------ |
| Requirement    | `FR-CP-01`  | ความสามารถสร้างโปรไฟล์                     |
| Business rule  | `BR-CP-01`  | กฎตรวจข้อมูล                               |
| Data           | `DDC-CP-01` | ความหมายและชนิดข้อมูล CandidateProfile     |
| Master data    | `DDC-CP-02` | ข้อมูลหลัก Occupation และ code ที่ API ใช้ |
| Runtime        | `RV-CP-01`  | ลำดับบันทึกโปรไฟล์                         |
| UI interaction | `UIX-CP-01` | การเรียก API จากปุ่ม Save profile          |
| Security       | `SV-CP-01`  | ขอบเขต public local test                   |
| QA             | `QAT-CP-01` | ระดับการทดสอบ                              |
