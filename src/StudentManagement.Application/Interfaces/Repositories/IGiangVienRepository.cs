using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Interfaces.Repositories;

public interface IGiangVienRepository
{
    Task<List<GiangVien>> GetAllAsync();
    Task<GiangVien?> GetByIdAsync(int id);
    Task<GiangVien?> GetByMaGiangVienAsync(string maGiangVien);
    Task AddAsync(GiangVien entity);
    void Update(GiangVien entity);
    void Remove(GiangVien entity);
    Task<int> SaveChangesAsync();
}
