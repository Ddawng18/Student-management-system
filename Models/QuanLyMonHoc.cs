using System;
using System.Collections.Generic;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Models
{
    public class QuanLyMonHoc
    {
        private readonly MonHocService _monHocService;

        public QuanLyMonHoc()
        {
            _monHocService = new MonHocService();
        }

        public void ThemMonHoc(MonHoc monHoc)
        {
            _monHocService.Them(monHoc);
        }

        public void CapNhatMonHoc(MonHoc monHoc)
        {
            _monHocService.CapNhat(monHoc);
        }

        public void XoaMonHoc(string maMonHoc)
        {
            _monHocService.XoaTheoMa(maMonHoc);
        }

        public IReadOnlyList<MonHoc> LayDanhSachMonHoc()
        {
            return _monHocService.LayTatCa();
        }

        public MonHoc? TimMonHocTheoMa(string maMonHoc)
        {
            return _monHocService.TimTheoMa(maMonHoc);
        }
    }
}
