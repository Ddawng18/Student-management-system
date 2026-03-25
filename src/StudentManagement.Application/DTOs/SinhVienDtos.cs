namespace StudentManagement.Application.DTOs;

public record SinhVienDto(int SinhVienId, string MaSinhVien, string HoTen, string Email, DateTime NgaySinh, int? LopHocId);

public record CreateSinhVienRequest(string MaSinhVien, string HoTen, string Email, DateTime NgaySinh, int? LopHocId);

public record UpdateSinhVienRequest(string HoTen, string Email, DateTime NgaySinh, int? LopHocId);

public record KetQuaHocTapDto(int DangKyHocId, int MonHocId, string TenMon, int HocKyId, string TenHocKy, int NamHoc, decimal? Diem, string KetQua);
