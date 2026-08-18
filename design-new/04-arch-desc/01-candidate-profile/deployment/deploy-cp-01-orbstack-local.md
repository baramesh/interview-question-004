---
doc_id: DEP-CP-01
module: CANDIDATE_PROFILE
type: deployment-view
relates_to:
  - AD-CP-01
---

# DEP-CP-01 — การ deploy ทดสอบบน OrbStack

| บริการ   | image/build                       | พอร์ตเครื่อง | พอร์ต container | ที่เก็บถาวร                         |
| -------- | --------------------------------- | -----------: | --------------: | ----------------------------------- |
| client   | Angular build + Nginx 1.29 Alpine |         4204 |              80 | ไม่มี                               |
| api      | .NET 10 Alpine                    |         5004 |            8080 | ไม่มี                               |
| postgres | PostgreSQL 18.6 Alpine            |         5434 |            5432 | `postgres_data:/var/lib/postgresql` |

## การเริ่มและตรวจ

```bash
docker compose up -d --build
docker compose ps
curl http://localhost:5004/health
```

เปิด `http://localhost:4204` เพื่อทดสอบผ่าน Nginx ค่าใน `.env.example` ใช้เฉพาะเครื่องพัฒนาและต้องเปลี่ยนเมื่อใช้สภาพแวดล้อมอื่น
