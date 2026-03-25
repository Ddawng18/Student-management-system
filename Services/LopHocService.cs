using System;
using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services
{
    public class LopHocService : DichVuQuanLyCoSo<LopHoc>
    {
        protected override string LayKhoa(LopHoc doiTuong)
        {
            return doiTuong.MaLop;
        }

        public IReadOnlyList<LopHoc> TimTheoTen(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                return new List<LopHoc>().AsReadOnly();
            }

            string duLieuCanTim = tuKhoa.Trim().ToLowerInvariant();
            List<LopHoc> ketQua = new List<LopHoc>();
            int index = 0;

            while (index < DuLieuNoiBo.Count)
            {
                LopHoc lopHoc = DuLieuNoiBo[index];

                if (lopHoc.TenLop.ToLowerInvariant().Contains(duLieuCanTim))
                {
                    ketQua.Add(lopHoc);
                }

                index = index + 1;
            }

            return ketQua.AsReadOnly();
        }
    }
}
