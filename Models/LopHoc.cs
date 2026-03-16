using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    public class LopHoc
    {
        private readonly List<SinhVien> _danhSachSinhVien;

        public string MaLop { get; private set; }
        public string TenLop { get; private set; }
        public IReadOnlyList<SinhVien> DanhSachSinhVien
        {
            get { return _danhSachSinhVien.AsReadOnly(); }
        }

        public LopHoc(string maLop, string tenLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
            {
                throw new ArgumentException("Mã lớp không được để trống.", nameof(maLop));
            }

            if (string.IsNullOrWhiteSpace(tenLop))
            {
                throw new ArgumentException("Tên lớp không được để trống.", nameof(tenLop));
            }

            MaLop = maLop;
            TenLop = tenLop;
            _danhSachSinhVien = new List<SinhVien>();
        }

        public void ThemSinhVien(SinhVien sinhVien)
        {
            if (sinhVien == null)
            {
                throw new ArgumentNullException(nameof(sinhVien));
            }

            if (!_danhSachSinhVien.Contains(sinhVien))
            {
                _danhSachSinhVien.Add(sinhVien);
            }
        }

        public void XoaSinhVien(SinhVien sinhVien)
        {
            if (sinhVien == null)
            {
                throw new ArgumentNullException(nameof(sinhVien));
            }

            _danhSachSinhVien.Remove(sinhVien);
        }

        public override string ToString()
        {
            return $"Mã lớp: {MaLop}, Tên lớp: {TenLop}";
        }
    }
}