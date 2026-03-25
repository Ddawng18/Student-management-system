namespace StudentManagement.Application.DTOs;

public record HocKyDto(int HocKyId, string MaHocKy, string TenHocKy, int NamHoc, DateTime? NgayBatDau, DateTime? NgayKetThuc);

public record CreateHocKyRequest(string MaHocKy, string TenHocKy, int NamHoc, DateTime? NgayBatDau, DateTime? NgayKetThuc);

public record UpdateHocKyRequest(string TenHocKy, int NamHoc, DateTime? NgayBatDau, DateTime? NgayKetThuc);
