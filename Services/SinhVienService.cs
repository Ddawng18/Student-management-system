using System;
using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services
{
    public class SinhVienService : DichVuQuanLyCoSo<SinhVien>
    {
        protected override string LayKhoa(SinhVien doiTuong)
        {
            return doiTuong.MaSinhVien;
        }

        public IReadOnlyList<SinhVien> TimTheoTen(string ten)
        {
            if (string.IsNullOrWhiteSpace(ten))
            {
                return new List<SinhVien>().AsReadOnly();
            }

            string tenCanTim = ten.Trim().ToLowerInvariant();
            List<SinhVien> ketQua = new List<SinhVien>();
            int index = 0;

            while (index < DuLieuNoiBo.Count)
            {
                SinhVien sinhVien = DuLieuNoiBo[index];

                if (sinhVien.HoTen.ToLowerInvariant().Contains(tenCanTim))
                {
                    ketQua.Add(sinhVien);
                }

                index = index + 1;
            }

            return ketQua.AsReadOnly();
        }

        public IReadOnlyList<SinhVien> TimTheoLop(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
            {
                return new List<SinhVien>().AsReadOnly();
            }

            string maLopCanTim = maLop.Trim();
            List<SinhVien> ketQua = new List<SinhVien>();
            int index = 0;

            while (index < DuLieuNoiBo.Count)
            {
                SinhVien sinhVien = DuLieuNoiBo[index];
                LopHoc? lopHoc = sinhVien.LopHoc;

                if (lopHoc != null && string.Equals(lopHoc.MaLop, maLopCanTim, StringComparison.OrdinalIgnoreCase))
                {
                    ketQua.Add(sinhVien);
                }

                index = index + 1;
            }

            return ketQua.AsReadOnly();
        }

        public override void XoaTheoMa(string ma)
        {
            SinhVien? sinhVien = TimTheoMa(ma);

            if (sinhVien == null)
            {
                return;
            }

            IReadOnlyList<DangKyHoc> danhSachDangKy = sinhVien.DanhSachDangKy;
            List<DangKyHoc> banSaoDangKy = new List<DangKyHoc>();
            int indexBanSao = 0;

            while (indexBanSao < danhSachDangKy.Count)
            {
                banSaoDangKy.Add(danhSachDangKy[indexBanSao]);
                indexBanSao = indexBanSao + 1;
            }

            int index = 0;
            while (index < banSaoDangKy.Count)
            {
                DangKyHoc dangKyHoc = banSaoDangKy[index];
                dangKyHoc.MonHoc.XoaDangKy(dangKyHoc);
                sinhVien.XoaDangKy(dangKyHoc);
                index = index + 1;
            }

            base.XoaTheoMa(ma);
        }
    }
}