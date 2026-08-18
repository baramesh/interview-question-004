---
doc_id: UIS-CP-01
module: CANDIDATE_PROFILE
type: ui-screen
route: /
relates_to:
  - FR-CP-01
  - FR-CP-02
  - UIX-CP-01
---

# UIS-CP-01 — หน้าสร้างโปรไฟล์ผู้สมัคร

## โครงสร้างข้อมูล

| ส่วน                  | เนื้อหา                                                   |
| --------------------- | --------------------------------------------------------- |
| Intro                 | ชื่อหน้า คำอธิบาย และแนวทางสามข้อ                         |
| Candidate information | ฟิลด์ชื่อ นามสกุล อีเมล โทรศัพท์ วันเกิด อาชีพ เพศ และรูป |
| Actions               | Clear และ Save profile                                    |
| Feedback              | ข้อผิดพลาดรายฟิลด์ สถานะกำลังบันทึก และผลสำเร็จ/ล้มเหลว   |

## แหล่งข้อมูลอาชีพ

เมื่อเปิดหน้า ระบบเรียก `GET /api/occupations` และแสดง `name` ใน combo box โดยใช้ `code` เป็นค่าของตัวเลือก หากโหลดไม่สำเร็จ ระบบแสดง `occupation-load-error` และไม่ใช้รายการที่เขียนตายตัวใน Angular

## ตัวระบุทดสอบหลัก

`candidate-profile-page`, `candidate-profile-form`, `first-name-input`, `last-name-input`, `email-input`, `phone-input`, `birth-date-input`, `occupation-select`, `sex-radio-group`, `profile-image-input`, `clear-button`, `save-button`, `save-notification`

## การตอบสนองตามขนาดจอ

- ตั้งแต่ `1024px` แบ่ง Intro และแบบฟอร์มเป็นสองคอลัมน์
- ต่ำกว่า `1024px` เรียงสองส่วนในแนวตั้ง
- ต่ำกว่า `768px` ฟิลด์เรียงหนึ่งคอลัมน์
