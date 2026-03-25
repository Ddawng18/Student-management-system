using System;
using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services
{
    public class GiangVienService : DichVuQuanLyCoSo<GiangVien>
    {
        protected override string LayKhoa(GiangVien doiTuong)
        {
            return doiTuong.MaGiangVien;
        }

        public IReadOnlyList<GiangVien> TimTheoTen(string ten)
        {
            if (string.IsNullOrWhiteSpace(ten))
            {
                return new List<GiangVien>().AsReadOnly();
            }

            string tenCanTim = ten.Trim().ToLowerInvariant();
            List<GiangVien> ketQua = new List<GiangVien>();
            int index = 0;

            while (index < DuLieuNoiBo.Count)
            {
                GiangVien giangVien = DuLieuNoiBo[index];

                if (giangVien.HoTen.ToLowerInvariant().Contains(tenCanTim))
                {
                    ketQua.Add(giangVien);
                }

                index = index + 1;
            }

            return ketQua.AsReadOnly();
        }
    }
}
