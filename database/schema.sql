-- Student Management System - SQL Server Schema
-- Phase 1: Database design from class diagram

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF DB_ID(N'StudentManagementSystemDb') IS NULL
BEGIN
    CREATE DATABASE StudentManagementSystemDb;
END;
GO

USE StudentManagementSystemDb;
GO

-- Drop in dependency order for rerun convenience.
IF OBJECT_ID(N'dbo.DangKyHoc', N'U') IS NOT NULL DROP TABLE dbo.DangKyHoc;
IF OBJECT_ID(N'dbo.MonHoc', N'U') IS NOT NULL DROP TABLE dbo.MonHoc;
IF OBJECT_ID(N'dbo.HocKy', N'U') IS NOT NULL DROP TABLE dbo.HocKy;
IF OBJECT_ID(N'dbo.SinhVien', N'U') IS NOT NULL DROP TABLE dbo.SinhVien;
IF OBJECT_ID(N'dbo.LopHoc', N'U') IS NOT NULL DROP TABLE dbo.LopHoc;
IF OBJECT_ID(N'dbo.GiangVien', N'U') IS NOT NULL DROP TABLE dbo.GiangVien;
IF OBJECT_ID(N'dbo.Khoa', N'U') IS NOT NULL DROP TABLE dbo.Khoa;
GO

CREATE TABLE dbo.Khoa
(
    KhoaId INT IDENTITY(1,1) PRIMARY KEY,
    MaKhoa NVARCHAR(20) NOT NULL,
    TenKhoa NVARCHAR(150) NOT NULL,
    TruongKhoaId INT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Khoa_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_Khoa_UpdatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT UQ_Khoa_MaKhoa UNIQUE (MaKhoa)
);
GO

CREATE TABLE dbo.GiangVien
(
    GiangVienId INT IDENTITY(1,1) PRIMARY KEY,
    MaGiangVien NVARCHAR(20) NOT NULL,
    HoTen NVARCHAR(150) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    KhoaId INT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_GiangVien_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_GiangVien_UpdatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT UQ_GiangVien_MaGiangVien UNIQUE (MaGiangVien),
    CONSTRAINT UQ_GiangVien_Email UNIQUE (Email),
    CONSTRAINT FK_GiangVien_Khoa FOREIGN KEY (KhoaId) REFERENCES dbo.Khoa(KhoaId)
        ON DELETE SET NULL ON UPDATE NO ACTION
);
GO

ALTER TABLE dbo.Khoa
ADD CONSTRAINT FK_Khoa_TruongKhoa FOREIGN KEY (TruongKhoaId) REFERENCES dbo.GiangVien(GiangVienId)
    ON DELETE SET NULL ON UPDATE NO ACTION;
GO

CREATE TABLE dbo.LopHoc
(
    LopHocId INT IDENTITY(1,1) PRIMARY KEY,
    MaLop NVARCHAR(20) NOT NULL,
    TenLop NVARCHAR(150) NOT NULL,
    KhoaId INT NOT NULL,
    CoVanHocTapId INT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_LopHoc_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_LopHoc_UpdatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT UQ_LopHoc_MaLop UNIQUE (MaLop),
    CONSTRAINT FK_LopHoc_Khoa FOREIGN KEY (KhoaId) REFERENCES dbo.Khoa(KhoaId)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_LopHoc_CoVan FOREIGN KEY (CoVanHocTapId) REFERENCES dbo.GiangVien(GiangVienId)
        ON DELETE SET NULL ON UPDATE NO ACTION
);
GO

CREATE TABLE dbo.SinhVien
(
    SinhVienId INT IDENTITY(1,1) PRIMARY KEY,
    MaSinhVien NVARCHAR(20) NOT NULL,
    HoTen NVARCHAR(150) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    NgaySinh DATE NOT NULL,
    LopHocId INT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_SinhVien_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_SinhVien_UpdatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT UQ_SinhVien_MaSinhVien UNIQUE (MaSinhVien),
    CONSTRAINT UQ_SinhVien_Email UNIQUE (Email),
    CONSTRAINT FK_SinhVien_LopHoc FOREIGN KEY (LopHocId) REFERENCES dbo.LopHoc(LopHocId)
        ON DELETE SET NULL ON UPDATE NO ACTION
);
GO

CREATE TABLE dbo.HocKy
(
    HocKyId INT IDENTITY(1,1) PRIMARY KEY,
    MaHocKy NVARCHAR(20) NOT NULL,
    TenHocKy NVARCHAR(50) NOT NULL,
    NamHoc INT NOT NULL,
    NgayBatDau DATE NULL,
    NgayKetThuc DATE NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_HocKy_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_HocKy_UpdatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT UQ_HocKy_MaHocKy UNIQUE (MaHocKy),
    CONSTRAINT CK_HocKy_NamHoc CHECK (NamHoc >= 2000 AND NamHoc <= 2100),
    CONSTRAINT CK_HocKy_ThoiGian CHECK (NgayBatDau IS NULL OR NgayKetThuc IS NULL OR NgayBatDau <= NgayKetThuc)
);
GO

