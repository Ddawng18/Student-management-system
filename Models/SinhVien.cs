using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    // Inheritance: SinhVien kế thừa từ lớp trừu tượng Nguoi.
    public class SinhVien : Nguoi
    {
        private readonly List<DangKyHoc> _danhSachDangKy;

        public string MaSinhVien { get; private set; }
        public DateTime NgaySinh { get; private set; }
        public LopHoc? LopHoc { get; private set; } //cho phép đọc từ bên ngoài //private set → chỉ class này được phép gán
        public IReadOnlyList<DangKyHoc> DanhSachDangKy
        {
            get { return _danhSachDangKy.AsReadOnly(); }
        }

        public SinhVien(string maSinhVien, string hoTen, string email, DateTime ngaySinh)
            : base(maSinhVien, hoTen, email)
        {
            if (string.IsNullOrWhiteSpace(maSinhVien))
            {
                throw new ArgumentException("Mã sinh viên không được để trống.", nameof(maSinhVien));
            }

            MaSinhVien = maSinhVien;
            NgaySinh = ngaySinh;
            _danhSachDangKy = new List<DangKyHoc>();
        }
        internal void GanVaoLop(LopHoc lop)
        {
            LopHoc = lop;
        }

        public void DangKyMonHoc(MonHoc monHoc, HocKy hocKy)
        {
            if (monHoc == null)
            {
                throw new ArgumentNullException(nameof(monHoc));
            }

            if (hocKy == null)
            {
                throw new ArgumentNullException(nameof(hocKy));
            }

            DangKyHoc dangKyHoc = new DangKyHoc(this, monHoc, hocKy);
            _danhSachDangKy.Add(dangKyHoc);
        }

        public string XemKetQuaHocTap()
        {
            if (_danhSachDangKy.Count == 0)
            {
                return "Chưa có kết quả học tập.";
            }

            List<string> ketQua = new List<string>();
            foreach (DangKyHoc dangKyHoc in _danhSachDangKy)
            {
                ketQua.Add($"{dangKyHoc.MonHoc.TenMon}: {dangKyHoc.KetQua} ({dangKyHoc.Diem})");
            }

            return string.Join(Environment.NewLine, ketQua);
        }

        public override string LayVaiTro()
        {
            return "SinhVien";
        }

    }
}