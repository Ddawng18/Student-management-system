using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces.Services;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/v1/courses")]
public class CoursesController : ControllerBase
{
    private readonly IQuanLyMonHocService _monHocService;

    public CoursesController(IQuanLyMonHocService monHocService)
    {
        _monHocService = monHocService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _monHocService.LayDanhSachMonHocAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _monHocService.LayMonHocTheoIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMonHocRequest request)
    {
        var created = await _monHocService.ThemMonHocAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.MonHocId }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMonHocRequest request)
    {
        var updated = await _monHocService.CapNhatMonHocAsync(id, request);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _monHocService.XoaMonHocAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
