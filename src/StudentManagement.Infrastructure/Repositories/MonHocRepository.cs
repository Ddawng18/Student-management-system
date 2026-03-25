using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repositories;

public class MonHocRepository : IMonHocRepository
{
    private readonly AppDbContext _dbContext;

    public MonHocRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<MonHoc>> GetAllAsync() =>
        _dbContext.MonHocs.AsNoTracking().ToListAsync();

    public Task<MonHoc?> GetByIdAsync(int id) =>
        _dbContext.MonHocs.FirstOrDefaultAsync(x => x.MonHocId == id);

    public Task<MonHoc?> GetByMaMonHocAsync(string maMonHoc) =>
        _dbContext.MonHocs.AsNoTracking().FirstOrDefaultAsync(x => x.MaMonHoc == maMonHoc);

    public Task AddAsync(MonHoc entity) => _dbContext.MonHocs.AddAsync(entity).AsTask();

    public void Update(MonHoc entity) => _dbContext.MonHocs.Update(entity);

    public void Remove(MonHoc entity) => _dbContext.MonHocs.Remove(entity);

    public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
