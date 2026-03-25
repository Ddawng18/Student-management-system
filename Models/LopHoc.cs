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

            MaLop = maLop.Trim();
            TenLop = tenLop.Trim();
            _danhSachSinhVien = new List<SinhVien>();
        }

        public void DoiTenLop(string tenLopMoi)
        {
            if (string.IsNullOrWhiteSpace(tenLopMoi))
            {
                throw new ArgumentException("Tên lớp mới không được để trống.", nameof(tenLopMoi));
            }

            TenLop = tenLopMoi.Trim();
        }

        public void ThemSinhVien(SinhVien sinhVien)
        {
            if (sinhVien == null)
            {
                throw new ArgumentNullException(nameof(sinhVien));
            }

            bool daTonTai = false;
            int index = 0;

            while (index < _danhSachSinhVien.Count)
            {
                if (ReferenceEquals(_danhSachSinhVien[index], sinhVien))
                {
                    daTonTai = true;
                    break;
                }

                index = index + 1;
            }

            if (!daTonTai)
            {
                _danhSachSinhVien.Add(sinhVien);
                sinhVien.GanVaoLop(this);
            }
        }

        public void XoaSinhVien(SinhVien sinhVien)
        {
            if (sinhVien == null)
            {
                throw new ArgumentNullException(nameof(sinhVien));
            }

            bool daXoa = _danhSachSinhVien.Remove(sinhVien);

            if (daXoa)
            {
                sinhVien.GanVaoLop(null);
            }
        }

        public override string ToString()
        {
            return $"Mã lớp: {MaLop}, Tên lớp: {TenLop}";
        }
    }
}