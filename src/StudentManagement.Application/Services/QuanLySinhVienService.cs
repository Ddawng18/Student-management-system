using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Application.Interfaces.Services;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Services;

public class QuanLySinhVienService : IQuanLySinhVienService
{
    private readonly ISinhVienRepository _sinhVienRepository;
    private readonly IDangKyHocRepository _dangKyHocRepository;

    public QuanLySinhVienService(ISinhVienRepository sinhVienRepository, IDangKyHocRepository dangKyHocRepository)
    {
        _sinhVienRepository = sinhVienRepository;
        _dangKyHocRepository = dangKyHocRepository;
    }

    public async Task<List<SinhVienDto>> LayDanhSachSinhVienAsync()
    {
        var items = await _sinhVienRepository.GetAllAsync();
        return items.Select(Map).ToList();
    }

    public async Task<SinhVienDto?> LaySinhVienTheoIdAsync(int id)
    {
        var item = await _sinhVienRepository.GetByIdAsync(id);
        return item is null ? null : Map(item);
    }

    public async Task<List<SinhVienDto>> TimKiemSinhVienAsync(string keyword)
    {
        var items = await _sinhVienRepository.SearchByKeywordAsync(keyword.Trim());
        return items.Select(Map).ToList();
    }

    public async Task<SinhVienDto> ThemSinhVienAsync(CreateSinhVienRequest request)
    {
        var existing = await _sinhVienRepository.GetByMaSinhVienAsync(request.MaSinhVien);
        if (existing is not null)
        {
            throw new InvalidOperationException("Ma sinh vien da ton tai.");
        }

        var entity = new SinhVien
        {
            MaSinhVien = request.MaSinhVien.Trim(),
            HoTen = request.HoTen.Trim(),
            Email = request.Email.Trim(),
            NgaySinh = request.NgaySinh,
            LopHocId = request.LopHocId,
            AvatarUrl = NormalizeNullable(request.AvatarUrl),
            GioiTinh = NormalizeNullable(request.GioiTinh),
            QueQuan = NormalizeNullable(request.QueQuan),
            SoDienThoai = NormalizeNullable(request.SoDienThoai),
            DiaChiThuongTru = NormalizeNullable(request.DiaChiThuongTru),
            NoiTamTru = NormalizeNullable(request.NoiTamTru),
            DanToc = NormalizeNullable(request.DanToc),
            TonGiao = NormalizeNullable(request.TonGiao),
            Cccd = NormalizeNullable(request.Cccd),
            NgayCapCccd = request.NgayCapCccd,
            NoiCapCccd = NormalizeNullable(request.NoiCapCccd),
            HoTenPhuHuynh = NormalizeNullable(request.HoTenPhuHuynh),
            SoDienThoaiPhuHuynh = NormalizeNullable(request.SoDienThoaiPhuHuynh),
            NgheNghiepPhuHuynh = NormalizeNullable(request.NgheNghiepPhuHuynh),
            GhiChu = NormalizeNullable(request.GhiChu)
        };

        await _sinhVienRepository.AddAsync(entity);
        await _sinhVienRepository.SaveChangesAsync();
        return Map(entity);
    }

    public async Task<bool> CapNhatSinhVienAsync(int id, UpdateSinhVienRequest request)
    {
        var entity = await _sinhVienRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return false;
        }

        entity.HoTen = request.HoTen.Trim();
        entity.Email = request.Email.Trim();
        entity.NgaySinh = request.NgaySinh;
        entity.LopHocId = request.LopHocId;
        entity.AvatarUrl = NormalizeNullable(request.AvatarUrl);
        entity.GioiTinh = NormalizeNullable(request.GioiTinh);
        entity.QueQuan = NormalizeNullable(request.QueQuan);
        entity.SoDienThoai = NormalizeNullable(request.SoDienThoai);
        entity.DiaChiThuongTru = NormalizeNullable(request.DiaChiThuongTru);
        entity.NoiTamTru = NormalizeNullable(request.NoiTamTru);
        entity.DanToc = NormalizeNullable(request.DanToc);
        entity.TonGiao = NormalizeNullable(request.TonGiao);
        entity.Cccd = NormalizeNullable(request.Cccd);
        entity.NgayCapCccd = request.NgayCapCccd;
        entity.NoiCapCccd = NormalizeNullable(request.NoiCapCccd);
        entity.HoTenPhuHuynh = NormalizeNullable(request.HoTenPhuHuynh);
        entity.SoDienThoaiPhuHuynh = NormalizeNullable(request.SoDienThoaiPhuHuynh);
        entity.NgheNghiepPhuHuynh = NormalizeNullable(request.NgheNghiepPhuHuynh);
        entity.GhiChu = NormalizeNullable(request.GhiChu);

        _sinhVienRepository.Update(entity);
        await _sinhVienRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> XoaSinhVienAsync(int id)
    {
        var entity = await _sinhVienRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return false;
        }

        _sinhVienRepository.Remove(entity);
        await _sinhVienRepository.SaveChangesAsync();
        return true;
    }

    public async Task<List<KetQuaHocTapDto>> XemKetQuaHocTapAsync(int sinhVienId)
    {
        var dangKys = await _dangKyHocRepository.GetBySinhVienIdAsync(sinhVienId);

        return dangKys
            .Select(x => new KetQuaHocTapDto(
                x.DangKyHocId,
                x.MonHocId,
                x.MonHoc.TenMon,
                x.HocKyId,
                x.HocKy.TenHocKy,
                x.HocKy.NamHoc,
                x.Diem,
                x.KetQua))
            .ToList();
    }

    private static SinhVienDto Map(SinhVien x) =>
        new(
            x.SinhVienId,
            x.MaSinhVien,
            x.HoTen,
            x.Email,
            x.NgaySinh,
            x.LopHocId,
            x.AvatarUrl,
            x.GioiTinh,
            x.QueQuan,
            x.SoDienThoai,
            x.DiaChiThuongTru,
            x.NoiTamTru,
            x.DanToc,
            x.TonGiao,
            x.Cccd,
            x.NgayCapCccd,
            x.NoiCapCccd,
            x.HoTenPhuHuynh,
            x.SoDienThoaiPhuHuynh,
            x.NgheNghiepPhuHuynh,
            x.GhiChu);

    private static string? NormalizeNullable(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return value.Trim();
    }
}
