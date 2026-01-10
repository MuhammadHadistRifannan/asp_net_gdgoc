# 🚀 GDGOC-ASP.NET

![GDG Lockup](https://res.cloudinary.com/startup-grind/image/upload/dpr_2.0,fl_sanitize/v1/gcs/platform-data-goog/contentbuilder/GDG-Lockup-1Line-Black_vMWBFT9.svg)

![.NET Core](https://img.shields.io/badge/.NET%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![Status](https://img.shields.io/badge/Status-Active%20Dev-success?style=for-the-badge)

> **Build fast, but keep it clean.**

**GDGOC-ASP.NET** adalah **ASP.NET Core Web API** yang dirancang sebagai _backend foundation_ yang modern, modular, dan _scalable_. Proyek ini difokuskan untuk kebutuhan aplikasi dunia nyata seperti Auth Service, SaaS, atau produk digital lainnya dengan menerapkan **Clean Architecture** dan **Best Practices**.

---

## 📋 Table of Contents
- [Tech Stack](#-tech-stack)
- [Project Structure](#-project-structure)
- [Features & Roadmap](#-features--roadmap)
- [Installation & Setup](#-installation--setup)
- [API Documentation](#-api-documentation)
- [Design Decisions](#-design-decisions)
- [Author](#-author)

---

## 🧠 Tech Stack

Dibangun menggunakan teknologi dan library pilihan untuk performa dan kemudahan maintenance:

| Component | Technology |
| :--- | :--- |
| **Framework** | ASP.NET Core Web API |
| **Database** | PostgreSQL |
| **ORM** | Entity Framework Core |
| **Auth** | JWT Authentication (Bearer) |
| **Validation** | FluentValidation |
| **Containerization** | Docker |

---

## 📁 Project Structure

Struktur direktori disusun agar _loose coupling_ dan mudah untuk di-scale tanpa merusak kode lama.

```text
GDGOC-ASP.NET
│
├── Controllers/       # HTTP Layer (Endpoint API)
├── Database/          # DbContext & Database Configuration
├── Dto/               # Data Transfer Objects (Boundary)
├── Helper/            # Utilities & Helper Functions
├── Interfaces/        # Contracts & Abstractions
├── Migrations/        # EF Core Migrations History
├── Repository/        # Data Access Layer
│   └── User/          # Specific User Repository
├── Services/          # Business Logic Layer
│   └── JwtService.cs  # Token Handling Logic
├── Validator/         # FluentValidation Rules
│   └── User/          # User Validation Rules
│
├── appsettings.json   # Configuration File
├── Program.cs         # Entry Point & DI Container
├── Dockerfile         # Docker Image Setup
└── README.md          # Documentation
```

## 🧩 Features & Roadmap
```
✅ Current Features
[x] User Registration (Secure Password Hashing)

[x] User Login (Credential Verification)

[x] JWT Authentication (Secure Token Generation)

[x] Request Validation (FluentValidation pipeline)

[x] Repository Pattern implementation

[x] Clean Separation (Controller – Service – Repository)

[x] Global Error Handling Middleware

🔄 In Development (Roadmap)
[ ] Role & Permission Management (RBAC)

[ ] Refresh Token Mechanism


[ ] Centralized Logging & Monitoring (Serilog/ELK)

[ ] Unit Testing (xUnit/NUnit)

[ ] Rate Limiting

[ ] API Documentation (Swagger/OpenAPI Integration)
```
## ⚙️ Installation & Setup
Ikuti langkah berikut untuk menjalankan proyek di lingkungan lokal.

1. Prerequisites
Pastikan kamu sudah menginstall:
```
.NET SDK

PostgreSQL

Docker (Opsional)
```
2. Clone Repository
Bash
```bash
git clone [https://github.com/username/gdgoc-aspnet.git](https://github.com/username/gdgoc-aspnet.git)
cd gdgoc-aspnet
```
3. Configuration
Sesuaikan appsettings.json dengan kredensial database kamu:
```json
JSON

{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=mydb;Username=postgres;Password=yourpassword"
  },
  "Jwt": {
    "SecretKey": "YOUR_SUPER_SECRET_KEY_MUST_BE_LONG_ENOUGH",
    "Issuer": "gdgoc",
    "Issued_At": "gdgoc",
    "Audience": "gdgoc_user",
  }
}
```
⚠️ Note: Untuk production, selalu gunakan Environment Variables atau Secret Manager.

4. Database Setup
Jalankan migrasi untuk membuat tabel:

Bash
```bash
# Install tool jika belum ada
dotnet tool install --global dotnet-ef

# Jalankan migrasi
dotnet-ef migrations add InitialCreate
dotnet-ef database update
```
5. Run Application
Menggunakan .NET CLI:

Bash
```bash
dotnet restore
dotnet run
```
API akan berjalan di https://localhost:xxxx (sesuai launchSettings.json).

Menggunakan Docker:

Bash
```bash
docker build -t gdgoc-api .
docker run -p 8080:8080 gdgoc-api
```
## 🔌 API Documentation
Berikut adalah endpoint utama yang tersedia saat ini.

👤 User Module
1. Register User
Mendaftarkan pengguna baru ke dalam sistem.
```HTTP
URL: /users/register

Method: POST

Auth: None

Request Body:

JSON

{
  "email": "user@example.com",     // required
  "password": "securePassword123", // required
  "first_name": "John",            // optional
  "last_name": "Doe",              // optional
  "address": "Jakarta, Indonesia"  // optional
}
Response (Success):

JSON

{
    "message": "success"
}
```
2. Login User
Otentikasi pengguna dan mendapatkan JWT Token.

``` HTTP
URL: /users/login

Method: POST

Auth: None

Request Body:

JSON

{
  "email": "user@example.com",
  "password": "securePassword123"
}
Response (Success):

JSON

{
    "message": "success",
    "data": {
        "id": "hashed_string_id_example",
        "email": "user@example.com",
        "password": "$2a$11$ExampleHashedPassword...",
        "first_name": "John",
        "last_name": "Doe",
        "address": "Jakarta, Indonesia",
        "created_at": "2024-01-01T00:00:00Z",
        "updated_at": "2024-01-01T00:00:00Z"
    },
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```
3. Get User Profile
Mendapatkan informasi profil pengguna yang sedang login.

``` HTTP
URL: /users/profile

Method: GET

Auth: Bearer Token

Headers:

HTTP

Authorization: Bearer <YOUR_JWT_TOKEN>
Response (Success):

JSON

{
  "message": "success",
  "user": {
    "id": "hashed_string_id_example",
    "first_name": "John",
    "last_name": "Doe",
    "email": "user@example.com",
    "address": "Jakarta, Indonesia"
  }
}
```
## 🧪 Validation Strategy
Kami menerapkan Fail-Fast Strategy menggunakan library FluentValidation. Setiap request yang masuk akan divalidasi sebelum menyentuh Business Logic.

    LoginUserValidator

    RegisterUserValidator

    UpdateUserValidator

Jika input tidak valid, API akan langsung mengembalikan response error tanpa membebani database.

## 🧠 Design Decisions
Kenapa arsitektur ini dipilih?
```text
Separation of Concerns: Controller hanya menghandle HTTP request/response. Logic berat ada di Service.

DTO Pattern: Menggunakan DTO sebagai boundary untuk mencegah kebocoran struktur database ke client (Over-posting/Under-posting).

Repository Pattern: Abstraksi layer data memudahkan unit testing dan penggantian database di masa depan.

JWT Service Separation: Logic pembuatan token dipisah agar reusable dan rapi.
```

### 👤 Author
Developed by Hadist Backend Engineer | Game Dev | .NET Enthusiast

"Code should be easy to read, hard to misuse."