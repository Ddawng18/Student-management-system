using System.Collections.Generic;
using System.Linq;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public class GiangVienRepository : IGiangVienRepository
    {
        private readonly List<GiangVien> _giangViens = new List<GiangVien>();
        private int _nextId = 1;

        public void ThemGiangVien(GiangVien giangVien)
        {
            giangVien.MaGiangVien = _nextId++;
            _giangViens.Add(giangVien);
        }

        public List<GiangVien> LayTatCaGiangVien()
        {
            return _giangViens;
        }

        public GiangVien LayGiangVienTheoId(int id)
        {
            return _giangViens.FirstOrDefault(g => g.MaGiangVien == id);
        }

        public void CapNhatGiangVien(GiangVien giangVien)
        {
            var existing = LayGiangVienTheoId(giangVien.MaGiangVien);
            if (existing != null)
            {
                existing.HoTen = giangVien.HoTen;
                existing.Email = giangVien.Email;
            }
        }

        public void XoaGiangVien(int id)
        {
            var giangVien = LayGiangVienTheoId(id);
            if (giangVien != null)
            {
                _giangViens.Remove(giangVien);
            }
        }
    }
}