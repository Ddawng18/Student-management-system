# Phase 2 - System Architecture Design

## 1. Target architecture overview

System is split into 2 deployable applications:

1. Backend API: ASP.NET Core Web API + EF Core + SQL Server
2. Frontend Web: Node.js + Express to serve static HTML/CSS/JS

Runtime flow:

- Browser loads frontend pages from Node server
- Frontend calls ASP.NET Core REST API with Fetch
- API reads/writes SQL Server through EF Core repositories/services

## 2. Backend architecture (layered)

Recommended folder structure for API project:

- src/StudentManagement.Api
  - Controllers
  - Middlewares
  - Extensions
  - Program.cs
  - appsettings.json
- src/StudentManagement.Application
  - DTOs
  - Interfaces
  - Services
  - Validators
  - Mapping
- src/StudentManagement.Domain
  - Entities
  - Enums
  - Common
- src/StudentManagement.Infrastructure
  - Persistence
    - AppDbContext.cs
    - Configurations
    - Migrations
  - Repositories
  - Seed

Layer responsibilities:

- Domain: pure entities and business rules without framework coupling
- Application: use-cases, service orchestration, DTO mapping, validations
- Infrastructure: EF Core implementation, repository classes, SQL concerns
- Api: HTTP endpoints, middleware, error mapping, dependency injection

## 3. Frontend architecture

Recommended structure under web-client:

- web-client
  - server.js
  - package.json
  - public
    - index.html
    - dashboard.html
    - students.html
    - lecturers.html
    - departments.html
    - classes.html
    - semesters.html
    - courses.html
    - enrollments.html
    - grades.html
    - assets
      - css
      - js
      - vendors

JS module split:

- assets/js/api.js: common fetch wrapper
- assets/js/common.js: shared UI helpers
- assets/js/{module}.js: page-specific scripts

## 4. Entity mapping from class diagram to DB + EF Core

- Nguoi (abstract class in C#)
  - shared fields: Id code, HoTen, Email
  - concrete entities:
    - SinhVien
    - GiangVien
- Khoa 1-n LopHoc
- LopHoc 1-n SinhVien
- Khoa 1-n GiangVien
- HocKy 1-n MonHoc
- SinhVien n-n MonHoc via DangKyHoc
- GiangVien 1-n MonHoc (teaching assignment)

## 5. Service contracts to implement

- QuanLySinhVienService
  - ThemSinhVien
  - CapNhatSinhVien
  - XoaSinhVien
  - LayDanhSachSinhVien
  - TimKiemSinhVien
- QuanLyMonHocService
  - ThemMonHoc
  - CapNhatMonHoc
  - XoaMonHoc
  - LayDanhSachMonHoc
- QuanLyDangKyService
  - DangKyMonHoc
  - NhapDiem
  - TinhKetQua
  - XemKetQuaHocTap

## 6. API principles

- RESTful resource naming
- Validation at API boundary + service level
- Consistent error envelope
- Pagination for list endpoints
- Idempotent update/delete handling

Standard error response:

- code
- message
- details
- traceId

## 7. Security and quality baseline

- CORS allowed for frontend origin only
- Input validation using FluentValidation
- Global exception middleware
- Serilog or built-in logging with structured scopes
- Swagger for contract verification

## 8. Build and run strategy

Local ports:

- API: 5000 (http), 5001 (https)
- Frontend: 3000

Environment files:

- backend appsettings.Development.json for SQL connection
- frontend .env for API_BASE_URL

## 9. Estimated effort for phase 2

- Architecture blueprint: 1.5h
- API contract breakdown: 1.0h
- Folder strategy and coding standards: 0.5h

Total estimate: 3 hours.
