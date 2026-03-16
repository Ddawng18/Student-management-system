using System;

namespace StudentManagementSystem.Models
{
    public class GiangVien : Nguoi
    {
        public int MaGiangVien { get; set; }
        public int MaKhoa { get; set; }

        public GiangVien(int maGiangVien, string hoTen, string email, int maKhoa = 0)
            : base(maGiangVien, hoTen, email)
        {
            MaGiangVien = maGiangVien;
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