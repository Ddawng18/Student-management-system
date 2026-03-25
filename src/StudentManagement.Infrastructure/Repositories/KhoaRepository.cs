using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repositories;

public class KhoaRepository : IKhoaRepository
{
    private readonly AppDbContext _dbContext;

    public KhoaRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Khoa>> GetAllAsync() =>
        _dbContext.Khoas.AsNoTracking().ToListAsync();

    public Task<Khoa?> GetByIdAsync(int id) =>
        _dbContext.Khoas.FirstOrDefaultAsync(x => x.KhoaId == id);

    public Task<Khoa?> GetByMaKhoaAsync(string maKhoa) =>
        _dbContext.Khoas.FirstOrDefaultAsync(x => x.MaKhoa == maKhoa);

    public Task AddAsync(Khoa entity) => _dbContext.Khoas.AddAsync(entity).AsTask();

    public void Update(Khoa entity) => _dbContext.Khoas.Update(entity);

    public void Remove(Khoa entity) => _dbContext.Khoas.Remove(entity);

    public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
