using System.Collections.Generic;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services
{
    public class HocKyService : DichVuQuanLyCoSo<HocKy>
    {
        protected override string LayKhoa(HocKy doiTuong)
        {
            return doiTuong.MaHocKy;
        }

        public IReadOnlyList<HocKy> TimTheoNamHoc(int namHoc)
        {
            List<HocKy> ketQua = new List<HocKy>();
            int index = 0;

            while (index < DuLieuNoiBo.Count)
            {
                HocKy hocKy = DuLieuNoiBo[index];

                if (hocKy.NamHoc == namHoc)
                {
                    ketQua.Add(hocKy);
                }

                index = index + 1;
            }

            return ketQua.AsReadOnly();
        }
    }
}
