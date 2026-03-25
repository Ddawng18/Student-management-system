using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repositories;

public class DangKyHocRepository : IDangKyHocRepository
{
    private readonly AppDbContext _dbContext;

    public DangKyHocRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<DangKyHoc>> GetAllAsync() =>
        _dbContext.DangKyHocs
            .AsNoTracking()
            .Include(x => x.SinhVien)
            .Include(x => x.MonHoc)
            .Include(x => x.HocKy)
            .ToListAsync();

    public Task<DangKyHoc?> GetByIdAsync(int id) =>
        _dbContext.DangKyHocs
            .Include(x => x.SinhVien)
            .Include(x => x.MonHoc)
            .Include(x => x.HocKy)
            .FirstOrDefaultAsync(x => x.DangKyHocId == id);

    public Task<List<DangKyHoc>> GetBySinhVienIdAsync(int sinhVienId) =>
        _dbContext.DangKyHocs
            .AsNoTracking()
            .Include(x => x.MonHoc)
            .Include(x => x.HocKy)
            .Where(x => x.SinhVienId == sinhVienId)
            .ToListAsync();

    public Task<bool> ExistsAsync(int sinhVienId, int monHocId, int hocKyId) =>
        _dbContext.DangKyHocs.AnyAsync(x => x.SinhVienId == sinhVienId && x.MonHocId == monHocId && x.HocKyId == hocKyId);

    public Task AddAsync(DangKyHoc entity) => _dbContext.DangKyHocs.AddAsync(entity).AsTask();

    public void Update(DangKyHoc entity) => _dbContext.DangKyHocs.Update(entity);

    public void Remove(DangKyHoc entity) => _dbContext.DangKyHocs.Remove(entity);

    public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
