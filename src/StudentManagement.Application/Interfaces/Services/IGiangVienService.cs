using StudentManagement.Application.DTOs;

namespace StudentManagement.Application.Interfaces.Services;

public interface IGiangVienService
{
    Task<List<GiangVienDto>> GetAllAsync();
    Task<GiangVienDto?> GetByIdAsync(int id);
    Task<GiangVienDto> CreateAsync(CreateGiangVienRequest request);
    Task<bool> UpdateAsync(int id, UpdateGiangVienRequest request);
    Task<bool> DeleteAsync(int id);
}
