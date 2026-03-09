using System;

namespace StudentManagementSystem.Models
{
    public class GiangVien
    {
        public int MaGiangVien { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public int MaKhoa { get; set; }

        public GiangVien(int maGiangVien, string hoTen, string email, int maKhoa = 0)
        {
            MaGiangVien = maGiangVien;
            HoTen = hoTen;
            Email = email;
            MaKhoa = maKhoa;
        }

        public void NhapDiem(DangKyHoc dangKyHoc, float diem)
        {
            dangKyHoc.Diem = diem;
            dangKyHoc.TinhKetQua();
        }

        public override string ToString()
        {
            return $"Mã GV: {MaGiangVien}, Họ tên: {HoTen}, Email: {Email}, Mã khoa: {MaKhoa}";
        }
    }
}