using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces.Services;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/v1/departments")]
public class DepartmentsController : ControllerBase
{
    private readonly IKhoaService _khoaService;

    public DepartmentsController(IKhoaService khoaService)
    {
        _khoaService = khoaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _khoaService.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _khoaService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateKhoaRequest request)
    {
        var created = await _khoaService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.KhoaId }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateKhoaRequest request)
    {
        var updated = await _khoaService.UpdateAsync(id, request);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _khoaService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    [HttpPut("{id:int}/head-lecturer/{lecturerId:int}")]
    public async Task<IActionResult> AssignHead(int id, int lecturerId)
    {
        var updated = await _khoaService.GanTruongKhoaAsync(id, lecturerId);
        return updated ? NoContent() : NotFound();
    }
}
