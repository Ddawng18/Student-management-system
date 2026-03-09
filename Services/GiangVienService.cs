using System.Collections.Generic;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.Services
{
    public class GiangVienService
    {
        private readonly IGiangVienRepository _giangVienRepository;

        public GiangVienService(IGiangVienRepository giangVienRepository)
        {
            _giangVienRepository = giangVienRepository;
        }

        public void ThemGiangVien(string hoTen, string email)
        {
            var giangVien = new GiangVien(0, hoTen, email);
            _giangVienRepository.ThemGiangVien(giangVien);
        }

        public List<GiangVien> LayTatCaGiangVien()
        {
            return _giangVienRepository.LayTatCaGiangVien();
        }

        public GiangVien LayGiangVienTheoId(int id)
        {
            return _giangVienRepository.LayGiangVienTheoId(id);
        }

        public void CapNhatGiangVien(int id, string hoTen, string email)
        {
            var giangVien = new GiangVien(id, hoTen, email);
            _giangVienRepository.CapNhatGiangVien(giangVien);
        }

        public void XoaGiangVien(int id)
        {
            _giangVienRepository.XoaGiangVien(id);
        }

        public void NhapDiem(DangKyHoc dangKyHoc, float diem)
        {
            var giangVien = new GiangVien(0, "", "");
            giangVien.NhapDiem(dangKyHoc, diem);
        }
    }
}