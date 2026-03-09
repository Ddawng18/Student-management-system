using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public interface ILogHocRepository
    {
        void ThemLogHoc(LogHoc logHoc);
        List<LogHoc> LayTatCaLogHoc();
        LogHoc LayLogHocTheoId(int id);
        void CapNhatLogHoc(LogHoc logHoc);
        void XoaLogHoc(int id);
    }
}