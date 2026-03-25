using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repositories;

public class GiangVienRepository : IGiangVienRepository
{
    private readonly AppDbContext _dbContext;

    public GiangVienRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<GiangVien>> GetAllAsync() =>
        _dbContext.GiangViens.AsNoTracking().ToListAsync();

    public Task<GiangVien?> GetByIdAsync(int id) =>
        _dbContext.GiangViens.FirstOrDefaultAsync(x => x.GiangVienId == id);

    public Task<GiangVien?> GetByMaGiangVienAsync(string maGiangVien) =>
        _dbContext.GiangViens.FirstOrDefaultAsync(x => x.MaGiangVien == maGiangVien);

    public Task AddAsync(GiangVien entity) => _dbContext.GiangViens.AddAsync(entity).AsTask();

    public void Update(GiangVien entity) => _dbContext.GiangViens.Update(entity);

    public void Remove(GiangVien entity) => _dbContext.GiangViens.Remove(entity);

    public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
