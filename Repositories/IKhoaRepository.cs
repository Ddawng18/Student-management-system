using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public interface IKhoaRepository
    {
        void ThemKhoa(Khoa khoa);
        List<Khoa> LayTatCaKhoa();
        Khoa LayKhoaTheoId(int id);
        void CapNhatKhoa(Khoa khoa);
        void XoaKhoa(int id);
    }
}