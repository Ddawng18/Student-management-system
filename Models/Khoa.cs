using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    public class Khoa
    {
        private readonly List<LopHoc> _danhSachLopHoc;
        private const string MaTruongKhoaMacDinh = "GV_MAC_DINH";
        private string _maKhoa;
        private string _tenKhoa;
        private GiangVien _truongKhoa;

        public string MaKhoa
        {
            get { return _maKhoa; }
        }

        public string TenKhoa
        {
            get { return _tenKhoa; }
        }

        public GiangVien TruongKhoa
        {
            get { return _truongKhoa; }
        }

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

            _maKhoa = maKhoa.Trim();
            _tenKhoa = tenKhoa.Trim();
            _danhSachLopHoc = new List<LopHoc>();
            _truongKhoa = new GiangVien(MaTruongKhoaMacDinh, "Chưa phân công", "chuaphancong@khoa.local");
        }

        public void DoiTenKhoa(string tenKhoaMoi)
        {
            if (string.IsNullOrWhiteSpace(tenKhoaMoi))
            {
                throw new ArgumentException("Tên khoa mới không được để trống.", nameof(tenKhoaMoi));
            }

            _tenKhoa = tenKhoaMoi.Trim();
        }

        public void ThemLopHoc(LopHoc lopHoc)
        {
            if (lopHoc == null)
            {
                throw new ArgumentNullException(nameof(lopHoc));
            }

            bool daTonTai = false;
            int index = 0;

            while (index < _danhSachLopHoc.Count)
            {
                if (ReferenceEquals(_danhSachLopHoc[index], lopHoc))
                {
                    daTonTai = true;
                    break;
                }

                index = index + 1;
            }

            if (!daTonTai)
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

        public void GanTruongKhoa(GiangVien giangVien)
        {
            if (giangVien == null)
            {
                throw new ArgumentNullException(nameof(giangVien));
            }

            _truongKhoa = giangVien;
        }

        public void HuyTruongKhoa()
        {
            _truongKhoa = new GiangVien(MaTruongKhoaMacDinh, "Chưa phân công", "chuaphancong@khoa.local");
        }

        public virtual string LayMoTaKhoa()
        {
            string thongTinTruongKhoa;

            if (_truongKhoa.MaGiangVien == MaTruongKhoaMacDinh)
            {
                thongTinTruongKhoa = "Chưa phân công trưởng khoa";
            }
            else
            {
                thongTinTruongKhoa = "Trưởng khoa: " + _truongKhoa.HoTen;
            }

            return "Khoa " + _tenKhoa + " có " + _danhSachLopHoc.Count + " lớp học. " + thongTinTruongKhoa + ".";
        }

        public override string ToString()
        {
            return LayMoTaKhoa();
        }
    }

    public class KhoaCongNgheThongTin : Khoa
    {
        public KhoaCongNgheThongTin(string maKhoa, string tenKhoa)
            : base(maKhoa, tenKhoa)
        {
        }

        public override string LayMoTaKhoa()
        {
            return TenKhoa + " (CNTT) tập trung đào tạo lập trình và hệ thống thông tin.";
        }
    }

    public class KhoaHeThongThongTinKinhDoanh : Khoa
    {
        public KhoaHeThongThongTinKinhDoanh(string maKhoa, string tenKhoa)
            : base(maKhoa, tenKhoa)
        {
        }

        public override string LayMoTaKhoa()
        {
            return $"{TenKhoa} (HTTTKD) tập trung vào phân tích dữ liệu và hệ thống kinh doanh.";
        }
    }

    public class KhoaKeToan : Khoa
    {
        public KhoaKeToan(string maKhoa, string tenKhoa)
            : base(maKhoa, tenKhoa)
        {
        }

        public override string LayMoTaKhoa()
        {
            return $"{TenKhoa} (Kế toán) chuyên đào tạo về tài chính, kế toán và kiểm toán.";
        }
    }
}
