using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repositories;

public class HocKyRepository : IHocKyRepository
{
    private readonly AppDbContext _dbContext;

    public HocKyRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<HocKy>> GetAllAsync() =>
        _dbContext.HocKys.AsNoTracking().ToListAsync();

    public Task<HocKy?> GetByIdAsync(int id) =>
        _dbContext.HocKys.FirstOrDefaultAsync(x => x.HocKyId == id);

    public Task<HocKy?> GetByMaHocKyAsync(string maHocKy) =>
        _dbContext.HocKys.FirstOrDefaultAsync(x => x.MaHocKy == maHocKy);

    public Task AddAsync(HocKy entity) => _dbContext.HocKys.AddAsync(entity).AsTask();

    public void Update(HocKy entity) => _dbContext.HocKys.Update(entity);

    public void Remove(HocKy entity) => _dbContext.HocKys.Remove(entity);

    public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
