using System.Collections.Generic;
using System.Linq;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public class LopHocRepository : ILopHocRepository
    {
        private readonly List<LopHoc> _lopHocs = new List<LopHoc>();
        private int _nextId = 1;

        public void ThemLopHoc(LopHoc lopHoc)
        {
            lopHoc.MaLop = _nextId++;
            _lopHocs.Add(lopHoc);
        }

        public List<LopHoc> LayTatCaLopHoc()
        {
            return _lopHocs;
        }

        public LopHoc LayLopHocTheoId(int id)
        {
            return _lopHocs.FirstOrDefault(l => l.MaLop == id);
        }

        public void CapNhatLopHoc(LopHoc lopHoc)
        {
            var existing = LayLopHocTheoId(lopHoc.MaLop);
            if (existing != null)
            {
                existing.TenLop = lopHoc.TenLop;
            }
        }

        public void XoaLopHoc(int id)
        {
            var lopHoc = LayLopHocTheoId(id);
            if (lopHoc != null)
            {
                _lopHocs.Remove(lopHoc);
            }
        }
    }
}