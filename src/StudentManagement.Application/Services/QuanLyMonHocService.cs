using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Application.Interfaces.Services;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Services;

public class QuanLyMonHocService : IQuanLyMonHocService
{
    private readonly IMonHocRepository _monHocRepository;

    public QuanLyMonHocService(IMonHocRepository monHocRepository)
    {
        _monHocRepository = monHocRepository;
    }

    public async Task<List<MonHocDto>> LayDanhSachMonHocAsync()
    {
        var items = await _monHocRepository.GetAllAsync();
        return items.Select(Map).ToList();
    }

    public async Task<MonHocDto?> LayMonHocTheoIdAsync(int id)
    {
        var item = await _monHocRepository.GetByIdAsync(id);
        return item is null ? null : Map(item);
    }

    public async Task<MonHocDto> ThemMonHocAsync(CreateMonHocRequest request)
    {
        var existing = await _monHocRepository.GetByMaMonHocAsync(request.MaMonHoc);
        if (existing is not null)
        {
            throw new InvalidOperationException("Ma mon hoc da ton tai.");
        }

        var entity = new MonHoc
        {
            MaMonHoc = request.MaMonHoc.Trim(),
            TenMon = request.TenMon.Trim(),
            SoTinChi = request.SoTinChi,
            KhoaId = request.KhoaId,
            GiangVienGiangDayId = request.GiangVienGiangDayId,
            HocKyId = request.HocKyId
        };

        await _monHocRepository.AddAsync(entity);
        await _monHocRepository.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> CapNhatMonHocAsync(int id, UpdateMonHocRequest request)
    {
        var entity = await _monHocRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return false;
        }

        entity.TenMon = request.TenMon.Trim();
        entity.SoTinChi = request.SoTinChi;
        entity.KhoaId = request.KhoaId;
        entity.GiangVienGiangDayId = request.GiangVienGiangDayId;
        entity.HocKyId = request.HocKyId;

        _monHocRepository.Update(entity);
        await _monHocRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> XoaMonHocAsync(int id)
    {
        var entity = await _monHocRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return false;
        }

        _monHocRepository.Remove(entity);
        await _monHocRepository.SaveChangesAsync();
        return true;
    }

    private static MonHocDto Map(MonHoc x) =>
        new(x.MonHocId, x.MaMonHoc, x.TenMon, x.SoTinChi, x.KhoaId, x.GiangVienGiangDayId, x.HocKyId);
}
