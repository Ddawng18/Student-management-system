using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    public class SinhVien : Nguoi
    {
        private readonly List<DangKyHoc> _danhSachDangKy;
        private string _maSinhVien;
        private DateTime _ngaySinh;
        private LopHoc? _lopHoc;

        public string MaSinhVien
        {
            get { return _maSinhVien; }
        }

        public DateTime NgaySinh
        {
            get { return _ngaySinh; }
        }

        public LopHoc? LopHoc
        {
            get { return _lopHoc; }
        }

        public IReadOnlyList<DangKyHoc> DanhSachDangKy
        {
            get { return _danhSachDangKy.AsReadOnly(); }
        }

        public SinhVien(string maSinhVien, string hoTen, string email, DateTime ngaySinh)
            : base(maSinhVien, hoTen, email)
        {
            if (string.IsNullOrWhiteSpace(maSinhVien))
            {
                throw new ArgumentException("Mã sinh viên không được để trống.", nameof(maSinhVien));
            }

            _maSinhVien = maSinhVien.Trim();
            _ngaySinh = ngaySinh;
            _danhSachDangKy = new List<DangKyHoc>();
            _lopHoc = null;
        }

        public void CapNhatThongTinCoBan(string hoTenMoi, string emailMoi)
        {
            CapNhatHoTen(hoTenMoi);
            CapNhatEmail(emailMoi);
        }

        public void CapNhatNgaySinh(DateTime ngaySinhMoi)
        {
            _ngaySinh = ngaySinhMoi;
        }

        internal void GanVaoLop(LopHoc? lop)
        {
            _lopHoc = lop;
        }

        internal void ThemDangKy(DangKyHoc dangKyHoc)
        {
            if (dangKyHoc == null)
            {
                throw new ArgumentNullException(nameof(dangKyHoc));
            }

            if (!_danhSachDangKy.Contains(dangKyHoc))
            {
                _danhSachDangKy.Add(dangKyHoc);
            }
        }

        internal void XoaDangKy(DangKyHoc dangKyHoc)
        {
            if (dangKyHoc == null)
            {
                throw new ArgumentNullException(nameof(dangKyHoc));
            }

            _danhSachDangKy.Remove(dangKyHoc);
        }

        public string XemKetQuaHocTap()
        {
            if (_danhSachDangKy.Count == 0)
            {
                return "Chưa có kết quả học tập.";
            }

            List<string> ketQua = new List<string>();
            int index = 0;

            while (index < _danhSachDangKy.Count)
            {
                DangKyHoc dangKyHoc = _danhSachDangKy[index];
                string dong = dangKyHoc.MonHoc.TenMon + ": " + dangKyHoc.KetQua + " (" + dangKyHoc.Diem.ToString("0.0") + ")";
                ketQua.Add(dong);
                index = index + 1;
            }

            return string.Join(Environment.NewLine, ketQua);
        }

        public override string LayVaiTro()
        {
            return "SinhVien";
        }

        public override string HienThiThongTin()
        {
            string thongTinLop = _lopHoc == null ? "Chưa có lớp" : _lopHoc.TenLop;
            return base.HienThiThongTin() + ", Mã SV: " + _maSinhVien + ", Lớp: " + thongTinLop;
        }
    }
}
