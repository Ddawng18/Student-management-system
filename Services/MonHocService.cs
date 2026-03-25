using System;
using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services
{
    public class MonHocService : DichVuQuanLyCoSo<MonHoc>
    {
        protected override string LayKhoa(MonHoc doiTuong)
        {
            return doiTuong.MaMonHoc;
        }

        public IReadOnlyList<MonHoc> TimTheoSoTinChi(int soTinChi)
        {
            List<MonHoc> ketQua = new List<MonHoc>();
            int index = 0;

            while (index < DuLieuNoiBo.Count)
            {
                MonHoc monHoc = DuLieuNoiBo[index];

                if (monHoc.SoTinChi == soTinChi)
                {
                    ketQua.Add(monHoc);
                }

                index = index + 1;
            }

            return ketQua.AsReadOnly();
        }

        public IReadOnlyList<MonHoc> TimTheoTen(string tenMon)
        {
            if (string.IsNullOrWhiteSpace(tenMon))
            {
                return new List<MonHoc>().AsReadOnly();
            }

            string tenCanTim = tenMon.Trim().ToLowerInvariant();
            List<MonHoc> ketQua = new List<MonHoc>();
            int index = 0;

            while (index < DuLieuNoiBo.Count)
            {
                MonHoc monHoc = DuLieuNoiBo[index];

                if (monHoc.TenMon.ToLowerInvariant().Contains(tenCanTim))
                {
                    ketQua.Add(monHoc);
                }

                index = index + 1;
            }

            return ketQua.AsReadOnly();
        }
    }
}
