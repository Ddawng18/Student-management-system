using System;

namespace StudentManagementSystem.Models
{
    public class Khoa
    {
        public int MaKhoa { get; set; }
        public string TenKhoa { get; set; }

        public Khoa(int maKhoa, string tenKhoa)
        {
            MaKhoa = maKhoa;
            TenKhoa = tenKhoa;
        }

        public override string ToString()
        {
            return $"Mã khoa: {MaKhoa}, Tên khoa: {TenKhoa}";
        }
    }
}