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

        public SinhVien(int maSinhVien, string hoTen, string email, DateTime ngaySinh, string diaChi)
        {
            MaSinhVien = maSinhVien;
            HoTen = hoTen;
            Email = email;
            NgaySinh = ngaySinh;
            DiaChi = diaChi;
        }

        public override string ToString()
        {
            return $"Mã SV: {MaSinhVien}, Họ tên: {HoTen}, Email: {Email}, Ngày sinh: {NgaySinh.ToShortDateString()}, Địa chỉ: {DiaChi}";
        }
    }
}