using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public interface IMonHocRepository
    {
        void ThemMonHoc(MonHoc monHoc);
        List<MonHoc> LayTatCaMonHoc();
        MonHoc LayMonHocTheoId(int id);
        void CapNhatMonHoc(MonHoc monHoc);
        void XoaMonHoc(int id);
    }
}