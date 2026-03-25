using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Infrastructure.Persistence;
using StudentManagement.Infrastructure.Repositories;

namespace StudentManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IKhoaRepository, KhoaRepository>();
        services.AddScoped<ILopHocRepository, LopHocRepository>();
        services.AddScoped<ISinhVienRepository, SinhVienRepository>();
        services.AddScoped<IGiangVienRepository, GiangVienRepository>();
        services.AddScoped<IHocKyRepository, HocKyRepository>();
        services.AddScoped<IMonHocRepository, MonHocRepository>();
        services.AddScoped<IDangKyHocRepository, DangKyHocRepository>();

        return services;
    }
}
