using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public interface ISinhVienRepository
    {
        void ThemSinhVien(SinhVien sinhVien);
        List<SinhVien> LayTatCaSinhVien();
        SinhVien LaySinhVienTheoId(int id);
        void CapNhatSinhVien(SinhVien sinhVien);
        void XoaSinhVien(int id);
    }
}