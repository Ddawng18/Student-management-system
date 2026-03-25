using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Interfaces.Services;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/v1/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IKhoaService _khoaService;
    private readonly ILopHocService _lopHocService;
    private readonly IQuanLySinhVienService _sinhVienService;
    private readonly IGiangVienService _giangVienService;
    private readonly IQuanLyMonHocService _monHocService;
    private readonly IQuanLyDangKyService _dangKyService;

    public DashboardController(
        IKhoaService khoaService,
        ILopHocService lopHocService,
        IQuanLySinhVienService sinhVienService,
        IGiangVienService giangVienService,
        IQuanLyMonHocService monHocService,
        IQuanLyDangKyService dangKyService)
    {
        _khoaService = khoaService;
        _lopHocService = lopHocService;
        _sinhVienService = sinhVienService;
        _giangVienService = giangVienService;
        _monHocService = monHocService;
        _dangKyService = dangKyService;
    }

    [HttpGet("overview")]
    public async Task<IActionResult> Overview()
    {
        var khoa = await _khoaService.GetAllAsync();
        var lop = await _lopHocService.GetAllAsync();
        var sinhVien = await _sinhVienService.LayDanhSachSinhVienAsync();
        var giangVien = await _giangVienService.GetAllAsync();
        var monHoc = await _monHocService.LayDanhSachMonHocAsync();
        var dangKy = await _dangKyService.LayDanhSachDangKyAsync();

        return Ok(new
        {
            totalDepartments = khoa.Count,
            totalClasses = lop.Count,
            totalStudents = sinhVien.Count,
            totalLecturers = giangVien.Count,
            totalCourses = monHoc.Count,
            totalEnrollments = dangKy.Count
        });
    }
}
