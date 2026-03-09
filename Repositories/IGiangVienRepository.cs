using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public interface IGiangVienRepository
    {
        void ThemGiangVien(GiangVien giangVien);
        List<GiangVien> LayTatCaGiangVien();
        GiangVien LayGiangVienTheoId(int id);
        void CapNhatGiangVien(GiangVien giangVien);
        void XoaGiangVien(int id);
    }
}