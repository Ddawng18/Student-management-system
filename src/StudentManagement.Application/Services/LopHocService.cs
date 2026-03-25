using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Application.Interfaces.Services;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Services;

public class LopHocService : ILopHocService
{
    private readonly ILopHocRepository _lopHocRepository;

    public LopHocService(ILopHocRepository lopHocRepository)
    {
        _lopHocRepository = lopHocRepository;
    }

    public async Task<List<LopHocDto>> GetAllAsync()
    {
        var items = await _lopHocRepository.GetAllAsync();
        return items.Select(Map).ToList();
    }

    public async Task<LopHocDto?> GetByIdAsync(int id)
    {
        var item = await _lopHocRepository.GetByIdAsync(id);
        return item is null ? null : Map(item);
    }

    public async Task<LopHocDto> CreateAsync(CreateLopHocRequest request)
    {
        var existing = await _lopHocRepository.GetByMaLopAsync(request.MaLop);
        if (existing is not null)
        {
            throw new InvalidOperationException("Ma lop da ton tai.");
        }

        var entity = new LopHoc
        {
            MaLop = request.MaLop.Trim(),
            TenLop = request.TenLop.Trim(),
            KhoaId = request.KhoaId
        };

        await _lopHocRepository.AddAsync(entity);
        await _lopHocRepository.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateLopHocRequest request)
    {
        var entity = await _lopHocRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return false;
        }

        entity.MaLop = request.MaLop.Trim();
        entity.TenLop = request.TenLop.Trim();
        entity.KhoaId = request.KhoaId;
        entity.CoVanHocTapId = request.CoVanHocTapId;

        _lopHocRepository.Update(entity);
        await _lopHocRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _lopHocRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return false;
        }

        _lopHocRepository.Remove(entity);
        await _lopHocRepository.SaveChangesAsync();
        return true;
    }

    private static LopHocDto Map(LopHoc x) => new(x.LopHocId, x.MaLop, x.TenLop, x.KhoaId, x.CoVanHocTapId);
}
