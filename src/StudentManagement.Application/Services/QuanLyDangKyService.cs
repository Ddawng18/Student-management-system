using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Application.Interfaces.Services;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Services;

public class QuanLyDangKyService : IQuanLyDangKyService
{
    private readonly IDangKyHocRepository _dangKyHocRepository;
    private readonly ISinhVienRepository _sinhVienRepository;
    private readonly IMonHocRepository _monHocRepository;
    private readonly IHocKyRepository _hocKyRepository;

    public QuanLyDangKyService(
        IDangKyHocRepository dangKyHocRepository,
        ISinhVienRepository sinhVienRepository,
        IMonHocRepository monHocRepository,
        IHocKyRepository hocKyRepository)
    {
        _dangKyHocRepository = dangKyHocRepository;
        _sinhVienRepository = sinhVienRepository;
        _monHocRepository = monHocRepository;
        _hocKyRepository = hocKyRepository;
    }

    public async Task<List<DangKyHocDto>> LayDanhSachDangKyAsync()
    {
        var items = await _dangKyHocRepository.GetAllAsync();
        return items.Select(Map).ToList();
    }

    public async Task<DangKyHocDto?> LayDangKyTheoIdAsync(int id)
    {
        var item = await _dangKyHocRepository.GetByIdAsync(id);
        return item is null ? null : Map(item);
    }

    public async Task<DangKyHocDto> DangKyMonHocAsync(CreateDangKyHocRequest request)
    {
        var sinhVien = await _sinhVienRepository.GetByIdAsync(request.SinhVienId)
            ?? throw new InvalidOperationException("Sinh vien khong ton tai.");

        var monHoc = await _monHocRepository.GetByIdAsync(request.MonHocId)
            ?? throw new InvalidOperationException("Mon hoc khong ton tai.");

        var hocKy = await _hocKyRepository.GetByIdAsync(request.HocKyId)
            ?? throw new InvalidOperationException("Hoc ky khong ton tai.");

        var existed = await _dangKyHocRepository.ExistsAsync(request.SinhVienId, request.MonHocId, request.HocKyId);
        if (existed)
        {
            throw new InvalidOperationException("Sinh vien da dang ky mon hoc nay trong hoc ky da chon.");
        }

        var entity = new DangKyHoc
        {
            SinhVienId = request.SinhVienId,
            MonHocId = request.MonHocId,
            HocKyId = request.HocKyId,
            NgayDangKy = DateTime.UtcNow,
            KetQua = "Chua co diem"
        };

        await _dangKyHocRepository.AddAsync(entity);
        await _dangKyHocRepository.SaveChangesAsync();

        entity.SinhVien = sinhVien;
        entity.MonHoc = monHoc;
        entity.HocKy = hocKy;

        return Map(entity);
    }

    public async Task<DangKyHocDto?> NhapDiemAsync(int dangKyHocId, decimal diem)
    {
        if (diem < 0 || diem > 10)
        {
            throw new InvalidOperationException("Diem phai trong khoang 0 den 10.");
        }

        var entity = await _dangKyHocRepository.GetByIdAsync(dangKyHocId);
        if (entity is null)
        {
            return null;
        }

        entity.Diem = diem;
        entity.KetQua = diem >= 5 ? "Dat" : "Rot";

        _dangKyHocRepository.Update(entity);
        await _dangKyHocRepository.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<DangKyHocDto?> TinhKetQuaAsync(int dangKyHocId)
    {
        var entity = await _dangKyHocRepository.GetByIdAsync(dangKyHocId);
        if (entity is null)
        {
            return null;
        }

        entity.KetQua = entity.Diem is null
            ? "Chua co diem"
            : entity.Diem >= 5
                ? "Dat"
                : "Rot";

        _dangKyHocRepository.Update(entity);
        await _dangKyHocRepository.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> HuyDangKyAsync(int dangKyHocId)
    {
        var entity = await _dangKyHocRepository.GetByIdAsync(dangKyHocId);
        if (entity is null)
        {
            return false;
        }

        _dangKyHocRepository.Remove(entity);
        await _dangKyHocRepository.SaveChangesAsync();
        return true;
    }

    private static DangKyHocDto Map(DangKyHoc x) =>
        new(
            x.DangKyHocId,
            x.SinhVienId,
            x.SinhVien.HoTen,
            x.MonHocId,
            x.MonHoc.TenMon,
            x.HocKyId,
            x.HocKy.TenHocKy,
            x.Diem,
            x.KetQua,
            x.NgayDangKy);
}
