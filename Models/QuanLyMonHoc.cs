using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    public class QuanLyMonHoc
    {
        private readonly List<MonHoc> _danhSachMonHoc;

        public QuanLyMonHoc()
        {
            _danhSachMonHoc = new List<MonHoc>();
        }

        public void ThemMonHoc(MonHoc monHoc)
        {
            if (monHoc == null)
            {
                throw new ArgumentNullException(nameof(monHoc));
            }

            if (!_danhSachMonHoc.Contains(monHoc))
            {
                _danhSachMonHoc.Add(monHoc);
            }
        }

        public void CapNhatMonHoc(MonHoc monHoc)
        {
            if (monHoc == null)
            {
                throw new ArgumentNullException(nameof(monHoc));
            }

            for (int index = 0; index < _danhSachMonHoc.Count; index++)
            {
                if (_danhSachMonHoc[index].MaMonHoc == monHoc.MaMonHoc)
                {
                    _danhSachMonHoc[index] = monHoc;
                    return;
                }
            }
        }

        public void XoaMonHoc(string maMonHoc)
        {
            if (string.IsNullOrWhiteSpace(maMonHoc))
            {
                throw new ArgumentException("Mã môn học không được để trống.", nameof(maMonHoc));
            }

            for (int index = 0; index < _danhSachMonHoc.Count; index++)
            {
                if (_danhSachMonHoc[index].MaMonHoc == maMonHoc)
                {
                    _danhSachMonHoc.RemoveAt(index);
                    return;
                }
            }
        }

        public IReadOnlyList<MonHoc> LayDanhSachMonHoc()
        {
            return _danhSachMonHoc.AsReadOnly();
        }
    }
}
