using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces.Services;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/v1/students")]
public class StudentsController : ControllerBase
{
    private readonly IQuanLySinhVienService _sinhVienService;

    public StudentsController(IQuanLySinhVienService sinhVienService)
    {
        _sinhVienService = sinhVienService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _sinhVienService.LayDanhSachSinhVienAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _sinhVienService.LaySinhVienTheoIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string keyword)
    {
        return Ok(await _sinhVienService.TimKiemSinhVienAsync(keyword));
    }

    [HttpGet("{id:int}/results")]
    public async Task<IActionResult> Results(int id)
    {
        return Ok(await _sinhVienService.XemKetQuaHocTapAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSinhVienRequest request)
    {
        var created = await _sinhVienService.ThemSinhVienAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.SinhVienId }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSinhVienRequest request)
    {
        var updated = await _sinhVienService.CapNhatSinhVienAsync(id, request);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _sinhVienService.XoaSinhVienAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
