---
doc_id: DNEW-API-CP-01
module: CANDIDATE_PROFILE
type: api-contract
method: POST
path: /api/candidate-profiles
caller_kind: ui-interaction
caller_refs:
  - DNEW-UIX-CP-01
relates_to:
  - DNEW-FR-CP-01
  - DNEW-DDC-CP-01
---

# API-CP-01 — สร้าง Candidate Profile

## Request

```json
{
  "firstName": "Ada",
  "lastName": "Lovelace",
  "email": "ada@example.com",
  "phone": "+66 81 234 5678",
  "profileBase64": "data:image/png;base64,iVBORw0KGgo=",
  "birthDate": "18/08/1990",
  "occupation": "Software Engineer",
  "sex": "Female"
}
```

ใช้กฎจาก `BR-CP-01`; ไม่รับ `id` หรือ `createdAtUtc` จากผู้เรียก

## Success — 201 Created

```json
{ "id": 1, "message": "save data success" }
```

## Error Responses

| status | ความหมาย | รูปแบบ |
|---|---|---|
| 400 | payload ไม่ผ่านการตรวจ | ASP.NET Core `ValidationProblemDetails` |
| 500 | persistence หรือ runtime ล้มเหลว | ไม่เปิดเผยรายละเอียดฐานข้อมูลแก่ผู้ใช้ |
