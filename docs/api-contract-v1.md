# API Contract v1

Base URL:

- /api/v1

## 1. Departments (Khoa)

- GET /departments
- GET /departments/{id}
- POST /departments
- PUT /departments/{id}
- DELETE /departments/{id}
- PUT /departments/{id}/head-lecturer/{lecturerId}

POST/PUT body:

- maKhoa
- tenKhoa

## 2. Classes (LopHoc)

- GET /classes
- GET /classes/{id}
- POST /classes
- PUT /classes/{id}
- DELETE /classes/{id}
- PUT /classes/{id}/advisor/{lecturerId}

POST/PUT body:

- maLop
- tenLop
- khoaId

## 3. Students (SinhVien)

- GET /students
- GET /students/{id}
- POST /students
- PUT /students/{id}
- DELETE /students/{id}
- GET /students/search?keyword=
- GET /students/{id}/results

POST body:

- maSinhVien
- hoTen
- email
- ngaySinh
- lopHocId

PUT body:

- hoTen
- email
- ngaySinh
- lopHocId

## 4. Lecturers (GiangVien)

- GET /lecturers
- GET /lecturers/{id}
- POST /lecturers
- PUT /lecturers/{id}
- DELETE /lecturers/{id}

POST/PUT body:

- maGiangVien
- hoTen
- email
- khoaId

## 5. Semesters (HocKy)

- GET /semesters
- GET /semesters/{id}
- POST /semesters
- PUT /semesters/{id}
- DELETE /semesters/{id}

POST/PUT body:

- maHocKy
- tenHocKy
- namHoc
- ngayBatDau
- ngayKetThuc

## 6. Courses (MonHoc)

- GET /courses
- GET /courses/{id}
- POST /courses
- PUT /courses/{id}
- DELETE /courses/{id}

POST/PUT body:

- maMonHoc
- tenMon
- soTinChi
- khoaId
- giangVienGiangDayId
- hocKyId

## 7. Enrollments (DangKyHoc)

- GET /enrollments
- GET /enrollments/{id}
- POST /enrollments
- DELETE /enrollments/{id}
- PUT /enrollments/{id}/score
- PUT /enrollments/{id}/result/recalculate

POST body:

- sinhVienId
- monHocId
- hocKyId

PUT /score body:

- diem

## 8. Dashboard and lookup

- GET /dashboard/overview
- GET /lookups/departments
- GET /lookups/classes
- GET /lookups/lecturers
- GET /lookups/semesters
- GET /lookups/courses

## 9. Response envelope

Success:

{
  "success": true,
  "data": {},
  "message": "OK"
}

Error:

{
  "success": false,
  "code": "VALIDATION_ERROR",
  "message": "Input is invalid",
  "details": ["Email is required"],
  "traceId": "..."
}
