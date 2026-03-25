using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Application.Interfaces.Services;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Services;

public class GiangVienService : IGiangVienService
{
    private readonly IGiangVienRepository _giangVienRepository;

    public GiangVienService(IGiangVienRepository giangVienRepository)
    {
        _giangVienRepository = giangVienRepository;
    }

    public async Task<List<GiangVienDto>> GetAllAsync()
    {
        var items = await _giangVienRepository.GetAllAsync();
        return items.Select(Map).ToList();
    }

    public async Task<GiangVienDto?> GetByIdAsync(int id)
    {
        var item = await _giangVienRepository.GetByIdAsync(id);
        return item is null ? null : Map(item);
    }

    public async Task<GiangVienDto> CreateAsync(CreateGiangVienRequest request)
    {
        var existing = await _giangVienRepository.GetByMaGiangVienAsync(request.MaGiangVien);
        if (existing is not null)
        {
            throw new InvalidOperationException("Ma giang vien da ton tai.");
        }

        var entity = new GiangVien
        {
            MaGiangVien = request.MaGiangVien.Trim(),
            HoTen = request.HoTen.Trim(),
            Email = request.Email.Trim(),
            KhoaId = request.KhoaId
        };

        await _giangVienRepository.AddAsync(entity);
        await _giangVienRepository.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateGiangVienRequest request)
    {
        var entity = await _giangVienRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return false;
        }

        entity.HoTen = request.HoTen.Trim();
        entity.Email = request.Email.Trim();
        entity.KhoaId = request.KhoaId;

        _giangVienRepository.Update(entity);
        await _giangVienRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _giangVienRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return false;
        }

        _giangVienRepository.Remove(entity);
        await _giangVienRepository.SaveChangesAsync();
        return true;
    }

    private static GiangVienDto Map(GiangVien x) => new(x.GiangVienId, x.MaGiangVien, x.HoTen, x.Email, x.KhoaId);
}
