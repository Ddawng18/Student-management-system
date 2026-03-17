using System;

namespace StudentManagementSystem.Models
{
    // inharitance from Nguoi
    public class GiangVien : Nguoi
    {
        public string MaGiangVien { get; private set; }

        public GiangVien(string maGiangVien, string hoTen, string email)
            : base(maGiangVien, hoTen, email)
        {
            MaGiangVien = maGiangVien;
        }

        public void NhapDiem(DangKyHoc dangKyHoc, float diem)
        {
            if (dangKyHoc == null)
            {
                throw new ArgumentNullException(nameof(dangKyHoc));
            }

            dangKyHoc.NhapDiem(diem);
        }

        public string QuanLyMon()
        {
            return $"Giảng viên {HoTen} đang quản lý môn học.";
        }

        public override string LayVaiTro()
        {
            return "GiangVien";
        }

        public override string HienThiThongTin()
        {
            return $"GiangVien - {base.HienThiThongTin()}";
        }
    }
}
