using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces.Services;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/v1/semesters")]
public class SemestersController : ControllerBase
{
    private readonly IHocKyService _hocKyService;

    public SemestersController(IHocKyService hocKyService)
    {
        _hocKyService = hocKyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _hocKyService.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _hocKyService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHocKyRequest request)
    {
        var created = await _hocKyService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.HocKyId }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateHocKyRequest request)
    {
        var updated = await _hocKyService.UpdateAsync(id, request);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _hocKyService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
