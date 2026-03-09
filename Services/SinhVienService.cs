using System;
using System.Collections.Generic;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.Services
{
    public class SinhVienService
    {
        private readonly ISinhVienRepository _sinhVienRepository;

        public SinhVienService(ISinhVienRepository sinhVienRepository)
        {
            _sinhVienRepository = sinhVienRepository;
        }

        public void ThemSinhVien(string hoTen, string email, DateTime ngaySinh, string diaChi, string lop = "")
        {
            var sinhVien = new SinhVien(0, hoTen, email, ngaySinh, diaChi, lop);
            _sinhVienRepository.ThemSinhVien(sinhVien);
        }

        public List<SinhVien> LayTatCaSinhVien()
        {
            return _sinhVienRepository.LayTatCaSinhVien();
        }

        public SinhVien LaySinhVienTheoId(int id)
        {
            return _sinhVienRepository.LaySinhVienTheoId(id);
        }

        public void CapNhatSinhVien(int id, string hoTen, string email, DateTime ngaySinh, string diaChi, string lop = "")
        {
            var sinhVien = new SinhVien(id, hoTen, email, ngaySinh, diaChi, lop);
            _sinhVienRepository.CapNhatSinhVien(sinhVien);
        }

        public void XoaSinhVien(int id)
        {
            _sinhVienRepository.XoaSinhVien(id);
        }
    }
}