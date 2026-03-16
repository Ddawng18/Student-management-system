using System;

namespace StudentManagementSystem.Models
{
    // Polymorphism: method TinhKetQua là virtual, có thể override theo quy tắc từng hệ đào tạo.
    public class DangKyHoc
    {
        public SinhVien SinhVien { get; private set; }
        public MonHoc MonHoc { get; private set; }
        public HocKy HocKy { get; private set; }
        public float Diem { get; private set; }
        public string KetQua { get; protected set; }

        public DangKyHoc(SinhVien sinhVien, MonHoc monHoc, HocKy hocKy)
        {
            SinhVien = sinhVien ?? throw new ArgumentNullException(nameof(sinhVien));
            MonHoc = monHoc ?? throw new ArgumentNullException(nameof(monHoc));
            HocKy = hocKy ?? throw new ArgumentNullException(nameof(hocKy));
            Diem = -1;
            KetQua = "Chưa có điểm";

            MonHoc.ThemDangKy(this);
        }

        public void NhapDiem(float diem)
        {
            if (diem < 0 || diem > 10)
            {
                throw new ArgumentException("Điểm phải trong khoảng 0 đến 10.", nameof(diem));
            }

            Diem = diem;
            TinhKetQua();
        }

        public virtual void TinhKetQua()
        {
            if (Diem < 0)
            {
                KetQua = "Chưa có điểm";
                return;
            }

            KetQua = Diem >= 5 ? "Đạt" : "Rớt";
        }
    }
}
