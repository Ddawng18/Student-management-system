# Phase 4 - Entity, Repository, Service, Controller implementation

## Completed scope

- Implemented repository layer for all core entities:
  - Khoa, LopHoc, SinhVien, GiangVien, HocKy, MonHoc, DangKyHoc
- Implemented service layer with business methods from class diagram:
  - themSinhVien, capNhatSinhVien, xoaSinhVien
  - themMonHoc, capNhatMonHoc, xoaMonHoc
  - dangKyMonHoc, nhapDiem, tinhKetQua, xemKetQuaHocTap
- Implemented REST controllers for all required modules:
  - departments, classes, students, lecturers, semesters, courses, enrollments
  - lookups, dashboard overview

## Key implementation files

- Application contracts and DTOs:
  - src/StudentManagement.Application/DTOs
  - src/StudentManagement.Application/Interfaces
- Application services:
  - src/StudentManagement.Application/Services
- Infrastructure repositories:
  - src/StudentManagement.Infrastructure/Repositories
- API controllers:
  - src/StudentManagement.Api/Controllers

## Startup wiring completed

- Program.cs now registers both AddApplication and AddInfrastructure.
- Infrastructure DI registers all repository interfaces.

## Verification

1. Build success:

   dotnet build src/StudentManagement.Api/StudentManagement.Api.csproj

2. Runtime smoke test:

   dotnet run --project src/StudentManagement.Api/StudentManagement.Api.csproj

   GET http://localhost:5015/health -> {"status":"Healthy"}

## Notes for next phase

- Frontend integration (phase 5/6) will consume endpoints under /api/v1.
- Validation and refined error envelopes will be enhanced further in phase 7.
