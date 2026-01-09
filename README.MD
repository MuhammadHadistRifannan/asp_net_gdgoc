# GDGOC-ASP.NET 🚀  
![A screenshot of the Markdown syntax guide](https://res.cloudinary.com/startup-grind/image/upload/dpr_2.0,fl_sanitize/v1/gcs/platform-data-goog/contentbuilder/GDG-Lockup-1Line-Black_vMWBFT9.svg)
> Ongoing Development Project

GDGOC-ASP.NET adalah **ASP.NET Core Web API** yang saat ini **sedang dalam tahap pengembangan aktif**, difokuskan sebagai backend yang clean, modular, dan siap di-scale untuk kebutuhan aplikasi modern (auth service, SaaS, atau produk digital).

Project ini dikembangkan dengan mindset:
> *build fast, but keep it clean.*

---

## 📌 Status Project

🟢 **Active Development**  
Beberapa fitur sudah stabil, sebagian masih dalam tahap iterasi dan peningkatan.

---

## 🧠 Tech Stack

- **ASP.NET Core Web API**
- **Entity Framework Core**
- **JWT Authentication**
- **FluentValidation**
- **PostgreSQL**
- **Docker (Development & Deployment)**

---

## 🎯 Tujuan Project

- Menjadi **backend foundation** untuk aplikasi real-world
- Menerapkan **best practice ASP.NET Core**

---

## 📁 Struktur Project

GDGOC-ASP.NET
│
├── Controllers/ # HTTP Layer (endpoint API)
├── Database/ # DbContext & database configuration
├── dto/ # Data Transfer Object
├── helper/ # Helper & utility
├── Interfaces/ # Interface / contract
├── Migrations/ # EF Core migrations
├── Repository/
│ └── User/ # User data access layer
├── Services/
│ └── JwtService.cs # JWT token handling
├── Validator/
│ └── user/ # FluentValidation rules
│
├── appsettings.json # Application config
├── Program.cs # Entry point
├── Dockerfile # Docker setup
└── README.md


### Kenapa struktur ini dipakai?
- Mudah dikembangkan
- Logic terpisah rapi
- Tidak vendor-lock
- Siap untuk refactor besar tanpa chaos

---

## 🔐 Fitur yang Sudah Ada

- ✅ User Registration
- ✅ User Login
- ✅ JWT Authentication
- ✅ Request Validation (FluentValidation)
- ✅ Repository Pattern
- ✅ Clean separation antara Controller – Service – Repository

---

## 🧩 Fitur dalam Pengembangan

- 🔄 Role & Permission
- 🔄 Refresh Token
- 🔄 Global Error Handling
- 🔄 Logging & Monitoring
- 🔄 Unit Test
- 🔄 Rate Limiting
- 🔄 API Documentation (Swagger improvement)

> Roadmap ini masih bisa berubah seiring kebutuhan dan feedback.

---

## ⚙️ Konfigurasi Aplikasi

### 1️⃣ Clone Project
```bash
git clone https://github.com/username/gdgoc-aspnet.git
cd gdgoc-aspnet
```

2️⃣ Konfigurasi appsettings.json

```
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=mydb;Username=postgres;Password=postgres"
  },
  "Jwt": {
    "Key": "SUPER_SECRET_KEY",
    "Issuer": "gdgoc",
    "Audience": "gdgoc_user",
    "ExpireMinutes": 60
  }
} 
```


⚠️ Untuk production, gunakan environment variable atau secret manager.

🗄️ Database Setup
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Pastikan database server aktif sebelum menjalankan perintah di atas.

▶️ Menjalankan Project (Local)
```C#
dotnet restore
dotnet run
```

Endpoint:
```HTTP
https://localhost:xxxx
```

🐳 Menjalankan dengan Docker (Development)
```docker
docker build -t gdgoc-api .
docker run -p 8080:8080 gdgoc-api
```


```json
🔌 User Endpoint
Login User
POST /users/login

Request body:
{
  "email" required,
  "password" required
}

Response:
{
    "message": "success",
    "data": {
        "id": <HashedString>,
        "email": <Email>,
        "password": <HashedString>,
        "first_name": <string>,
        "last_name": <string>,
        "address": <string>,
        "created_at": <date>,
        "updated_at": <date>
  },
  "token": <JWT_TOKEN>
}


Regist User
POST /users/register

Request body:
{
  "email" required,
  "password" required,
  "first_name" optional, 
  "last_name" optional ,
  "address" optional 
}

Response:
{
    "message": "success"
}

Profile User 
GET /users/profile

Header : Bearer <JWT_TOKEN>

Response: 
{
  "message": "success",
  "user": {
    "id": <HASHED_STRING>,
    "first_name": <string>,
    "last_name": <string>,
    "email": <Email>,
    "address": <string>
  }
}

```
🧪 Validation Strategy

```
  -  Semua request user divalidasi menggunakan FluentValidation:

   - LoginUserValidator

   - RegisterUserValidator

   - UpdateUserValidator

   - Invalid input langsung ditolak sebelum masuk ke business logic.
```
🧠 Design Decisions

    Controller tidak menyimpan logic berat

    JWT logic dipisah ke service

    DTO digunakan sebagai boundary

    Validation dilakukan sebelum logic berjalan

    Repository sebagai abstraction data layer

## 📌 Notes

##### Project ini masih terus berkembang.
Refactor dan improvement akan dilakukan secara bertahap seiring kebutuhan produk.

👤 Author

Developed by Hadist
Backend Engineer | Game Dev | .NET Enthusiast

### “Code should be easy to read, hard to misuse.”