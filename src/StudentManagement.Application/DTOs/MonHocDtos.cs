namespace StudentManagement.Application.DTOs;

public record MonHocDto(int MonHocId, string MaMonHoc, string TenMon, int SoTinChi, int? KhoaId, int? GiangVienGiangDayId, int? HocKyId);

public record CreateMonHocRequest(string MaMonHoc, string TenMon, int SoTinChi, int? KhoaId, int? GiangVienGiangDayId, int? HocKyId);

public record UpdateMonHocRequest(string TenMon, int SoTinChi, int? KhoaId, int? GiangVienGiangDayId, int? HocKyId);
