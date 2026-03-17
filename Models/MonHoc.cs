using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    public class MonHoc
    {
        private readonly List<DangKyHoc> _danhSachDangKy;
        private readonly string _maMonHoc;
        private string _tenMon;
        private int _soTinChi;

        public string MaMonHoc
        {
            get { return _maMonHoc; }
        }

        public string TenMon
        {
            get { return _tenMon; }
        }

        public int SoTinChi
        {
            get { return _soTinChi; }
        }

        public IReadOnlyList<DangKyHoc> DanhSachDangKy
        {
            get { return _danhSachDangKy.AsReadOnly(); }
        }

        public MonHoc(string maMonHoc, string tenMon, int soTinChi)
        {
            if (string.IsNullOrWhiteSpace(maMonHoc))
            {
                throw new ArgumentException("Mã môn học không được để trống.", nameof(maMonHoc));
            }

            if (string.IsNullOrWhiteSpace(tenMon))
            {
                throw new ArgumentException("Tên môn không được để trống.", nameof(tenMon));
            }

            if (soTinChi <= 0)
            {
                throw new ArgumentException("Số tín chỉ phải lớn hơn 0.", nameof(soTinChi));
            }

            _maMonHoc = maMonHoc;
            _tenMon = tenMon;
            _soTinChi = soTinChi;
            _danhSachDangKy = new List<DangKyHoc>();
        }

        public void DoiTenMon(string tenMonMoi)
        {
            if (string.IsNullOrWhiteSpace(tenMonMoi))
            {
                throw new ArgumentException("Tên môn mới không được để trống.", nameof(tenMonMoi));
            }

            _tenMon = tenMonMoi;
        }

        public void CapNhatSoTinChi(int soTinChiMoi)
        {
            if (soTinChiMoi <= 0)
            {
                throw new ArgumentException("Số tín chỉ phải lớn hơn 0.", nameof(soTinChiMoi));
            }

            _soTinChi = soTinChiMoi;
        }

        public void ThemDangKy(DangKyHoc dangKyHoc)
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

        public void XoaDangKy(DangKyHoc dangKyHoc)
        {
            if (dangKyHoc == null)
            {
                throw new ArgumentNullException(nameof(dangKyHoc));
            }

            _danhSachDangKy.Remove(dangKyHoc);
        }
    }
}
