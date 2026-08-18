---
doc_id: DEP-PF-01
module: PROFILE
type: deployment-view
relates_to:
  - AD-PF-01
---

# DEP-PF-01 — การ deploy ทดสอบบน OrbStack

| บริการ   | image/build                       | ที่อยู่เครื่อง   | พอร์ต container | ที่เก็บถาวร                         |
| -------- | --------------------------------- | ---------------- | --------------: | ----------------------------------- |
| client   | Angular build + Nginx 1.29 Alpine | `127.0.0.1:4204` |              80 | ไม่มี                               |
| api      | .NET 10 Alpine                    | `127.0.0.1:5004` |            8080 | ไม่มี                               |
| postgres | PostgreSQL 18.6 Alpine            | `127.0.0.1:5434` |            5432 | `postgres_data:/var/lib/postgresql` |

ทุกพอร์ตผูกกับ loopback เพื่อไม่เปิดรับจากเครือข่ายภายนอกเครื่อง Nginx จำกัด request body ที่ 3 MiB และเพิ่ม security headers สำหรับคำตอบหน้าเว็บและ API

## การเริ่มและตรวจ

```bash
docker compose up -d --build
docker compose ps
curl http://localhost:5004/health
```

เปิด `http://localhost:4204` เพื่อทดสอบผ่าน Nginx ค่าใน `.env.example` ใช้เฉพาะเครื่องพัฒนาและต้องเปลี่ยนเมื่อใช้สภาพแวดล้อมอื่น
