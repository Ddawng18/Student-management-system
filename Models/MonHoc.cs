using System;

namespace StudentManagementSystem.Models
{
    public class MonHoc
    {
        public int MaMonHoc { get; set; }
        public string TenMon { get; set; }
        public string MoTa { get; set; }
        public int SoTinChi { get; set; }
        public int MaKhoa { get; set; }
        public int MaGiangVien { get; set; }

        public MonHoc(int maMonHoc, string tenMon, string moTa, int soTinChi, int maKhoa = 0, int maGiangVien = 0)
        {
            MaMonHoc = maMonHoc;
            TenMon = tenMon;
            MoTa = moTa;
            SoTinChi = soTinChi;
            MaKhoa = maKhoa;
            MaGiangVien = maGiangVien;
        }

        public void MoMonHoc()
        {
            Console.WriteLine($"Mô tả môn học {TenMon}: {MoTa}");
        }

        public override string ToString()
        {
            return $"Mã môn: {MaMonHoc}, Tên môn: {TenMon}, Mô tả: {MoTa}, Số tín chỉ: {SoTinChi}, Mã khoa: {MaKhoa}, Mã GV: {MaGiangVien}";
        }
    }
}