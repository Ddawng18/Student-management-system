using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repositories;

public class SinhVienRepository : ISinhVienRepository
{
    private readonly AppDbContext _dbContext;

    public SinhVienRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<SinhVien>> GetAllAsync() =>
        _dbContext.SinhViens.AsNoTracking().ToListAsync();

    public Task<SinhVien?> GetByIdAsync(int id) =>
        _dbContext.SinhViens.FirstOrDefaultAsync(x => x.SinhVienId == id);

    public Task<SinhVien?> GetByMaSinhVienAsync(string maSinhVien) =>
        _dbContext.SinhViens.AsNoTracking().FirstOrDefaultAsync(x => x.MaSinhVien == maSinhVien);

    public Task<List<SinhVien>> SearchByKeywordAsync(string keyword) =>
        _dbContext.SinhViens
            .AsNoTracking()
            .Where(x => x.HoTen.Contains(keyword) || x.MaSinhVien.Contains(keyword) || x.Email.Contains(keyword))
            .ToListAsync();

    public Task AddAsync(SinhVien entity) => _dbContext.SinhViens.AddAsync(entity).AsTask();

    public void Update(SinhVien entity) => _dbContext.SinhViens.Update(entity);

    public void Remove(SinhVien entity) => _dbContext.SinhViens.Remove(entity);

    public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
