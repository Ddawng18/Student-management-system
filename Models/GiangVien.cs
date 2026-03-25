using System;

namespace StudentManagementSystem.Models
{
    public class GiangVien : Nguoi
    {
        private string _maGiangVien;

        public string MaGiangVien
        {
            get { return _maGiangVien; }
        }

        public GiangVien(string maGiangVien, string hoTen, string email)
            : base(maGiangVien, hoTen, email)
        {
            if (string.IsNullOrWhiteSpace(maGiangVien))
            {
                throw new ArgumentException("Mã giảng viên không được để trống.", nameof(maGiangVien));
            }

            _maGiangVien = maGiangVien.Trim();
        }

        public void CapNhatThongTinCoBan(string hoTenMoi, string emailMoi)
        {
            CapNhatHoTen(hoTenMoi);
            CapNhatEmail(emailMoi);
        }

        public void NhapDiem(DangKyHoc dangKyHoc, float diem)
        {
            if (dangKyHoc == null)
            {
                throw new ArgumentNullException(nameof(dangKyHoc));
            }

            dangKyHoc.NhapDiem(diem);
        }

        public string QuanLyMon(MonHoc monHoc)
        {
            if (monHoc == null)
            {
                throw new ArgumentNullException(nameof(monHoc));
            }

            return "Giảng viên " + HoTen + " đang phụ trách môn " + monHoc.TenMon + ".";
        }

        public override string LayVaiTro()
        {
            return "GiangVien";
        }

        public override string HienThiThongTin()
        {
            return "GiangVien - " + base.HienThiThongTin() + ", Mã GV: " + _maGiangVien;
        }
    }
}
