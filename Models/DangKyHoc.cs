using System;

namespace StudentManagementSystem.Models
{
    public class DangKyHoc
    {
        public int MaDangKy { get; set; }
        public int MaSinhVien { get; set; }
        public int MaMonHoc { get; set; }
        public DateTime NgayDangKy { get; set; }

        public DangKyHoc(int maDangKy, int maSinhVien, int maMonHoc, DateTime ngayDangKy)
        {
            MaDangKy = maDangKy;
            MaSinhVien = maSinhVien;
            MaMonHoc = maMonHoc;
            NgayDangKy = ngayDangKy;
        }

        public override string ToString()
        {
            return $"Mã đăng ký: {MaDangKy}, Mã SV: {MaSinhVien}, Mã môn: {MaMonHoc}, Ngày đăng ký: {NgayDangKy.ToShortDateString()}";
        }
    }
}