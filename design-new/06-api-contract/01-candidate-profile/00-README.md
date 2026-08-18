---
doc_id: DNEW-API-CP-INDEX
print_id: API-CP-INDEX
module: CANDIDATE_PROFILE
name_th: สารบัญ API Contract ของ Candidate Profile
name_en: candidate-profile-api-index
---

# Candidate Profile — API Contract

## ภาพรวม

โฟลเดอร์นี้เก็บ API Contract แบบหนึ่งไฟล์ต่อหนึ่ง route และ method ตามรูปแบบ `pea-docs/design-new/06-api-contract` โดยแยกตัวอย่าง request/response ไปไว้ใต้ `examples/{endpoint-file-stem}/` เนื้อหา API เป็นเจ้าของเฉพาะ interface contract ส่วนลำดับ runtime อ้าง `DNEW-RV-CP-01` และขอบเขตความมั่นคงปลอดภัยอ้าง `DNEW-SV-CP-01`

## รายการ Endpoint

| No. | doc_id           | api_key                        | Method | Route                     | Caller           | Runtime owner   | ชื่อ                 |
| --- | ---------------- | ------------------------------ | ------ | ------------------------- | ---------------- | --------------- | -------------------- |
| 01  | `DNEW-API-CP-01` | `cpf-candidate-profile-create` | `POST` | `/api/candidate-profiles` | `DNEW-UIX-CP-01` | `DNEW-RV-CP-01` | สร้างโปรไฟล์ผู้สมัคร |

## Contract ร่วมของโมดูล

- JSON ใช้ชื่อฟิลด์แบบ camelCase
- Endpoint นี้เป็น public local-test surface ตาม `DNEW-SV-CP-01` และไม่มี authentication/authorization
- ข้อผิดพลาดจากการตรวจ payload ใช้ ASP.NET Core `ValidationProblemDetails`
- ไม่มี pagination, query string หรือ path parameter ในขอบเขตปัจจุบัน

## อ้างอิง

| ประเภท         | doc_id           | เนื้อหาเจ้าของ                         |
| -------------- | ---------------- | -------------------------------------- |
| Requirement    | `DNEW-FR-CP-01`  | ความสามารถสร้างโปรไฟล์                 |
| Business rule  | `DNEW-BR-CP-01`  | กฎตรวจข้อมูล                           |
| Data           | `DNEW-DDC-CP-01` | ความหมายและชนิดข้อมูล CandidateProfile |
| Runtime        | `DNEW-RV-CP-01`  | ลำดับบันทึกโปรไฟล์                     |
| UI interaction | `DNEW-UIX-CP-01` | การเรียก API จากปุ่ม Save profile      |
| Security       | `DNEW-SV-CP-01`  | ขอบเขต public local test               |
| QA             | `DNEW-QAT-CP-01` | ระดับการทดสอบ                          |
