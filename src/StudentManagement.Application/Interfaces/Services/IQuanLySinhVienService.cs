using StudentManagement.Application.DTOs;

namespace StudentManagement.Application.Interfaces.Services;

public interface IQuanLySinhVienService
{
    Task<List<SinhVienDto>> LayDanhSachSinhVienAsync();
    Task<SinhVienDto?> LaySinhVienTheoIdAsync(int id);
    Task<List<SinhVienDto>> TimKiemSinhVienAsync(string keyword);
    Task<SinhVienDto> ThemSinhVienAsync(CreateSinhVienRequest request);
    Task<bool> CapNhatSinhVienAsync(int id, UpdateSinhVienRequest request);
    Task<bool> XoaSinhVienAsync(int id);
    Task<List<KetQuaHocTapDto>> XemKetQuaHocTapAsync(int sinhVienId);
}
