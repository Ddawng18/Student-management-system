using System;

namespace StudentManagementSystem.Models
{
    public class LogHoc
    {
        public int MaLop { get; set; }
        public string TenLop { get; set; }

        public LogHoc(int maLop, string tenLop)
        {
            MaLop = maLop;
            TenLop = tenLop;
        }

        public override string ToString()
        {
            return $"Mã lớp: {MaLop}, Tên lớp: {TenLop}";
        }
    }
}