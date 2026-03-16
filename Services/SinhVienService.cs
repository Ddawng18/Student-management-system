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

        // Thêm sinh viên
        public void ThemSinhVien(SinhVien sinhVien)
        {
            _sinhVienRepository.ThemSinhVien(sinhVien);
        }

        // Lấy tất cả sinh viên
        public List<SinhVien> LayTatCaSinhVien()
        {
            return _sinhVienRepository.LayTatCaSinhVien();
        }

        // Lấy sinh viên theo ID
        public SinhVien LaySinhVienTheoId(int id)
        {
            return _sinhVienRepository.LaySinhVienTheoId(id);
        }

        // Cập nhật sinh viên
        public void CapNhatSinhVien(SinhVien sinhVien)
        {
            _sinhVienRepository.CapNhatSinhVien(sinhVien);
        }

        // Xóa sinh viên
        public void XoaSinhVien(int id)
        {
            _sinhVienRepository.XoaSinhVien(id);
        }

        // Tìm sinh viên theo tên
        public List<SinhVien> TimSinhVienTheoTen(string ten)
        {
            return _sinhVienRepository.TimSinhVienTheoTen(ten);
        }

        // Tìm sinh viên theo lớp
        public List<SinhVien> TimSinhVienTheoLop(int maLop)
        {
            return _sinhVienRepository.TimSinhVienTheoLop(maLop);
        }
    }
}