using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    // Encapsulation + Polymorphism: đóng gói dữ liệu và cho phép lớp con thay đổi cách mô tả khoa.
    public class Khoa
    {
        private readonly List<LopHoc> _danhSachLopHoc;

        public string MaKhoa { get; private set; }
        public string TenKhoa { get; private set; }

        public IReadOnlyList<LopHoc> DanhSachLopHoc
        {
            get { return _danhSachLopHoc.AsReadOnly(); }
        }

        public Khoa(string maKhoa, string tenKhoa)
        {
            if (string.IsNullOrWhiteSpace(maKhoa))
            {
                throw new ArgumentException("Mã khoa không được để trống.", nameof(maKhoa));
            }

            if (string.IsNullOrWhiteSpace(tenKhoa))
            {
                throw new ArgumentException("Tên khoa không được để trống.", nameof(tenKhoa));
            }

            MaKhoa = maKhoa;
            TenKhoa = tenKhoa;
            _danhSachLopHoc = new List<LopHoc>();
        }

        public void DoiTenKhoa(string tenKhoaMoi)
        {
            if (string.IsNullOrWhiteSpace(tenKhoaMoi))
            {
                throw new ArgumentException("Tên khoa mới không được để trống.", nameof(tenKhoaMoi));
            }

            TenKhoa = tenKhoaMoi;
        }

        public void ThemLopHoc(LopHoc lopHoc)
        {
            if (lopHoc == null)
            {
                throw new ArgumentNullException(nameof(lopHoc));
            }

            if (!_danhSachLopHoc.Contains(lopHoc))
            {
                _danhSachLopHoc.Add(lopHoc);
            }
        }

        public void XoaLopHoc(LopHoc lopHoc)
        {
            if (lopHoc == null)
            {
                throw new ArgumentNullException(nameof(lopHoc));
            }

            _danhSachLopHoc.Remove(lopHoc);
        }

        public virtual string LayMoTaKhoa()
        {
            return $"Khoa {TenKhoa} có {_danhSachLopHoc.Count} lớp học.";
        }

        public override string ToString()
        {
            return LayMoTaKhoa();
        }
    }

    // Polymorphism: override hành vi mô tả từ lớp cha Khoa.
    public class KhoaCongNgheThongTin : Khoa
    {
        public KhoaCongNgheThongTin(string maKhoa, string tenKhoa)
            : base(maKhoa, tenKhoa)
        {
        }

        public override string LayMoTaKhoa()
        {
            return $"{TenKhoa} (CNTT) tập trung đào tạo lập trình và hệ thống thông tin.";
        }
    }
}
