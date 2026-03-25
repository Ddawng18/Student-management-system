using System;
using System.Collections.Generic;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Models
{
    public class QuanLyDangKy
    {
        private readonly DangKyHocService _dangKyHocService;

        public QuanLyDangKy()
        {
            _dangKyHocService = new DangKyHocService();
        }

        public DangKyHoc DangKyMonHoc(SinhVien sinhVien, MonHoc monHoc, HocKy hocKy)
        {
            return _dangKyHocService.DangKyMonHoc(sinhVien, monHoc, hocKy);
        }

        public DangKyHoc DangKyMonHocHeChatLuongCao(SinhVien sinhVien, MonHoc monHoc, HocKy hocKy)
        {
            return _dangKyHocService.DangKyMonHocHeChatLuongCao(sinhVien, monHoc, hocKy);
        }

        public void NhapDiem(DangKyHoc dangKyHoc, float diem)
        {
            _dangKyHocService.NhapDiem(dangKyHoc, diem);
        }

        public string TinhKetQua(DangKyHoc dangKyHoc)
        {
            return _dangKyHocService.TinhKetQua(dangKyHoc);
        }

        public IReadOnlyList<DangKyHoc> LayDanhSachDangKy()
        {
            return _dangKyHocService.LayTatCa();
        }
    }
}
