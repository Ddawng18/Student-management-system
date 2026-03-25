-- Student Management System - Seed Data
USE StudentManagementSystemDb;
GO

INSERT INTO dbo.Khoa (MaKhoa, TenKhoa)
VALUES
(N'CNTT', N'Cong nghe thong tin'),
(N'QTKD', N'Quan tri kinh doanh'),
(N'KT', N'Ke toan');
GO

INSERT INTO dbo.GiangVien (MaGiangVien, HoTen, Email, KhoaId)
VALUES
(N'GV001', N'Nguyen Van A', N'gva@example.edu.vn', (SELECT KhoaId FROM dbo.Khoa WHERE MaKhoa = N'CNTT')),
(N'GV002', N'Tran Thi B', N'gvb@example.edu.vn', (SELECT KhoaId FROM dbo.Khoa WHERE MaKhoa = N'CNTT')),
(N'GV003', N'Le Van C', N'gvc@example.edu.vn', (SELECT KhoaId FROM dbo.Khoa WHERE MaKhoa = N'QTKD'));
GO

UPDATE dbo.Khoa
SET TruongKhoaId = gv.GiangVienId
FROM dbo.Khoa k
INNER JOIN dbo.GiangVien gv ON gv.MaGiangVien = N'GV001'
WHERE k.MaKhoa = N'CNTT';
GO

INSERT INTO dbo.LopHoc (MaLop, TenLop, KhoaId, CoVanHocTapId)
VALUES
(N'DH22TH1', N'Dai hoc 2022 - Thuc hanh 1',
    (SELECT KhoaId FROM dbo.Khoa WHERE MaKhoa = N'CNTT'),
    (SELECT GiangVienId FROM dbo.GiangVien WHERE MaGiangVien = N'GV001')),
(N'DH22TH2', N'Dai hoc 2022 - Thuc hanh 2',
    (SELECT KhoaId FROM dbo.Khoa WHERE MaKhoa = N'CNTT'),
    (SELECT GiangVienId FROM dbo.GiangVien WHERE MaGiangVien = N'GV002'));
GO

INSERT INTO dbo.SinhVien (MaSinhVien, HoTen, Email, NgaySinh, LopHocId)
VALUES
(N'SV001', N'Pham Minh Bao', N'sv001@example.edu.vn', '2004-03-12', (SELECT LopHocId FROM dbo.LopHoc WHERE MaLop = N'DH22TH1')),
(N'SV002', N'Nguyen Thi Lan', N'sv002@example.edu.vn', '2004-11-22', (SELECT LopHocId FROM dbo.LopHoc WHERE MaLop = N'DH22TH1')),
(N'SV003', N'Tran Quoc Huy', N'sv003@example.edu.vn', '2004-06-09', (SELECT LopHocId FROM dbo.LopHoc WHERE MaLop = N'DH22TH2'));
GO

INSERT INTO dbo.HocKy (MaHocKy, TenHocKy, NamHoc, NgayBatDau, NgayKetThuc)
VALUES
(N'HK1-2025', N'Hoc ky 1', 2025, '2025-09-01', '2026-01-15'),
(N'HK2-2025', N'Hoc ky 2', 2025, '2026-02-01', '2026-06-15');
GO

INSERT INTO dbo.MonHoc (MaMonHoc, TenMon, SoTinChi, KhoaId, GiangVienGiangDayId, HocKyId)
VALUES
(N'CS101', N'Nhap mon lap trinh', 3,
    (SELECT KhoaId FROM dbo.Khoa WHERE MaKhoa = N'CNTT'),
    (SELECT GiangVienId FROM dbo.GiangVien WHERE MaGiangVien = N'GV001'),
    (SELECT HocKyId FROM dbo.HocKy WHERE MaHocKy = N'HK1-2025')),
(N'CS102', N'Cau truc du lieu va giai thuat', 4,
    (SELECT KhoaId FROM dbo.Khoa WHERE MaKhoa = N'CNTT'),
    (SELECT GiangVienId FROM dbo.GiangVien WHERE MaGiangVien = N'GV002'),
    (SELECT HocKyId FROM dbo.HocKy WHERE MaHocKy = N'HK1-2025')),
(N'BUS101', N'Quan tri hoc co ban', 3,
    (SELECT KhoaId FROM dbo.Khoa WHERE MaKhoa = N'QTKD'),
    (SELECT GiangVienId FROM dbo.GiangVien WHERE MaGiangVien = N'GV003'),
    (SELECT HocKyId FROM dbo.HocKy WHERE MaHocKy = N'HK2-2025'));
GO

INSERT INTO dbo.DangKyHoc (SinhVienId, MonHocId, HocKyId, Diem)
VALUES
((SELECT SinhVienId FROM dbo.SinhVien WHERE MaSinhVien = N'SV001'),
 (SELECT MonHocId FROM dbo.MonHoc WHERE MaMonHoc = N'CS101'),
 (SELECT HocKyId FROM dbo.HocKy WHERE MaHocKy = N'HK1-2025'),
 8.00),
((SELECT SinhVienId FROM dbo.SinhVien WHERE MaSinhVien = N'SV001'),
 (SELECT MonHocId FROM dbo.MonHoc WHERE MaMonHoc = N'CS102'),
 (SELECT HocKyId FROM dbo.HocKy WHERE MaHocKy = N'HK1-2025'),
 6.50),
((SELECT SinhVienId FROM dbo.SinhVien WHERE MaSinhVien = N'SV002'),
 (SELECT MonHocId FROM dbo.MonHoc WHERE MaMonHoc = N'CS101'),
 (SELECT HocKyId FROM dbo.HocKy WHERE MaHocKy = N'HK1-2025'),
 4.75),
((SELECT SinhVienId FROM dbo.SinhVien WHERE MaSinhVien = N'SV003'),
 (SELECT MonHocId FROM dbo.MonHoc WHERE MaMonHoc = N'BUS101'),
 (SELECT HocKyId FROM dbo.HocKy WHERE MaHocKy = N'HK2-2025'),
 NULL);
GO

SELECT TOP 20 *
FROM dbo.vw_KetQuaHocTap
ORDER BY MaSinhVien, MaMonHoc;
GO
