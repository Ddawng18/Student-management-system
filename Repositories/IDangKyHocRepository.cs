using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public interface IDangKyHocRepository
    {
        void ThemDangKy(DangKyHoc dangKyHoc);
        List<DangKyHoc> LayTatCaDangKy();
        List<DangKyHoc> LayDangKyTheoSinhVien(int maSinhVien);
        List<DangKyHoc> LayDangKyTheoMonHoc(int maMonHoc);
        void XoaDangKy(int id);
    }
}