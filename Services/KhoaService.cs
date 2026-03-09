using System.Collections.Generic;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.Services
{
    public class KhoaService
    {
        private readonly IKhoaRepository _khoaRepository;

        public KhoaService(IKhoaRepository khoaRepository)
        {
            _khoaRepository = khoaRepository;
        }

        public void ThemKhoa(string tenKhoa)
        {
            var khoa = new Khoa(0, tenKhoa);
            _khoaRepository.ThemKhoa(khoa);
        }

        public List<Khoa> LayTatCaKhoa()
        {
            return _khoaRepository.LayTatCaKhoa();
        }

        public Khoa LayKhoaTheoId(int id)
        {
            return _khoaRepository.LayKhoaTheoId(id);
        }

        public void CapNhatKhoa(int id, string tenKhoa)
        {
            var khoa = new Khoa(id, tenKhoa);
            _khoaRepository.CapNhatKhoa(khoa);
        }

        public void XoaKhoa(int id)
        {
            _khoaRepository.XoaKhoa(id);
        }
    }
}