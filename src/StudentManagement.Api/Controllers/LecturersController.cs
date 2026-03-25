using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces.Services;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/v1/lecturers")]
public class LecturersController : ControllerBase
{
    private readonly IGiangVienService _giangVienService;

    public LecturersController(IGiangVienService giangVienService)
    {
        _giangVienService = giangVienService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _giangVienService.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _giangVienService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGiangVienRequest request)
    {
        var created = await _giangVienService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.GiangVienId }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateGiangVienRequest request)
    {
        var updated = await _giangVienService.UpdateAsync(id, request);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _giangVienService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
