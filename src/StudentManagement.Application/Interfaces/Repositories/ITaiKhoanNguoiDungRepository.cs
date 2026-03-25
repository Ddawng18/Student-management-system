using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Interfaces.Repositories;

public interface ITaiKhoanNguoiDungRepository
{
    Task<TaiKhoanNguoiDung?> GetByEmailAsync(string email);
    Task AddAsync(TaiKhoanNguoiDung entity);
    Task<int> SaveChangesAsync();
}
