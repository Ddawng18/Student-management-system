using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public interface ILopHocRepository
    {
        void ThemLopHoc(LopHoc lopHoc);
        List<LopHoc> LayTatCaLopHoc();
        LopHoc LayLopHocTheoId(int id);
        void CapNhatLopHoc(LopHoc lopHoc);
        void XoaLopHoc(int id);
    }
}