using System;

namespace StudentManagementSystem.Models
{
    public class GiangVien
    {
        public int MaGiangVien { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }

        public GiangVien(int maGiangVien, string hoTen, string email)
        {
            MaGiangVien = maGiangVien;
            HoTen = hoTen;
            Email = email;
        }

        public void NhapDiem(DangKyHoc dangKyHoc, float diem)
        {
            dangKyHoc.Diem = diem;
            dangKyHoc.TinhKetQua();
        }

        public override string ToString()
        {
            return $"Mã GV: {MaGiangVien}, Họ tên: {HoTen}, Email: {Email}";
        }
    }
}