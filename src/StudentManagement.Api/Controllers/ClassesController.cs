using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces.Services;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/v1/classes")]
public class ClassesController : ControllerBase
{
    private readonly ILopHocService _lopHocService;

    public ClassesController(ILopHocService lopHocService)
    {
        _lopHocService = lopHocService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _lopHocService.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _lopHocService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLopHocRequest request)
    {
        var created = await _lopHocService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.LopHocId }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateLopHocRequest request)
    {
        var updated = await _lopHocService.UpdateAsync(id, request);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _lopHocService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
