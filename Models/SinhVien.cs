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
        public string Lop { get; set; }

        public SinhVien(int maSinhVien, string hoTen, string email, DateTime ngaySinh, string diaChi, string lop = "")
        {
            MaSinhVien = maSinhVien;
            HoTen = hoTen;
            Email = email;
            NgaySinh = ngaySinh;
            DiaChi = diaChi;
            Lop = lop;
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
            return $"Mã SV: {MaSinhVien}, Họ tên: {HoTen}, Email: {Email}, Ngày sinh: {NgaySinh.ToShortDateString()}, Lớp: {Lop}, Địa chỉ: {DiaChi}";
        }
    }
}