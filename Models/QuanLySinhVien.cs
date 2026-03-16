using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    public class QuanLySinhVien
    {
        private readonly List<SinhVien> _danhSachSinhVien;

        public QuanLySinhVien()
        {
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

        public void CapNhatSinhVien(SinhVien sinhVien)
        {
            if (sinhVien == null)
            {
                throw new ArgumentNullException(nameof(sinhVien));
            }

            for (int index = 0; index < _danhSachSinhVien.Count; index++)
            {
                if (_danhSachSinhVien[index].MaSinhVien == sinhVien.MaSinhVien)
                {
                    _danhSachSinhVien[index] = sinhVien;
                    return;
                }
            }
        }

        public void XoaSinhVien(string maSinhVien)
        {
            if (string.IsNullOrWhiteSpace(maSinhVien))
            {
                throw new ArgumentException("Mã sinh viên không được để trống.", nameof(maSinhVien));
            }

            for (int index = 0; index < _danhSachSinhVien.Count; index++)
            {
                if (_danhSachSinhVien[index].MaSinhVien == maSinhVien)
                {
                    _danhSachSinhVien.RemoveAt(index);
                    return;
                }
            }
        }

        public IReadOnlyList<SinhVien> LayDanhSachSinhVien()
        {
            return _danhSachSinhVien.AsReadOnly();
        }
    }
}
