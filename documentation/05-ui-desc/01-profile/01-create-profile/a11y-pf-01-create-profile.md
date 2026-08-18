---
doc_id: A11Y-PF-01
module: PROFILE
type: accessibility
relates_to:
  - UIS-PF-01
---

# A11Y-PF-01 — การเข้าถึงหน้าสร้างโปรไฟล์

- ใช้ `main`, `section`, `form`, `fieldset` และ heading ตามลำดับ
- Angular Material เชื่อม label, input, error และ radio group
- ข้อความผลการบันทึกใช้ `role="status"`
- ปุ่มทุกปุ่มระบุ `type` ชัดเจนและเข้าถึงด้วยแป้นพิมพ์
- ช่องไฟล์มีปุ่มที่มองเห็นได้และ input จริงยังเข้าถึงผ่านโปรแกรมช่วยอ่านหน้าจอ
- สีข้อผิดพลาดมีข้อความกำกับ ไม่พึ่งสีเพียงอย่างเดียว
- ใช้ `section` และ heading ระดับสามแยก Profile photo, Personal details, Contact details และ Professional details
- คำชี้แจงฟิลด์บังคับใช้ดอกจันตรงกับ marker ใน label; control มีสถานะ `required` สำหรับโปรแกรมช่วยอ่านหน้าจอ
- รูปตัวอย่างมี alt `Selected profile preview`; avatar ที่ไม่มีรูปเป็นองค์ประกอบตกแต่งและไม่อ่านซ้ำ
- ปุ่ม Remove แสดงเฉพาะเมื่อมีรูปและระบุชื่อการกระทำชัดเจน
