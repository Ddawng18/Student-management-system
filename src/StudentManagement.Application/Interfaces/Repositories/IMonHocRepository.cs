using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Interfaces.Repositories;

public interface IMonHocRepository
{
    Task<List<MonHoc>> GetAllAsync();
    Task<MonHoc?> GetByIdAsync(int id);
    Task<MonHoc?> GetByMaMonHocAsync(string maMonHoc);
    Task AddAsync(MonHoc entity);
    void Update(MonHoc entity);
    void Remove(MonHoc entity);
    Task<int> SaveChangesAsync();
}
