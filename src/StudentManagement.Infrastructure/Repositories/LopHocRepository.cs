using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repositories;

public class LopHocRepository : ILopHocRepository
{
    private readonly AppDbContext _dbContext;

    public LopHocRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<LopHoc>> GetAllAsync() =>
        _dbContext.LopHocs.AsNoTracking().ToListAsync();

    public Task<LopHoc?> GetByIdAsync(int id) =>
        _dbContext.LopHocs.FirstOrDefaultAsync(x => x.LopHocId == id);

    public Task<LopHoc?> GetByMaLopAsync(string maLop) =>
        _dbContext.LopHocs.AsNoTracking().FirstOrDefaultAsync(x => x.MaLop == maLop);

    public Task AddAsync(LopHoc entity) => _dbContext.LopHocs.AddAsync(entity).AsTask();

    public void Update(LopHoc entity) => _dbContext.LopHocs.Update(entity);

    public void Remove(LopHoc entity) => _dbContext.LopHocs.Remove(entity);

    public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
