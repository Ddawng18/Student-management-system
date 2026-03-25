namespace StudentManagement.Application.DTOs;

public record GiangVienDto(int GiangVienId, string MaGiangVien, string HoTen, string Email, int? KhoaId);

public record CreateGiangVienRequest(string MaGiangVien, string HoTen, string Email, int? KhoaId);

public record UpdateGiangVienRequest(string HoTen, string Email, int? KhoaId);
