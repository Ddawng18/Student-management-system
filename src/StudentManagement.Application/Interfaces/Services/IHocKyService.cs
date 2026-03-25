using StudentManagement.Application.DTOs;

namespace StudentManagement.Application.Interfaces.Services;

public interface IHocKyService
{
    Task<List<HocKyDto>> GetAllAsync();
    Task<HocKyDto?> GetByIdAsync(int id);
    Task<HocKyDto> CreateAsync(CreateHocKyRequest request);
    Task<bool> UpdateAsync(int id, UpdateHocKyRequest request);
    Task<bool> DeleteAsync(int id);
}
