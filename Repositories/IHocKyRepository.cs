using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public interface IHocKyRepository
    {
        void ThemHocKy(HocKy hocKy);
        List<HocKy> LayTatCaHocKy();
        HocKy LayHocKyTheoId(int id);
        void CapNhatHocKy(HocKy hocKy);
        void XoaHocKy(int id);
    }
}