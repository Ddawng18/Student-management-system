using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Application.Interfaces.Services;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Services;

public class KhoaService : IKhoaService
{
    private readonly IKhoaRepository _khoaRepository;
    private readonly IGiangVienRepository _giangVienRepository;

    public KhoaService(IKhoaRepository khoaRepository, IGiangVienRepository giangVienRepository)
    {
        _khoaRepository = khoaRepository;
        _giangVienRepository = giangVienRepository;
    }

    public async Task<List<KhoaDto>> GetAllAsync()
    {
        var items = await _khoaRepository.GetAllAsync();
        return items.Select(Map).ToList();
    }

    public async Task<KhoaDto?> GetByIdAsync(int id)
    {
        var item = await _khoaRepository.GetByIdAsync(id);
        return item is null ? null : Map(item);
    }

    public async Task<KhoaDto> CreateAsync(CreateKhoaRequest request)
    {
        var existing = await _khoaRepository.GetByMaKhoaAsync(request.MaKhoa);
        if (existing is not null)
        {
            throw new InvalidOperationException("Ma khoa da ton tai.");
        }

        var entity = new Khoa
        {
            MaKhoa = request.MaKhoa.Trim(),
            TenKhoa = request.TenKhoa.Trim()
        };

        await _khoaRepository.AddAsync(entity);
        await _khoaRepository.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateKhoaRequest request)
    {
        var entity = await _khoaRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return false;
        }

        entity.MaKhoa = request.MaKhoa.Trim();
        entity.TenKhoa = request.TenKhoa.Trim();

        _khoaRepository.Update(entity);
        await _khoaRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _khoaRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return false;
        }

        _khoaRepository.Remove(entity);
        await _khoaRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> GanTruongKhoaAsync(int khoaId, int giangVienId)
    {
        var khoa = await _khoaRepository.GetByIdAsync(khoaId);
        var giangVien = await _giangVienRepository.GetByIdAsync(giangVienId);

        if (khoa is null || giangVien is null)
        {
            return false;
        }

        khoa.TruongKhoaId = giangVienId;
        _khoaRepository.Update(khoa);
        await _khoaRepository.SaveChangesAsync();
        return true;
    }

    private static KhoaDto Map(Khoa x) => new(x.KhoaId, x.MaKhoa, x.TenKhoa, x.TruongKhoaId);
}
