using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Interfaces.Repositories;

public interface ISinhVienRepository
{
    Task<List<SinhVien>> GetAllAsync();
    Task<SinhVien?> GetByIdAsync(int id);
    Task<SinhVien?> GetByMaSinhVienAsync(string maSinhVien);
    Task<List<SinhVien>> SearchByKeywordAsync(string keyword);
    Task AddAsync(SinhVien entity);
    void Update(SinhVien entity);
    void Remove(SinhVien entity);
    Task<int> SaveChangesAsync();
}
