using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Interfaces.Services;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/v1/lookups")]
public class LookupsController : ControllerBase
{
    private readonly IKhoaService _khoaService;
    private readonly ILopHocService _lopHocService;
    private readonly IGiangVienService _giangVienService;
    private readonly IHocKyService _hocKyService;
    private readonly IQuanLyMonHocService _monHocService;

    public LookupsController(
        IKhoaService khoaService,
        ILopHocService lopHocService,
        IGiangVienService giangVienService,
        IHocKyService hocKyService,
        IQuanLyMonHocService monHocService)
    {
        _khoaService = khoaService;
        _lopHocService = lopHocService;
        _giangVienService = giangVienService;
        _hocKyService = hocKyService;
        _monHocService = monHocService;
    }

    [HttpGet("departments")]
    public async Task<IActionResult> Departments() => Ok(await _khoaService.GetAllAsync());

    [HttpGet("classes")]
    public async Task<IActionResult> Classes() => Ok(await _lopHocService.GetAllAsync());

    [HttpGet("lecturers")]
    public async Task<IActionResult> Lecturers() => Ok(await _giangVienService.GetAllAsync());

    [HttpGet("semesters")]
    public async Task<IActionResult> Semesters() => Ok(await _hocKyService.GetAllAsync());

    [HttpGet("courses")]
    public async Task<IActionResult> Courses() => Ok(await _monHocService.LayDanhSachMonHocAsync());
}
