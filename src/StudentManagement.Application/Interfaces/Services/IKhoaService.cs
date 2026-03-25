using StudentManagement.Application.DTOs;

namespace StudentManagement.Application.Interfaces.Services;

public interface IKhoaService
{
    Task<List<KhoaDto>> GetAllAsync();
    Task<KhoaDto?> GetByIdAsync(int id);
    Task<KhoaDto> CreateAsync(CreateKhoaRequest request);
    Task<bool> UpdateAsync(int id, UpdateKhoaRequest request);
    Task<bool> DeleteAsync(int id);
    Task<bool> GanTruongKhoaAsync(int khoaId, int giangVienId);
}
