using System.Collections.Generic;
using System.Linq;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public class HocKyRepository : IHocKyRepository
    {
        private readonly List<HocKy> _hocKys = new List<HocKy>();
        private int _nextId = 1;

        public void ThemHocKy(HocKy hocKy)
        {
            hocKy.MaHocKy = _nextId++;
            _hocKys.Add(hocKy);
        }

        public List<HocKy> LayTatCaHocKy()
        {
            return _hocKys;
        }

        public HocKy LayHocKyTheoId(int id)
        {
            return _hocKys.FirstOrDefault(h => h.MaHocKy == id);
        }

        public void CapNhatHocKy(HocKy hocKy)
        {
            var existing = LayHocKyTheoId(hocKy.MaHocKy);
            if (existing != null)
            {
                existing.TenHocKy = hocKy.TenHocKy;
                existing.NamHoc = hocKy.NamHoc;
            }
        }

        public void XoaHocKy(int id)
        {
            var hocKy = LayHocKyTheoId(id);
            if (hocKy != null)
            {
                _hocKys.Remove(hocKy);
            }
        }
    }
}