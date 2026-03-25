namespace StudentManagement.Application.DTOs;

public record LopHocDto(int LopHocId, string MaLop, string TenLop, int KhoaId, int? CoVanHocTapId);

public record CreateLopHocRequest(string MaLop, string TenLop, int KhoaId);

public record UpdateLopHocRequest(string MaLop, string TenLop, int KhoaId, int? CoVanHocTapId);
