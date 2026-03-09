using System;

namespace StudentManagementSystem.Models
{
    public class HocKy
    {
        public int MaHocKy { get; set; }
        public string TenHocKy { get; set; }
        public int NamHoc { get; set; }

        public HocKy(int maHocKy, string tenHocKy, int namHoc)
        {
            MaHocKy = maHocKy;
            TenHocKy = tenHocKy;
            NamHoc = namHoc;
        }

        public override string ToString()
        {
            return $"Mã học kỳ: {MaHocKy}, Tên: {TenHocKy}, Năm học: {NamHoc}";
        }
    }
}