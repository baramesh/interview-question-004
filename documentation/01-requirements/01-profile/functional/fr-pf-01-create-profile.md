---
doc_id: FR-PF-01
module: PROFILE
source_refs:
  - documentation/00-intake/source-register.md
---

# FR-PF-01 — สร้างโปรไฟล์ผู้กรอกแบบฟอร์ม

## Statement

ระบบต้องให้ผู้กรอกแบบฟอร์มกรอกข้อมูลส่วนตัว อัปโหลดรูปโปรไฟล์ และบันทึกเป็นระเบียนใหม่ใน PostgreSQL ได้ เมื่อบันทึกสำเร็จระบบต้องแจ้ง `save data success` พร้อมรหัสที่ฐานข้อมูลสร้างและล้างแบบฟอร์ม

## ข้อมูลที่รับ

`firstName`, `lastName`, `email`, `phone`, `profileBase64`, `birthDate`, `occupationCode` และ `sex`

ค่า `occupationCode` ต้องมาจากข้อมูลหลักที่ API ส่งให้หน้าเว็บตาม `FR-PF-02`
