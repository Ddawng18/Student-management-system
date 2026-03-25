namespace StudentManagement.Application.DTOs;

public record KhoaDto(int KhoaId, string MaKhoa, string TenKhoa, int? TruongKhoaId);

public record CreateKhoaRequest(string MaKhoa, string TenKhoa);

public record UpdateKhoaRequest(string MaKhoa, string TenKhoa);

public record GanTruongKhoaRequest(int GiangVienId);
