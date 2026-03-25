using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces.Services;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/v1/enrollments")]
public class EnrollmentsController : ControllerBase
{
    private readonly IQuanLyDangKyService _dangKyService;

    public EnrollmentsController(IQuanLyDangKyService dangKyService)
    {
        _dangKyService = dangKyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _dangKyService.LayDanhSachDangKyAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _dangKyService.LayDangKyTheoIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] CreateDangKyHocRequest request)
    {
        var created = await _dangKyService.DangKyMonHocAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.DangKyHocId }, created);
    }

    [HttpPut("{id:int}/score")]
    public async Task<IActionResult> NhapDiem(int id, [FromBody] NhapDiemRequest request)
    {
        var updated = await _dangKyService.NhapDiemAsync(id, request.Diem);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPut("{id:int}/result/recalculate")]
    public async Task<IActionResult> TinhKetQua(int id)
    {
        var updated = await _dangKyService.TinhKetQuaAsync(id);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _dangKyService.HuyDangKyAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
