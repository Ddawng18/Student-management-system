using System.Collections.Generic;
using System.Linq;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public class KhoaRepository : IKhoaRepository
    {
        private readonly List<Khoa> _khoas = new List<Khoa>();
        private int _nextId = 1;

        public void ThemKhoa(Khoa khoa)
        {
            khoa.MaKhoa = _nextId++;
            _khoas.Add(khoa);
        }

        public List<Khoa> LayTatCaKhoa()
        {
            return _khoas;
        }

        public Khoa LayKhoaTheoId(int id)
        {
            return _khoas.FirstOrDefault(k => k.MaKhoa == id);
        }

        public void CapNhatKhoa(Khoa khoa)
        {
            var existing = LayKhoaTheoId(khoa.MaKhoa);
            if (existing != null)
            {
                existing.TenKhoa = khoa.TenKhoa;
            }
        }

        public void XoaKhoa(int id)
        {
            var khoa = LayKhoaTheoId(id);
            if (khoa != null)
            {
                _khoas.Remove(khoa);
            }
        }
    }
}