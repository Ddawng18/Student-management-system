using StudentManagement.Application.DTOs;

namespace StudentManagement.Application.Interfaces.Services;

public interface IQuanLyMonHocService
{
    Task<List<MonHocDto>> LayDanhSachMonHocAsync();
    Task<MonHocDto?> LayMonHocTheoIdAsync(int id);
    Task<MonHocDto> ThemMonHocAsync(CreateMonHocRequest request);
    Task<bool> CapNhatMonHocAsync(int id, UpdateMonHocRequest request);
    Task<bool> XoaMonHocAsync(int id);
}
