using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Interfaces.Repositories;

public interface IDangKyHocRepository
{
    Task<List<DangKyHoc>> GetAllAsync();
    Task<DangKyHoc?> GetByIdAsync(int id);
    Task<List<DangKyHoc>> GetBySinhVienIdAsync(int sinhVienId);
    Task<bool> ExistsAsync(int sinhVienId, int monHocId, int hocKyId);
    Task AddAsync(DangKyHoc entity);
    void Update(DangKyHoc entity);
    void Remove(DangKyHoc entity);
    Task<int> SaveChangesAsync();
}
