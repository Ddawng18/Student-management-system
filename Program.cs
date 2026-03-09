using System;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;
using StudentManagementSystem.Services;

namespace StudentManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Khởi tạo repositories
            var sinhVienRepo = new SinhVienRepository();
            var monHocRepo = new MonHocRepository();
            var dangKyHocRepo = new DangKyHocRepository();

            // Khởi tạo services
            var sinhVienService = new SinhVienService(sinhVienRepo);
            var monHocService = new MonHocService(monHocRepo);
            var dangKyHocService = new DangKyHocService(dangKyHocRepo, sinhVienRepo, monHocRepo);

            // Dữ liệu mẫu
            sinhVienService.ThemSinhVien("Nguyen Van A", "a@gmail.com", new DateTime(2000, 1, 1), "123 Main St");
            monHocService.ThemMonHoc("Lap Trinh C#", "Hoc co ban C#", 3);
            dangKyHocService.DangKyMonHoc(1, 1);

            // Menu chính
            while (true)
            {
                Console.Clear();
                Console.WriteLine("He Thong Quan Ly Sinh Vien");
                Console.WriteLine("1. Quan Ly Sinh Vien");
                Console.WriteLine("2. Quan Ly Mon Hoc");
                Console.WriteLine("3. Quan Ly Dang Ky Hoc");
                Console.WriteLine("4. Thoat");
                Console.Write("Chon mot tuy chon: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        QuanLySinhVien(sinhVienService);
                        break;
                    case "2":
                        QuanLyMonHoc(monHocService);
                        break;
                    case "3":
                        QuanLyDangKyHoc(dangKyHocService);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Tuy chon khong hop le. Nhan bat ky phim nao de tiep tuc.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void QuanLySinhVien(SinhVienService service)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quan Ly Sinh Vien");
                Console.WriteLine("1. Them Sinh Vien");
                Console.WriteLine("2. Xem Tat Ca Sinh Vien");
                Console.WriteLine("3. Cap Nhat Sinh Vien");
                Console.WriteLine("4. Xoa Sinh Vien");
                Console.WriteLine("5. Quay Lai");
                Console.Write("Chon mot tuy chon: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ThemSinhVien(service);
                        break;
                    case "2":
                        XemSinhVien(service);
                        break;
                    case "3":
                        CapNhatSinhVien(service);
                        break;
                    case "4":
                        XoaSinhVien(service);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Tuy chon khong hop le. Nhan bat ky phim nao de tiep tuc.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ThemSinhVien(SinhVienService service)
        {
            Console.Write("Ho ten: ");
            var hoTen = Console.ReadLine();
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Ngay sinh (yyyy-mm-dd): ");
            var ngaySinh = DateTime.Parse(Console.ReadLine());
            Console.Write("Dia chi: ");
            var diaChi = Console.ReadLine();

            service.ThemSinhVien(hoTen, email, ngaySinh, diaChi);
            Console.WriteLine("Sinh vien da duoc them. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void XemSinhVien(SinhVienService service)
        {
            var sinhViens = service.LayTatCaSinhVien();
            foreach (var sinhVien in sinhViens)
            {
                Console.WriteLine(sinhVien);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void CapNhatSinhVien(SinhVienService service)
        {
            Console.Write("Ma sinh vien: ");
            var id = int.Parse(Console.ReadLine());
            var sinhVien = service.LaySinhVienTheoId(id);
            if (sinhVien == null)
            {
                Console.WriteLine("Sinh vien khong ton tai.");
                Console.ReadKey();
                return;
            }

            Console.Write($"Ho ten ({sinhVien.HoTen}): ");
            var hoTen = Console.ReadLine();
            if (string.IsNullOrEmpty(hoTen)) hoTen = sinhVien.HoTen;
            Console.Write($"Email ({sinhVien.Email}): ");
            var email = Console.ReadLine();
            if (string.IsNullOrEmpty(email)) email = sinhVien.Email;
            Console.Write($"Ngay sinh ({sinhVien.NgaySinh:yyyy-MM-dd}): ");
            var ngaySinhStr = Console.ReadLine();
            var ngaySinh = string.IsNullOrEmpty(ngaySinhStr) ? sinhVien.NgaySinh : DateTime.Parse(ngaySinhStr);
            Console.Write($"Dia chi ({sinhVien.DiaChi}): ");
            var diaChi = Console.ReadLine();
            if (string.IsNullOrEmpty(diaChi)) diaChi = sinhVien.DiaChi;

            service.CapNhatSinhVien(id, hoTen, email, ngaySinh, diaChi);
            Console.WriteLine("Sinh vien da duoc cap nhat. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void XoaSinhVien(SinhVienService service)
        {
            Console.Write("Ma sinh vien: ");
            var id = int.Parse(Console.ReadLine());
            service.XoaSinhVien(id);
            Console.WriteLine("Sinh vien da duoc xoa. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void QuanLyMonHoc(MonHocService service)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quan Ly Mon Hoc");
                Console.WriteLine("1. Them Mon Hoc");
                Console.WriteLine("2. Xem Tat Ca Mon Hoc");
                Console.WriteLine("3. Cap Nhat Mon Hoc");
                Console.WriteLine("4. Xoa Mon Hoc");
                Console.WriteLine("5. Quay Lai");
                Console.Write("Chon mot tuy chon: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ThemMonHoc(service);
                        break;
                    case "2":
                        XemMonHoc(service);
                        break;
                    case "3":
                        CapNhatMonHoc(service);
                        break;
                    case "4":
                        XoaMonHoc(service);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Tuy chon khong hop le. Nhan bat ky phim nao de tiep tuc.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ThemMonHoc(MonHocService service)
        {
            Console.Write("Ten mon: ");
            var tenMon = Console.ReadLine();
            Console.Write("Mo ta: ");
            var moTa = Console.ReadLine();
            Console.Write("So tin chi: ");
            var soTinChi = int.Parse(Console.ReadLine());

            service.ThemMonHoc(tenMon, moTa, soTinChi);
            Console.WriteLine("Mon hoc da duoc them. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void XemMonHoc(MonHocService service)
        {
            var monHocs = service.LayTatCaMonHoc();
            foreach (var monHoc in monHocs)
            {
                Console.WriteLine(monHoc);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void CapNhatMonHoc(MonHocService service)
        {
            Console.Write("Ma mon hoc: ");
            var id = int.Parse(Console.ReadLine());
            var monHoc = service.LayMonHocTheoId(id);
            if (monHoc == null)
            {
                Console.WriteLine("Mon hoc khong ton tai.");
                Console.ReadKey();
                return;
            }

            Console.Write($"Ten mon ({monHoc.TenMon}): ");
            var tenMon = Console.ReadLine();
            if (string.IsNullOrEmpty(tenMon)) tenMon = monHoc.TenMon;
            Console.Write($"Mo ta ({monHoc.MoTa}): ");
            var moTa = Console.ReadLine();
            if (string.IsNullOrEmpty(moTa)) moTa = monHoc.MoTa;
            Console.Write($"So tin chi ({monHoc.SoTinChi}): ");
            var soTinChiStr = Console.ReadLine();
            var soTinChi = string.IsNullOrEmpty(soTinChiStr) ? monHoc.SoTinChi : int.Parse(soTinChiStr);

            service.CapNhatMonHoc(id, tenMon, moTa, soTinChi);
            Console.WriteLine("Mon hoc da duoc cap nhat. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void XoaMonHoc(MonHocService service)
        {
            Console.Write("Ma mon hoc: ");
            var id = int.Parse(Console.ReadLine());
            service.XoaMonHoc(id);
            Console.WriteLine("Mon hoc da duoc xoa. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void QuanLyDangKyHoc(DangKyHocService service)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quan Ly Dang Ky Hoc");
                Console.WriteLine("1. Dang Ky Mon Hoc Cho Sinh Vien");
                Console.WriteLine("2. Xem Tat Ca Dang Ky Hoc");
                Console.WriteLine("3. Xem Dang Ky Hoc Theo Sinh Vien");
                Console.WriteLine("4. Huy Dang Ky Hoc");
                Console.WriteLine("5. Quay Lai");
                Console.Write("Chon mot tuy chon: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        DangKyMonHoc(service);
                        break;
                    case "2":
                        XemDangKyHoc(service);
                        break;
                    case "3":
                        XemDangKyHocTheoSinhVien(service);
                        break;
                    case "4":
                        HuyDangKyHoc(service);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Tuy chon khong hop le. Nhan bat ky phim nao de tiep tuc.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void DangKyMonHoc(DangKyHocService service)
        {
            Console.Write("Ma sinh vien: ");
            var maSinhVien = int.Parse(Console.ReadLine());
            Console.Write("Ma mon hoc: ");
            var maMonHoc = int.Parse(Console.ReadLine());

            try
            {
                service.DangKyMonHoc(maSinhVien, maMonHoc);
                Console.WriteLine("Sinh vien da dang ky mon hoc. Nhan bat ky phim nao de tiep tuc.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        static void XemDangKyHoc(DangKyHocService service)
        {
            var dangKyHocs = service.LayTatCaDangKyHoc();
            foreach (var dangKyHoc in dangKyHocs)
            {
                Console.WriteLine(dangKyHoc);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void XemDangKyHocTheoSinhVien(DangKyHocService service)
        {
            Console.Write("Ma sinh vien: ");
            var maSinhVien = int.Parse(Console.ReadLine());
            var dangKyHocs = service.LayDangKyHocTheoSinhVien(maSinhVien);
            foreach (var dangKyHoc in dangKyHocs)
            {
                Console.WriteLine(dangKyHoc);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void HuyDangKyHoc(DangKyHocService service)
        {
            Console.Write("Ma dang ky: ");
            var id = int.Parse(Console.ReadLine());
            service.HuyDangKyHoc(id);
            Console.WriteLine("Da huy dang ky. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }
    }
}
