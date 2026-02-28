using System;
using System.Collections.Generic;

namespace QuanLySinhVienApp
{
    // ================= LỚP CHA =================
    public class Nguoi
    {
        public string HoTen { get; set; }
        public string Email { get; set; }

        public Nguoi() { }

        public Nguoi(string hoTen, string email)
        {
            HoTen = hoTen;
            Email = email;
        }

        public virtual void HienThiThongTin()
        {
            Console.WriteLine($"Ho ten: {HoTen}");
            Console.WriteLine($"Email: {Email}");
        }
    }

    // ================= SINH VIÊN =================
    public class SinhVien : Nguoi
    {
        public int MaSinhVien { get; set; }
        public string Lop { get; set; }

        public List<DangKyHoc> DanhSachDangKy { get; set; }

        public SinhVien() 
        {
            DanhSachDangKy = new List<DangKyHoc>();
        }

        public SinhVien(int ma, string hoTen, string email, string lop)
            : base(hoTen, email)
        {
            MaSinhVien = ma;
            Lop = lop;
            DanhSachDangKy = new List<DangKyHoc>();
        }

        public override void HienThiThongTin()
        {
            base.HienThiThongTin();
            Console.WriteLine($"Ma SV: {MaSinhVien}");
            Console.WriteLine($"Lop: {Lop}");
        }

        public void DangKyMonHoc(MonHoc mon)
        {
            DangKyHoc dk = new DangKyHoc(this, mon);
            DanhSachDangKy.Add(dk);
        }

        public void XemKetQua()
        {
            foreach (var dk in DanhSachDangKy)
            {
                Console.WriteLine($"Mon: {dk.Mon.TenMon} - Diem: {dk.Diem} - Ket qua: {dk.KetQua}");
            }
        }
    }

    // ================= GIẢNG VIÊN =================
    public class GiangVien : Nguoi
    {
        public int MaGiangVien { get; set; }

        public GiangVien(int ma, string hoTen, string email)
            : base(hoTen, email)
        {
            MaGiangVien = ma;
        }

        public void NhapDiem(DangKyHoc dangKy, float diem)
        {
            dangKy.Diem = diem;
            dangKy.TinhKetQua();
        }
    }

    // ================= MÔN HỌC =================
    public class MonHoc
    {
        public int MaMonHoc { get; set; }
        public string TenMon { get; set; }
        public int SoTinChi { get; set; }

        public MonHoc(int ma, string ten, int soTinChi)
        {
            MaMonHoc = ma;
            TenMon = ten;
            SoTinChi = soTinChi;
        }
    }

    // ================= ĐĂNG KÝ HỌC =================
    public class DangKyHoc
    {
        public SinhVien SinhVien { get; set; }
        public MonHoc Mon { get; set; }
        public float Diem { get; set; }
        public string KetQua { get; set; }

        public DangKyHoc(SinhVien sv, MonHoc mon)
        {
            SinhVien = sv;
            Mon = mon;
            Diem = 0;
            KetQua = "Chua co diem";
        }

        public void TinhKetQua()
        {
            if (Diem >= 5)
                KetQua = "Dat";
            else
                KetQua = "Rot";
        }
    }

    // ================= Main =================
    class Program
    {
        static void Main(string[] args)
        {
            // Tạo sinh viên
            SinhVien sv1 = new SinhVien(1, "Nguyen Van A", "a@gmail.com", "CNTT1");

            // Tạo môn học
            MonHoc mon1 = new MonHoc(101, "Lap Trinh C#", 3);

            // Tạo giảng viên
            GiangVien gv1 = new GiangVien(10, "Tran Van B", "b@gmail.com");

            // Sinh viên đăng ký môn
            sv1.DangKyMonHoc(mon1);

            // Giảng viên nhập điểm
            gv1.NhapDiem(sv1.DanhSachDangKy[0], 8.5f);

            // Hiển thị kết quả
            sv1.HienThiThongTin();
            Console.WriteLine("----- Ket Qua Hoc Tap -----");
            sv1.XemKetQua();

            Console.ReadLine();
        }
    }
}