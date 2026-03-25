using System;

namespace StudentManagementSystem.Models
{
    public class HocKy
    {
        private readonly string _maHocKy;
        private string _tenHocKy;
        private int _namHoc;

        public string MaHocKy
        {
            get { return _maHocKy; }
        }

        public string TenHocKy
        {
            get { return _tenHocKy; }
        }

        public int NamHoc
        {
            get { return _namHoc; }
        }

        public HocKy(string maHocKy, string tenHocKy, int namHoc)
        {
            if (string.IsNullOrWhiteSpace(maHocKy))
            {
                throw new ArgumentException("Mã học kỳ không được để trống.", nameof(maHocKy));
            }

            if (string.IsNullOrWhiteSpace(tenHocKy))
            {
                throw new ArgumentException("Tên học kỳ không được để trống.", nameof(tenHocKy));
            }

            _maHocKy = maHocKy;
            _tenHocKy = tenHocKy;

            if (namHoc <= 0)
            {
                throw new ArgumentException("Năm học phải lớn hơn 0.", nameof(namHoc));
            }

            _namHoc = namHoc;
        }

        public void DoiTenHocKy(string tenHocKyMoi)
        {
            if (string.IsNullOrWhiteSpace(tenHocKyMoi))
            {
                throw new ArgumentException("Tên học kỳ mới không được để trống.", nameof(tenHocKyMoi));
            }

            _tenHocKy = tenHocKyMoi;
        }

        public void CapNhatNamHoc(int namHocMoi)
        {
            if (namHocMoi <= 0)
            {
                throw new ArgumentException("Năm học phải lớn hơn 0.", nameof(namHocMoi));
            }

            _namHoc = namHocMoi;
        }

        public override string ToString()
        {
            return _maHocKy + " - " + _tenHocKy + " (Năm học " + _namHoc + ")";
        }
    }
}