CREATE TABLE dbo.MonHoc
(
    MonHocId INT IDENTITY(1,1) PRIMARY KEY,
    MaMonHoc NVARCHAR(20) NOT NULL,
    TenMon NVARCHAR(150) NOT NULL,
    SoTinChi INT NOT NULL,
    KhoaId INT NULL,
    GiangVienGiangDayId INT NULL,
    HocKyId INT NULL,
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_MonHoc_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_MonHoc_UpdatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT UQ_MonHoc_MaMonHoc UNIQUE (MaMonHoc),
    CONSTRAINT CK_MonHoc_SoTinChi CHECK (SoTinChi BETWEEN 1 AND 10),
    CONSTRAINT FK_MonHoc_Khoa FOREIGN KEY (KhoaId) REFERENCES dbo.Khoa(KhoaId)
        ON DELETE SET NULL ON UPDATE NO ACTION,
    CONSTRAINT FK_MonHoc_GiangVien FOREIGN KEY (GiangVienGiangDayId) REFERENCES dbo.GiangVien(GiangVienId)
        ON DELETE SET NULL ON UPDATE NO ACTION,
    CONSTRAINT FK_MonHoc_HocKy FOREIGN KEY (HocKyId) REFERENCES dbo.HocKy(HocKyId)
        ON DELETE SET NULL ON UPDATE NO ACTION
);
GO

CREATE TABLE dbo.DangKyHoc
(
    DangKyHocId INT IDENTITY(1,1) PRIMARY KEY,
    SinhVienId INT NOT NULL,
    MonHocId INT NOT NULL,
    HocKyId INT NOT NULL,
    Diem DECIMAL(4,2) NULL,
    KetQua NVARCHAR(20) NOT NULL CONSTRAINT DF_DangKyHoc_KetQua DEFAULT N'Chua co diem',
    NgayDangKy DATETIME2 NOT NULL CONSTRAINT DF_DangKyHoc_NgayDangKy DEFAULT SYSUTCDATETIME(),
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_DangKyHoc_CreatedAt DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_DangKyHoc_UpdatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_DangKyHoc_SinhVien FOREIGN KEY (SinhVienId) REFERENCES dbo.SinhVien(SinhVienId)
        ON DELETE CASCADE ON UPDATE NO ACTION,
    CONSTRAINT FK_DangKyHoc_MonHoc FOREIGN KEY (MonHocId) REFERENCES dbo.MonHoc(MonHocId)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_DangKyHoc_HocKy FOREIGN KEY (HocKyId) REFERENCES dbo.HocKy(HocKyId)
        ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT UQ_DangKyHoc UNIQUE (SinhVienId, MonHocId, HocKyId),
    CONSTRAINT CK_DangKyHoc_Diem CHECK (Diem IS NULL OR (Diem >= 0 AND Diem <= 10))
);
GO

CREATE INDEX IX_GiangVien_KhoaId ON dbo.GiangVien(KhoaId);
CREATE INDEX IX_LopHoc_KhoaId ON dbo.LopHoc(KhoaId);
CREATE INDEX IX_SinhVien_LopHocId ON dbo.SinhVien(LopHocId);
CREATE INDEX IX_MonHoc_KhoaId ON dbo.MonHoc(KhoaId);
CREATE INDEX IX_MonHoc_GiangVienGiangDayId ON dbo.MonHoc(GiangVienGiangDayId);
CREATE INDEX IX_MonHoc_HocKyId ON dbo.MonHoc(HocKyId);
CREATE INDEX IX_DangKyHoc_SinhVienId ON dbo.DangKyHoc(SinhVienId);
CREATE INDEX IX_DangKyHoc_MonHocId ON dbo.DangKyHoc(MonHocId);
CREATE INDEX IX_DangKyHoc_HocKyId ON dbo.DangKyHoc(HocKyId);
GO

-- Trigger: auto-calculate KetQua based on Diem.
CREATE OR ALTER TRIGGER dbo.TR_DangKyHoc_SetKetQua
ON dbo.DangKyHoc
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dk
    SET KetQua = CASE
        WHEN i.Diem IS NULL THEN N'Chua co diem'
        WHEN i.Diem >= 5 THEN N'Dat'
        ELSE N'Rot'
    END,
    UpdatedAt = SYSUTCDATETIME()
    FROM dbo.DangKyHoc dk
    INNER JOIN inserted i ON dk.DangKyHocId = i.DangKyHocId;
END;
GO

-- View: quick report for student learning results.
CREATE OR ALTER VIEW dbo.vw_KetQuaHocTap
AS
SELECT
    sv.SinhVienId,
    sv.MaSinhVien,
    sv.HoTen,
    mh.MonHocId,
    mh.MaMonHoc,
    mh.TenMon,
    hk.HocKyId,
    hk.MaHocKy,
    hk.TenHocKy,
    hk.NamHoc,
    dk.Diem,
    dk.KetQua,
    dk.NgayDangKy
FROM dbo.DangKyHoc dk
INNER JOIN dbo.SinhVien sv ON dk.SinhVienId = sv.SinhVienId
INNER JOIN dbo.MonHoc mh ON dk.MonHocId = mh.MonHocId
INNER JOIN dbo.HocKy hk ON dk.HocKyId = hk.HocKyId;
GO
