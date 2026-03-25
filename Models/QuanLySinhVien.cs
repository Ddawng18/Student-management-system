using System;
using System.Collections.Generic;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Models
{
    public class QuanLySinhVien
    {
        private readonly SinhVienService _sinhVienService;

        public QuanLySinhVien()
        {
            _sinhVienService = new SinhVienService();
        }

        public void ThemSinhVien(SinhVien sinhVien)
        {
            _sinhVienService.Them(sinhVien);
        }

        public void CapNhatSinhVien(SinhVien sinhVien)
        {
            _sinhVienService.CapNhat(sinhVien);
        }

        public void XoaSinhVien(string maSinhVien)
        {
            _sinhVienService.XoaTheoMa(maSinhVien);
        }

        public IReadOnlyList<SinhVien> LayDanhSachSinhVien()
        {
            return _sinhVienService.LayTatCa();
        }

        public IReadOnlyList<SinhVien> TimSinhVienTheoLop(string maLop)
        {
            return _sinhVienService.TimTheoLop(maLop);
        }
    }
}
