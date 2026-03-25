using System;
using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services
{
    public class KhoaService : DichVuQuanLyCoSo<Khoa>
    {
        protected override string LayKhoa(Khoa doiTuong)
        {
            return doiTuong.MaKhoa;
        }

        public IReadOnlyList<Khoa> TimTheoTen(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                return new List<Khoa>().AsReadOnly();
            }

            string duLieuCanTim = tuKhoa.Trim().ToLowerInvariant();
            List<Khoa> ketQua = new List<Khoa>();
            int index = 0;

            while (index < DuLieuNoiBo.Count)
            {
                Khoa khoa = DuLieuNoiBo[index];

                if (khoa.TenKhoa.ToLowerInvariant().Contains(duLieuCanTim))
                {
                    ketQua.Add(khoa);
                }

                index = index + 1;
            }

            return ketQua.AsReadOnly();
        }

        public void GanLopVaoKhoa(string maKhoa, LopHoc lopHoc)
        {
            if (lopHoc == null)
            {
                throw new ArgumentNullException(nameof(lopHoc));
            }

            Khoa? khoa = TimTheoMa(maKhoa);

            if (khoa == null)
            {
                throw new InvalidOperationException("Không tìm thấy khoa để gán lớp.");
            }

            khoa.ThemLopHoc(lopHoc);
        }
    }
}
