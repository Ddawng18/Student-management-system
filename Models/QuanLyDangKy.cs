using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    public class QuanLyDangKy
    {
        private readonly List<DangKyHoc> _danhSachDangKy;

        public QuanLyDangKy()
        {
            _danhSachDangKy = new List<DangKyHoc>();
        }

        public DangKyHoc DangKyMonHoc(SinhVien sinhVien, MonHoc monHoc, HocKy hocKy)
        {
            if (sinhVien == null)
            {
                throw new ArgumentNullException(nameof(sinhVien));
            }

            if (monHoc == null)
            {
                throw new ArgumentNullException(nameof(monHoc));
            }

            if (hocKy == null)
            {
                throw new ArgumentNullException(nameof(hocKy));
            }

            DangKyHoc dangKyHoc = new DangKyHoc(sinhVien, monHoc, hocKy);
            _danhSachDangKy.Add(dangKyHoc);
            return dangKyHoc;
        }

        public void NhapDiem(DangKyHoc dangKyHoc, float diem)
        {
            if (dangKyHoc == null)
            {
                throw new ArgumentNullException(nameof(dangKyHoc));
            }

            dangKyHoc.NhapDiem(diem);
        }

        public string TinhKetQua(DangKyHoc dangKyHoc)
        {
            if (dangKyHoc == null)
            {
                throw new ArgumentNullException(nameof(dangKyHoc));
            }

            dangKyHoc.TinhKetQua();
            return dangKyHoc.KetQua;
        }

        public IReadOnlyList<DangKyHoc> LayDanhSachDangKy()
        {
            return _danhSachDangKy.AsReadOnly();
        }
    }
}
