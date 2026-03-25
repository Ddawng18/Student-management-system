using StudentManagement.Application.DTOs;

namespace StudentManagement.Application.Interfaces.Services;

public interface IQuanLyDangKyService
{
    Task<List<DangKyHocDto>> LayDanhSachDangKyAsync();
    Task<DangKyHocDto?> LayDangKyTheoIdAsync(int id);
    Task<DangKyHocDto> DangKyMonHocAsync(CreateDangKyHocRequest request);
    Task<DangKyHocDto?> NhapDiemAsync(int dangKyHocId, decimal diem);
    Task<DangKyHocDto?> TinhKetQuaAsync(int dangKyHocId);
    Task<bool> HuyDangKyAsync(int dangKyHocId);
}
