using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Interfaces.Repositories;

public interface ILopHocRepository
{
    Task<List<LopHoc>> GetAllAsync();
    Task<LopHoc?> GetByIdAsync(int id);
    Task<LopHoc?> GetByMaLopAsync(string maLop);
    Task AddAsync(LopHoc entity);
    void Update(LopHoc entity);
    void Remove(LopHoc entity);
    Task<int> SaveChangesAsync();
}
