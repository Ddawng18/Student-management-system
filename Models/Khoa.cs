using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    // Polymorphism
    public class Khoa
    {
        private readonly List<LopHoc> _danhSachLopHoc;
        private const string MaTruongKhoaMacDinh = "GV_MAC_DINH";

        public string MaKhoa { get; private set; }
        public string TenKhoa { get; private set; }
        public GiangVien TruongKhoa { get; private set; }

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
            TruongKhoa = new GiangVien(MaTruongKhoaMacDinh, "Chưa phân công", "chuaphancong@khoa.local");
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

        // Association: Khoa liên kết với GiangVien qua vai trò Trưởng khoa.
        // Hai đối tượng vẫn tồn tại độc lập, không phụ thuộc lẫn nhau.
        public void GanTruongKhoa(GiangVien giangVien)
        {
            if (giangVien == null)
            {
                throw new ArgumentNullException(nameof(giangVien));
            }

            TruongKhoa = giangVien;
        }

        public void HuyTruongKhoa()
        {
            TruongKhoa = new GiangVien(MaTruongKhoaMacDinh, "Chưa phân công", "chuaphancong@khoa.local");
        }

        public virtual string LayMoTaKhoa()
        {
            string thongTinTruongKhoa;

            if (TruongKhoa.MaGiangVien == MaTruongKhoaMacDinh)
            {
                thongTinTruongKhoa = "Chưa phân công trưởng khoa";
            }
            else
            {
                thongTinTruongKhoa = $"Trưởng khoa: {TruongKhoa.HoTen}";
            }

            return $"Khoa {TenKhoa} có {_danhSachLopHoc.Count} lớp học. {thongTinTruongKhoa}.";
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
