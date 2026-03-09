using System;

namespace StudentManagementSystem.Models
{
    public class SinhVien
    {
        public int MaSinhVien { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public int MaKhoa { get; set; }
        public int MaLop { get; set; }

        public SinhVien(int maSinhVien, string hoTen, string email, DateTime ngaySinh, string diaChi, int maKhoa = 0, int maLop = 0)
        {
            MaSinhVien = maSinhVien;
            HoTen = hoTen;
            Email = email;
            NgaySinh = ngaySinh;
            DiaChi = diaChi;
            MaKhoa = maKhoa;
            MaLop = maLop;
        }

        public void DangKyMonHoc(MonHoc mon)
        {
            // Method sẽ được gọi từ Services
        }

        public void XemKetQua()
        {
            // Method sẽ được gọi từ Services
        }

        public override string ToString()
        {
            return $"Mã SV: {MaSinhVien}, Họ tên: {HoTen}, Email: {Email}, Ngày sinh: {NgaySinh.ToShortDateString()}, Mã khoa: {MaKhoa}, Mã lớp: {MaLop}, Địa chỉ: {DiaChi}";
        }
    }
}