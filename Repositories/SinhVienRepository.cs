using System.Collections.Generic;
using System.Linq;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public class SinhVienRepository : ISinhVienRepository
    {
        private readonly List<SinhVien> _sinhViens = new List<SinhVien>();
        private int _nextId = 1;

        public void ThemSinhVien(SinhVien sinhVien)
        {
            sinhVien.MaSinhVien = _nextId++;
            _sinhViens.Add(sinhVien);
        }

        public List<SinhVien> LayTatCaSinhVien()
        {
            return _sinhViens;
        }

        public SinhVien LaySinhVienTheoId(int id)
        {
            return _sinhViens.FirstOrDefault(s => s.MaSinhVien == id);
        }

        public void CapNhatSinhVien(SinhVien sinhVien)
        {
            var existing = LaySinhVienTheoId(sinhVien.MaSinhVien);
            if (existing != null)
            {
                existing.HoTen = sinhVien.HoTen;
                existing.Email = sinhVien.Email;
                existing.NgaySinh = sinhVien.NgaySinh;
                existing.DiaChi = sinhVien.DiaChi;
                existing.MaKhoa = sinhVien.MaKhoa;
                existing.MaLop = sinhVien.MaLop;
            }
        }

        public void XoaSinhVien(int id)
        {
            var sinhVien = LaySinhVienTheoId(id);
            if (sinhVien != null)
            {
                _sinhViens.Remove(sinhVien);
            }
        }
        public List<SinhVien> TimSinhVienTheoTen(string ten)
        {
    List<SinhVien> ketQua = new List<SinhVien>();

    foreach (SinhVien sv in danhSachSinhVien)
    {
        if (sv.HoTen.Contains(ten))
        {
            ketQua.Add(sv);
        }
    }

    return ketQua;
        }
        public List<SinhVien> TimSinhVienTheoLop(int maLop)
{
    List<SinhVien> ketQua = new List<SinhVien>();

    foreach (SinhVien sv in danhSachSinhVien)
    {
        if (sv.MaLop == maLop)
        {
            ketQua.Add(sv);
        }
    }

    return ketQua;
}
    }
}