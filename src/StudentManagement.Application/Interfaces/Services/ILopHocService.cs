using StudentManagement.Application.DTOs;

namespace StudentManagement.Application.Interfaces.Services;

public interface ILopHocService
{
    Task<List<LopHocDto>> GetAllAsync();
    Task<LopHocDto?> GetByIdAsync(int id);
    Task<LopHocDto> CreateAsync(CreateLopHocRequest request);
    Task<bool> UpdateAsync(int id, UpdateLopHocRequest request);
    Task<bool> DeleteAsync(int id);
}
