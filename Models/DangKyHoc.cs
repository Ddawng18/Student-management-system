using System;

namespace StudentManagementSystem.Models
{
    public class DangKyHoc
    {
        public int MaDangKy { get; set; }
        public int MaSinhVien { get; set; }
        public int MaMonHoc { get; set; }
        public DateTime NgayDangKy { get; set; }
        public float Diem { get; set; }
        public string KetQua { get; set; }
        public int MaHocKy { get; set; }

        public DangKyHoc(int maDangKy, int maSinhVien, int maMonHoc, DateTime ngayDangKy, int maHocKy = 0)
        {
            MaDangKy = maDangKy;
            MaSinhVien = maSinhVien;
            MaMonHoc = maMonHoc;
            NgayDangKy = ngayDangKy;
            Diem = 0;
            KetQua = "Chưa có điểm";
            MaHocKy = maHocKy;
        }

        public void TinhKetQua()
        {
            if (Diem >= 5)
                KetQua = "Đạt";
            else if (Diem >= 0)
                KetQua = "Rớt";
            else
                KetQua = "Chưa có điểm";
        }

        public override string ToString()
        {
            return $"Mã đăng ký: {MaDangKy}, Mã SV: {MaSinhVien}, Mã môn: {MaMonHoc}, Ngày đăng ký: {NgayDangKy.ToShortDateString()}, Mã HK: {MaHocKy}, Điểm: {Diem}, Kết quả: {KetQua}";
        }
    }
}