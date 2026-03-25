using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repositories;

public class TaiKhoanNguoiDungRepository : ITaiKhoanNguoiDungRepository
{
    private readonly AppDbContext _dbContext;

    public TaiKhoanNguoiDungRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<TaiKhoanNguoiDung?> GetByEmailAsync(string email) =>
        _dbContext.TaiKhoanNguoiDungs.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);

    public Task AddAsync(TaiKhoanNguoiDung entity) => _dbContext.TaiKhoanNguoiDungs.AddAsync(entity).AsTask();

    public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
