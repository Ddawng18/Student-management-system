using System.Collections.Generic;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.Services
{
    public class HocSinhService : ISinhVienService
    {
        // Encapsulation: repository được đóng gói trong service
        private readonly ISinhVienRepository _sinhVienRepository;

        public HocSinhService(ISinhVienRepository sinhVienRepository)
        {
            _sinhVienRepository = sinhVienRepository;
        }

        // Thêm học sinh
        public void ThemSinhVien(SinhVien sinhVien)
        {
            _sinhVienRepository.ThemSinhVien(sinhVien);
        }

        // Lấy tất cả học sinh
        public List<SinhVien> LayTatCaSinhVien()
        {
            return _sinhVienRepository.LayTatCaSinhVien();
        }

        // Lấy học sinh theo ID
        public SinhVien LaySinhVienTheoId(int id)
        {
            return _sinhVienRepository.LaySinhVienTheoId(id);
        }

        // Cập nhật học sinh
        public void CapNhatSinhVien(SinhVien sinhVien)
        {
            _sinhVienRepository.CapNhatSinhVien(sinhVien);
        }

        // Xóa học sinh
        public void XoaSinhVien(int id)
        {
            _sinhVienRepository.XoaSinhVien(id);
        }

        // Tìm học sinh theo tên
        public List<SinhVien> TimSinhVienTheoTen(string ten)
        {
            return _sinhVienRepository.TimSinhVienTheoTen(ten);
        }

        // Tìm học sinh theo lớp
        public List<SinhVien> TimSinhVienTheoLop(int maLop)
        {
            return _sinhVienRepository.TimSinhVienTheoLop(maLop);
        }
    }
}