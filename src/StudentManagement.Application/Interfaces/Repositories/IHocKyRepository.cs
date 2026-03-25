using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Interfaces.Repositories;

public interface IHocKyRepository
{
    Task<List<HocKy>> GetAllAsync();
    Task<HocKy?> GetByIdAsync(int id);
    Task<HocKy?> GetByMaHocKyAsync(string maHocKy);
    Task AddAsync(HocKy entity);
    void Update(HocKy entity);
    void Remove(HocKy entity);
    Task<int> SaveChangesAsync();
}
