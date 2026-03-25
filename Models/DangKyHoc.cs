using System;

namespace StudentManagementSystem.Models
{
    public class DangKyHoc
    {
        private SinhVien _sinhVien;
        private MonHoc _monHoc;
        private HocKy _hocKy;
        private float _diem;
        private string _ketQua;

        public SinhVien SinhVien
        {
            get { return _sinhVien; }
        }

        public MonHoc MonHoc
        {
            get { return _monHoc; }
        }

        public HocKy HocKy
        {
            get { return _hocKy; }
        }

        public float Diem
        {
            get { return _diem; }
        }

        public string KetQua
        {
            get { return _ketQua; }
            protected set { _ketQua = value; }
        }

        public DangKyHoc(SinhVien sinhVien, MonHoc monHoc, HocKy hocKy)
        {
            _sinhVien = sinhVien ?? throw new ArgumentNullException(nameof(sinhVien));
            _monHoc = monHoc ?? throw new ArgumentNullException(nameof(monHoc));
            _hocKy = hocKy ?? throw new ArgumentNullException(nameof(hocKy));
            _diem = -1f;
            _ketQua = "Chưa có điểm";

            _monHoc.ThemDangKy(this);
            _sinhVien.ThemDangKy(this);
        }

        public void NhapDiem(float diem)
        {
            if (diem < 0 || diem > 10)
            {
                throw new ArgumentException("Điểm phải trong khoảng 0 đến 10.", nameof(diem));
            }

            _diem = diem;
            TinhKetQua();
        }

        public virtual void TinhKetQua()
        {
            if (_diem < 0)
            {
                _ketQua = "Chưa có điểm";
                return;
            }

            if (_diem >= 5f)
            {
                _ketQua = "Đạt";
                return;
            }

            _ketQua = "Rớt";
        }

        public virtual string LayThongTinDangKy()
        {
            return _sinhVien.MaSinhVien + " - " + _monHoc.MaMonHoc + " - " + _hocKy.MaHocKy + " => " + _ketQua;
        }
    }

    public sealed class DangKyHocHeChatLuongCao : DangKyHoc
    {
        public DangKyHocHeChatLuongCao(SinhVien sinhVien, MonHoc monHoc, HocKy hocKy)
            : base(sinhVien, monHoc, hocKy)
        {
        }

        public override void TinhKetQua()
        {
            if (Diem < 0)
            {
                KetQua = "Chưa có điểm";
                return;
            }

            if (Diem >= 6.5f)
            {
                KetQua = "Đạt (CLC)";
                return;
            }

            KetQua = "Rớt (CLC)";
        }

        public override string LayThongTinDangKy()
        {
            return "[CLC] " + base.LayThongTinDangKy();
        }
    }
}
