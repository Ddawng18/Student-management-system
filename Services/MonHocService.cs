using System.Collections.Generic;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.Services
{
    public class MonHocService
    {
        private readonly IMonHocRepository _monHocRepository;

        public MonHocService(IMonHocRepository monHocRepository)
        {
            _monHocRepository = monHocRepository;
        }

        public void ThemMonHoc(string tenMon, string moTa, int soTinChi)
        {
            var monHoc = new MonHoc(0, tenMon, moTa, soTinChi);
            _monHocRepository.ThemMonHoc(monHoc);
        }

        public List<MonHoc> LayTatCaMonHoc()
        {
            return _monHocRepository.LayTatCaMonHoc();
        }

        public MonHoc LayMonHocTheoId(int id)
        {
            return _monHocRepository.LayMonHocTheoId(id);
        }

        public void CapNhatMonHoc(int id, string tenMon, string moTa, int soTinChi)
        {
            var monHoc = new MonHoc(id, tenMon, moTa, soTinChi);
            _monHocRepository.CapNhatMonHoc(monHoc);
        }

        public void XoaMonHoc(int id)
        {
            _monHocRepository.XoaMonHoc(id);
        }
    }
}