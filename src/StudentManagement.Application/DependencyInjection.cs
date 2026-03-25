using Microsoft.Extensions.DependencyInjection;
using StudentManagement.Application.Interfaces.Services;
using StudentManagement.Application.Services;

namespace StudentManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IKhoaService, KhoaService>();
        services.AddScoped<ILopHocService, LopHocService>();
        services.AddScoped<IQuanLySinhVienService, QuanLySinhVienService>();
        services.AddScoped<IGiangVienService, GiangVienService>();
        services.AddScoped<IHocKyService, HocKyService>();
        services.AddScoped<IQuanLyMonHocService, QuanLyMonHocService>();
        services.AddScoped<IQuanLyDangKyService, QuanLyDangKyService>();

        return services;
    }
}
