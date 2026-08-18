---
doc_id: FR-CP-02
module: CANDIDATE_PROFILE
source_refs:
  - documentation/00-intake/source-register.md
---

# FR-CP-02 — อ่านข้อมูลหลักอาชีพ

## Statement

ระบบต้องเก็บอาชีพเป็นข้อมูลหลักใน PostgreSQL และให้หน้า Angular อ่านรายการที่ใช้งานอยู่จาก API โดยแต่ละรายการมี `code` สำหรับส่งกลับเมื่อสร้างโปรไฟล์และ `name` สำหรับแสดงต่อผู้ใช้

## ผลลัพธ์

- หน้าเว็บไม่กำหนดรายการอาชีพไว้ในรหัสโปรแกรม
- API ส่งเฉพาะรายการที่ `isActive = true` ตาม `displayOrder`
- คำสั่งสร้างโปรไฟล์รับ `occupationCode` และ API จับคู่เป็น foreign key ภายในฐานข้อมูล
