---
doc_id: FR-CP-01
module: CANDIDATE_PROFILE
source_refs:
  - documentation/00-intake/source-register.md
---

# FR-CP-01 — สร้างโปรไฟล์ผู้สมัคร

## Statement

ระบบต้องให้ผู้สมัครกรอกข้อมูลส่วนตัว อัปโหลดรูปโปรไฟล์ และบันทึกเป็นระเบียนใหม่ใน PostgreSQL ได้ เมื่อบันทึกสำเร็จระบบต้องแจ้ง `save data success` พร้อมรหัสที่ฐานข้อมูลสร้างและล้างแบบฟอร์ม

## ข้อมูลที่รับ

`firstName`, `lastName`, `email`, `phone`, `profileBase64`, `birthDate`, `occupationCode` และ `sex`

ค่า `occupationCode` ต้องมาจากข้อมูลหลักที่ API ส่งให้หน้าเว็บตาม `FR-CP-02`
