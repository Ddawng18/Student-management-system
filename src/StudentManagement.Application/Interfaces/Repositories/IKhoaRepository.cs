using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Interfaces.Repositories;

public interface IKhoaRepository
{
    Task<List<Khoa>> GetAllAsync();
    Task<Khoa?> GetByIdAsync(int id);
    Task<Khoa?> GetByMaKhoaAsync(string maKhoa);
    Task AddAsync(Khoa entity);
    void Update(Khoa entity);
    void Remove(Khoa entity);
    Task<int> SaveChangesAsync();
}
