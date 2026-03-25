using System;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;

namespace StudentManagementSystem
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            KhoaService khoaService = new KhoaService();
            LopHocService lopHocService = new LopHocService();
            HocKyService hocKyService = new HocKyService();
            MonHocService monHocService = new MonHocService();
            SinhVienService sinhVienService = new SinhVienService();
            GiangVienService giangVienService = new GiangVienService();
            DangKyHocService dangKyHocService = new DangKyHocService();

            KhoaCongNgheThongTin khoaCntt = new KhoaCongNgheThongTin("CNTT", "Khoa Cong nghe thong tin");
            khoaService.Them(khoaCntt);

            HocKy hocKy2026 = new HocKy("HK2026", "Hoc ky 1", 2026);
            LopHoc lopKTPM = new LopHoc("CTK46", "Cong nghe phan mem 46");

            hocKyService.Them(hocKy2026);
            lopHocService.Them(lopKTPM);
            khoaService.GanLopVaoKhoa(khoaCntt.MaKhoa, lopKTPM);

            MonHoc lapTrinh = new MonHoc("CS101", "Lap trinh huong doi tuong", 3);
            MonHoc coSoDuLieu = new MonHoc("CS202", "Co so du lieu", 3);
            monHocService.Them(lapTrinh);
            monHocService.Them(coSoDuLieu);

            SinhVien sinhVien = new SinhVien("SV001", "Nguyen Van A", "a@student.edu", new DateTime(2005, 1, 10));
            GiangVien giangVien = new GiangVien("GV001", "Tran Thi B", "b@teacher.edu");
            sinhVienService.Them(sinhVien);
            giangVienService.Them(giangVien);
            lopKTPM.ThemSinhVien(sinhVien);

            DangKyHoc dangKyThuong = dangKyHocService.DangKyMonHoc(sinhVien, lapTrinh, hocKy2026);
            DangKyHoc dangKyClc = dangKyHocService.DangKyMonHocHeChatLuongCao(sinhVien, coSoDuLieu, hocKy2026);

            giangVien.NhapDiem(dangKyThuong, 5.5f);
            giangVien.NhapDiem(dangKyClc, 5.5f);

            InTieuDe("THONG TIN CO BAN");
            Console.WriteLine(sinhVien.HienThiThongTin());
            Console.WriteLine(giangVien.HienThiThongTin());
            Console.WriteLine(giangVien.QuanLyMon(lapTrinh));

            InTieuDe("THE HIEN 4 TINH CHAT OOP");
            Console.WriteLine("1. Abstraction: Nguoi la lop truu tuong, dung qua doi tuong con.");
            Nguoi nguoiDung = sinhVien;
            Console.WriteLine("   Vai tro dong: " + nguoiDung.LayVaiTro());

            Console.WriteLine("2. Inheritance: SinhVien/GiangVien ke thua Nguoi.");
            Console.WriteLine("   SinhVien: " + sinhVien.HoTen + " | GiangVien: " + giangVien.HoTen);

            Console.WriteLine("3. Encapsulation: Du lieu dong goi qua property + method cap nhat co kiem tra.");
            sinhVien.CapNhatThongTinCoBan("Nguyen Van A Updated", "a.updated@student.edu");
            Console.WriteLine("   Sau cap nhat: " + sinhVien.HienThiThongTin());

            Console.WriteLine("4. Polymorphism: 2 doi tuong DangKyHoc cung goi TinhKetQua nhung ket qua khac nhau.");
            Console.WriteLine("   Dang ky thuong: " + dangKyThuong.LayThongTinDangKy());
            Console.WriteLine("   Dang ky CLC   : " + dangKyClc.LayThongTinDangKy());

            InTieuDe("KET QUA HOC TAP");
            Console.WriteLine(sinhVien.XemKetQuaHocTap());

            InTieuDe("MO TA KHOA (DA HINH OVERRIDE)");
            Console.WriteLine(khoaCntt.LayMoTaKhoa());

            InTieuDe("DANH SACH SINH VIEN CUNG LOP");
            int index = 0;
            IReadOnlyList<SinhVien> dsSinhVien = sinhVienService.LayTatCa();

            while (index < dsSinhVien.Count)
            {
                SinhVien item = dsSinhVien[index];
                Console.WriteLine("- " + item.MaSinhVien + " | " + item.HoTen + " | Lop: " + (item.LopHoc == null ? "-" : item.LopHoc.MaLop));
                index = index + 1;
            }
        }

        private static void InTieuDe(string tieuDe)
        {
            Console.WriteLine();
            Console.WriteLine("================ " + tieuDe + " ================");
        }
    }
}
