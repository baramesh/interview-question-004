---
doc_id: UIS-PF-01
module: PROFILE
type: ui-screen
route: /
relates_to:
  - FR-PF-01
  - FR-PF-02
  - UIX-PF-01
---

# UIS-PF-01 — หน้าสร้างโปรไฟล์ผู้กรอกแบบฟอร์ม

## โครงสร้างข้อมูล

หน้าจอใช้รูปแบบแบบฟอร์มบุคลากรขององค์กร เน้นการสแกนข้อมูลเป็นหมวด ไม่ใช้แผงประชาสัมพันธ์หรือกล่องอัปโหลดทรงแข็งเป็นองค์ประกอบหลัก

| ลำดับ | ส่วน                 | เนื้อหา                                                                     | Test ID                        |
| ----: | -------------------- | --------------------------------------------------------------------------- | ------------------------------ |
|     1 | Page header          | ชื่อระบบ ชื่อหน้า และคำชี้แจง `Fields marked * are required`                | `profile-page`                 |
|     2 | Profile photo        | avatar วงกลม รูปตัวอย่าง ชื่อไฟล์ ข้อกำหนดไฟล์ ปุ่ม Upload photo และ Remove | `profile-photo-section`        |
|     3 | Personal details     | ชื่อ นามสกุล วันเกิดผ่าน Angular Material Datepicker และเพศ                 | `personal-details-section`     |
|     4 | Contact details      | อีเมลและโทรศัพท์                                                            | `contact-details-section`      |
|     5 | Professional details | อาชีพจากข้อมูลหลัก                                                          | `professional-details-section` |
|     6 | Form actions         | Clear form และ Save profile ชิดขวาท้ายแบบฟอร์ม                              | `clear-button`, `save-button`  |
|     7 | Feedback             | ข้อผิดพลาดรายฟิลด์ สถานะกำลังบันทึก และ Toast ผลสำเร็จ/ล้มเหลว              | `mat-snack-bar-container`      |

## Visual Language

- ใช้ Google Sans น้ำหนัก 400, 500, 600 และ 700 โดยรวมไฟล์ฟอนต์ไว้ใน Angular build ผ่าน Fontsource
- พื้นหลังใช้สีเทาอมเขียวอ่อน เนื้อหาอยู่ใน card สีขาว เส้นแบ่งหมวดและพื้นที่ว่างทำหน้าที่สร้างลำดับชั้น
- avatar ขนาดไม่น้อยกว่า 88px แสดงรูปแบบ `object-cover`; เมื่อยังไม่มีรูปใช้ไอคอน `person_outline` จาก Angular Material และไม่ใช้ตัวอักษรย่อภายใน
- ช่องอัปโหลดรูปอนุญาตเฉพาะ `image/png` และ `image/jpeg`; แสดงข้อความ `PNG or JPEG. Maximum file size 2 MB.`
- แต่ละหมวดมีหมายเลขและชื่อหมวด; แสดงคำอธิบายเฉพาะเมื่อช่วยให้ผู้ใช้กรอกได้ถูกต้อง โดย field อยู่ด้านขวาบน desktop และเรียงลงบน mobile
- หน้า production แสดง `Example.com` และ `Profile` เพียงระดับละหนึ่งตำแหน่ง ไม่แสดง `Interview Question 004`, ชื่อบุคคลจากข้อมูลทดสอบ, ป้าย `Profile management`, ป้าย `Personal profile` หรือคำอธิบายที่เพียงทวนชื่อหมวด
- ใช้สีเขียวกับสิ่งที่กดได้หรือสถานะสำเร็จ สีแดงใช้เฉพาะข้อผิดพลาดและเครื่องหมายฟิลด์บังคับ

## Toast ผลการบันทึก

- ใช้ `MatSnackBar` ของ Angular Material แทนกล่องข้อความแบบตรึงในหน้า จึงไม่ดันโครงสร้างแบบฟอร์ม
- ผลสำเร็จแสดง `save data success · ID: {id}` มุมขวาบน 5 วินาที พร้อมปุ่ม `Close`
- ผลล้มเหลวแสดงข้อผิดพลาด 7 วินาทีโดยไม่ล้างข้อมูลที่กรอก

## Required-field Indicator

- ข้อความส่วนหัวต้องใช้ `Fields marked * are required` พร้อมดอกจันสีแดง ห้ามใช้จุดแดงแทน
- label ของทุกฟิลด์บังคับต้องแสดง `*` จาก Angular Material ให้ตรงกับคำชี้แจง
- ห้ามสื่อว่าบังคับกรอกด้วยสีเพียงอย่างเดียว; โปรแกรมช่วยอ่านหน้าจอต้องรับรู้สถานะ required จาก control

## ตัวเลือกวันเกิด

- ใช้ `MatDatepicker` กับ `@angular/material-date-fns-adapter` และ date-fns เป็นตัวแปลงวันที่ของไลบรารี
- ช่องวันเกิดห้ามใช้ `input type="date"` หรือตัวเลือกวันที่ดั้งเดิมของเบราว์เซอร์
- แสดงและส่งค่าเป็น `DD/MM/YYYY`; กำหนดวันสูงสุดเป็นเมื่อวานเพื่อให้เลือกได้เฉพาะวันที่ในอดีต
- ต้องมีปุ่มเปิดปฏิทินที่เข้าถึงด้วยแป้นพิมพ์และโปรแกรมช่วยอ่านหน้าจอได้

## แหล่งข้อมูลอาชีพ

เมื่อเปิดหน้า ระบบเรียก `GET /api/occupations` และแสดง `name` ใน combo box โดยใช้ `code` เป็นค่าของตัวเลือก หากโหลดไม่สำเร็จ ระบบแสดง `occupation-load-error` และไม่ใช้รายการที่เขียนตายตัวใน Angular

## ตัวระบุทดสอบหลัก

`profile-page`, `profile-form`, `profile-photo-section`, `personal-details-section`, `contact-details-section`, `professional-details-section`, `first-name-input`, `last-name-input`, `email-input`, `phone-input`, `birth-date-input`, `birth-date-toggle`, `occupation-select`, `sex-radio-group`, `profile-image-input`, `remove-image-button`, `clear-button`, `save-button`

## การตอบสนองตามขนาดจอ

- ตั้งแต่ `900px` แต่ละหมวดแบ่งคำอธิบายและข้อมูลเป็นสองคอลัมน์; ฟิลด์ภายในแบ่งได้สองคอลัมน์
- ต่ำกว่า `900px` คำอธิบายหมวดและข้อมูลเรียงลงด้านล่าง
- ต่ำกว่า `640px` ฟิลด์และชุดปุ่มเรียงหนึ่งคอลัมน์ โดยไม่มีการเลื่อนแนวนอน
