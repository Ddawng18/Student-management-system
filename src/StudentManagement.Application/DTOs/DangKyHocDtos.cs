namespace StudentManagement.Application.DTOs;

public record DangKyHocDto(int DangKyHocId, int SinhVienId, string SinhVienHoTen, int MonHocId, string TenMonHoc, int HocKyId, string TenHocKy, decimal? Diem, string KetQua, DateTime NgayDangKy);

public record CreateDangKyHocRequest(int SinhVienId, int MonHocId, int HocKyId);

public record NhapDiemRequest(decimal Diem);
