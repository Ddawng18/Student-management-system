using System;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services
{
    public class DangKyHocService : DichVuQuanLyCoSo<DangKyHoc>
    {
        protected override string LayKhoa(DangKyHoc doiTuong)
        {
            return TaoKhoaDangKy(doiTuong.SinhVien.MaSinhVien, doiTuong.MonHoc.MaMonHoc, doiTuong.HocKy.MaHocKy);
        }

        public DangKyHoc DangKyMonHoc(SinhVien sinhVien, MonHoc monHoc, HocKy hocKy)
        {
            KiemTraThamSoDangKy(sinhVien, monHoc, hocKy);

            string khoaDangKy = TaoKhoaDangKy(sinhVien.MaSinhVien, monHoc.MaMonHoc, hocKy.MaHocKy);
            DangKyHoc? daTonTai = TimNoiBoTheoMa(khoaDangKy);

            if (daTonTai != null)
            {
                throw new InvalidOperationException("Sinh viên đã đăng ký môn học này trong học kỳ đã chọn.");
            }

            DangKyHoc dangKyHoc = new DangKyHoc(sinhVien, monHoc, hocKy);
            Them(dangKyHoc);
            return dangKyHoc;
        }

        public DangKyHoc DangKyMonHocHeChatLuongCao(SinhVien sinhVien, MonHoc monHoc, HocKy hocKy)
        {
            KiemTraThamSoDangKy(sinhVien, monHoc, hocKy);

            string khoaDangKy = TaoKhoaDangKy(sinhVien.MaSinhVien, monHoc.MaMonHoc, hocKy.MaHocKy);
            DangKyHoc? daTonTai = TimNoiBoTheoMa(khoaDangKy);

            if (daTonTai != null)
            {
                throw new InvalidOperationException("Sinh viên đã đăng ký môn học này trong học kỳ đã chọn.");
            }

            DangKyHoc dangKyHoc = new DangKyHocHeChatLuongCao(sinhVien, monHoc, hocKy);
            Them(dangKyHoc);
            return dangKyHoc;
        }

        public void NhapDiem(DangKyHoc dangKyHoc, float diem)
        {
            if (dangKyHoc == null)
            {
                throw new ArgumentNullException(nameof(dangKyHoc));
            }

            dangKyHoc.NhapDiem(diem);
        }

        public string TinhKetQua(DangKyHoc dangKyHoc)
        {
            if (dangKyHoc == null)
            {
                throw new ArgumentNullException(nameof(dangKyHoc));
            }

            dangKyHoc.TinhKetQua();
            return dangKyHoc.KetQua;
        }

        private static string TaoKhoaDangKy(string maSinhVien, string maMonHoc, string maHocKy)
        {
            return maSinhVien + "|" + maMonHoc + "|" + maHocKy;
        }

        private static void KiemTraThamSoDangKy(SinhVien sinhVien, MonHoc monHoc, HocKy hocKy)
        {
            if (sinhVien == null)
            {
                throw new ArgumentNullException(nameof(sinhVien));
            }

            if (monHoc == null)
            {
                throw new ArgumentNullException(nameof(monHoc));
            }

            if (hocKy == null)
            {
                throw new ArgumentNullException(nameof(hocKy));
            }
        }
    }
}
