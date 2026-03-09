using System.Collections.Generic;
using System.Linq;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public class DangKyHocRepository : IDangKyHocRepository
    {
        private readonly List<DangKyHoc> _dangKyHocs = new List<DangKyHoc>();
        private int _nextId = 1;

        public void ThemDangKy(DangKyHoc dangKyHoc)
        {
            dangKyHoc.MaDangKy = _nextId++;
            _dangKyHocs.Add(dangKyHoc);
        }

        public List<DangKyHoc> LayTatCaDangKy()
        {
            return _dangKyHocs;
        }

        public List<DangKyHoc> LayDangKyTheoSinhVien(int maSinhVien)
        {
            return _dangKyHocs.Where(e => e.MaSinhVien == maSinhVien).ToList();
        }

        public List<DangKyHoc> LayDangKyTheoMonHoc(int maMonHoc)
        {
            return _dangKyHocs.Where(e => e.MaMonHoc == maMonHoc).ToList();
        }

        public void XoaDangKy(int id)
        {
            var dangKyHoc = _dangKyHocs.FirstOrDefault(e => e.MaDangKy == id);
            if (dangKyHoc != null)
            {
                _dangKyHocs.Remove(dangKyHoc);
            }
        }
    }
}