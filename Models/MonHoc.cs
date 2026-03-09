using System;

namespace StudentManagementSystem.Models
{
    public class MonHoc
    {
        public int MaMonHoc { get; set; }
        public string TenMon { get; set; }
        public string MoTa { get; set; }
        public int SoTinChi { get; set; }

        public MonHoc(int maMonHoc, string tenMon, string moTa, int soTinChi)
        {
            MaMonHoc = maMonHoc;
            TenMon = tenMon;
            MoTa = moTa;
            SoTinChi = soTinChi;
        }

        public override string ToString()
        {
            return $"Mã môn: {MaMonHoc}, Tên môn: {TenMon}, Mô tả: {MoTa}, Số tín chỉ: {SoTinChi}";
        }
    }
}