using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Application.Interfaces.Services;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Services;

public class HocKyService : IHocKyService
{
    private readonly IHocKyRepository _hocKyRepository;

    public HocKyService(IHocKyRepository hocKyRepository)
    {
        _hocKyRepository = hocKyRepository;
    }

    public async Task<List<HocKyDto>> GetAllAsync()
    {
        var items = await _hocKyRepository.GetAllAsync();
        return items.Select(Map).ToList();
    }

    public async Task<HocKyDto?> GetByIdAsync(int id)
    {
        var item = await _hocKyRepository.GetByIdAsync(id);
        return item is null ? null : Map(item);
    }

    public async Task<HocKyDto> CreateAsync(CreateHocKyRequest request)
    {
        var existing = await _hocKyRepository.GetByMaHocKyAsync(request.MaHocKy);
        if (existing is not null)
        {
            throw new InvalidOperationException("Ma hoc ky da ton tai.");
        }

        var entity = new HocKy
        {
            MaHocKy = request.MaHocKy.Trim(),
            TenHocKy = request.TenHocKy.Trim(),
            NamHoc = request.NamHoc,
            NgayBatDau = request.NgayBatDau,
            NgayKetThuc = request.NgayKetThuc
        };

        await _hocKyRepository.AddAsync(entity);
        await _hocKyRepository.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateHocKyRequest request)
    {
        var entity = await _hocKyRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return false;
        }

        entity.TenHocKy = request.TenHocKy.Trim();
        entity.NamHoc = request.NamHoc;
        entity.NgayBatDau = request.NgayBatDau;
        entity.NgayKetThuc = request.NgayKetThuc;

        _hocKyRepository.Update(entity);
        await _hocKyRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _hocKyRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return false;
        }

        _hocKyRepository.Remove(entity);
        await _hocKyRepository.SaveChangesAsync();
        return true;
    }

    private static HocKyDto Map(HocKy x) =>
        new(x.HocKyId, x.MaHocKy, x.TenHocKy, x.NamHoc, x.NgayBatDau, x.NgayKetThuc);
}
