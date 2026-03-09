using System;
using System.Collections.Generic;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.Services
{
    public class DangKyHocService
    {
        private readonly IDangKyHocRepository _dangKyHocRepository;
        private readonly ISinhVienRepository _sinhVienRepository;
        private readonly IMonHocRepository _monHocRepository;

        public DangKyHocService(IDangKyHocRepository dangKyHocRepository, ISinhVienRepository sinhVienRepository, IMonHocRepository monHocRepository)
        {
            _dangKyHocRepository = dangKyHocRepository;
            _sinhVienRepository = sinhVienRepository;
            _monHocRepository = monHocRepository;
        }

        public void DangKyMonHoc(int maSinhVien, int maMonHoc)
        {
            var sinhVien = _sinhVienRepository.LaySinhVienTheoId(maSinhVien);
            var monHoc = _monHocRepository.LayMonHocTheoId(maMonHoc);
            if (sinhVien != null && monHoc != null)
            {
                var dangKyHoc = new DangKyHoc(0, maSinhVien, maMonHoc, DateTime.Now);
                _dangKyHocRepository.ThemDangKy(dangKyHoc);
            }
            else
            {
                throw new ArgumentException("Mã sinh viên hoặc mã môn học không hợp lệ");
            }
        }

        public List<DangKyHoc> LayTatCaDangKyHoc()
        {
            return _dangKyHocRepository.LayTatCaDangKy();
        }

        public List<DangKyHoc> LayDangKyHocTheoSinhVien(int maSinhVien)
        {
            return _dangKyHocRepository.LayDangKyTheoSinhVien(maSinhVien);
        }

        public List<DangKyHoc> LayDangKyHocTheoMonHoc(int maMonHoc)
        {
            return _dangKyHocRepository.LayDangKyTheoMonHoc(maMonHoc);
        }

        public void HuyDangKyHoc(int maDangKy)
        {
            _dangKyHocRepository.XoaDangKy(maDangKy);
        }
    }
}