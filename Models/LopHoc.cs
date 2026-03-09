using System;

namespace StudentManagementSystem.Models
{
    public class LopHoc
    {
        public int MaLop { get; set; }
        public string TenLop { get; set; }
        public int MaKhoa { get; set; }

        public LopHoc(int maLop, string tenLop, int maKhoa = 0)
        {
            MaLop = maLop;
            TenLop = tenLop;
            MaKhoa = maKhoa;
        }

        public override string ToString()
        {
            return $"Mã lớp: {MaLop}, Tên lớp: {TenLop}, Mã khoa: {MaKhoa}";
        }
    }
}