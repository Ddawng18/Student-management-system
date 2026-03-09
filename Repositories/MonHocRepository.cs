using System.Collections.Generic;
using System.Linq;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public class MonHocRepository : IMonHocRepository
    {
        private readonly List<MonHoc> _monHocs = new List<MonHoc>();
        private int _nextId = 1;

        public void ThemMonHoc(MonHoc monHoc)
        {
            monHoc.MaMonHoc = _nextId++;
            _monHocs.Add(monHoc);
        }

        public List<MonHoc> LayTatCaMonHoc()
        {
            return _monHocs;
        }

        public MonHoc LayMonHocTheoId(int id)
        {
            return _monHocs.FirstOrDefault(c => c.MaMonHoc == id);
        }

        public void CapNhatMonHoc(MonHoc monHoc)
        {
            var existing = LayMonHocTheoId(monHoc.MaMonHoc);
            if (existing != null)
            {
                existing.TenMon = monHoc.TenMon;
                existing.MoTa = monHoc.MoTa;
                existing.SoTinChi = monHoc.SoTinChi;
            }
        }

        public void XoaMonHoc(int id)
        {
            var monHoc = LayMonHocTheoId(id);
            if (monHoc != null)
            {
                _monHocs.Remove(monHoc);
            }
        }
    }
}