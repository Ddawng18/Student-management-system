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
            var khoaRepo = new KhoaRepository();
            var lopHocRepo = new LopHocRepository();
            var giangVienRepo = new GiangVienRepository();
            var hocKyRepo = new HocKyRepository();
            var sinhVienRepo = new SinhVienRepository();
            var monHocRepo = new MonHocRepository();
            var dangKyHocRepo = new DangKyHocRepository();

            // Khởi tạo services
            var khoaService = new KhoaService(khoaRepo);
            var lopHocService = new LopHocService(lopHocRepo);
            var giangVienService = new GiangVienService(giangVienRepo);
            var hocKyService = new HocKyService(hocKyRepo);
            var sinhVienService = new SinhVienService(sinhVienRepo);
            var monHocService = new MonHocService(monHocRepo);
            var dangKyHocService = new DangKyHocService(dangKyHocRepo, sinhVienRepo, monHocRepo);

            // Dữ liệu mẫu
            khoaService.ThemKhoa("Khoa Tin Hoc");
            lopHocService.ThemLopHoc("12CT1", 1);
            giangVienService.ThemGiangVien("Tran Van B", "b@gmail.com", 1);
            hocKyService.ThemHocKy("HK1", 2024);
            sinhVienService.ThemSinhVien("Nguyen Van A", "a@gmail.com", new DateTime(2000, 1, 1), "123 Main St", 1, 1);
            monHocService.ThemMonHoc("Lap Trinh C#", "Hoc co ban C#", 3, 1, 1);
            dangKyHocService.DangKyMonHoc(1, 1, 1);

            // Menu chính
            while (true)
            {
                Console.Clear();
                Console.WriteLine("He Thong Quan Ly Sinh Vien");
                Console.WriteLine("1. Quan Ly Khoa");
                Console.WriteLine("2. Quan Ly Lop Hoc");
                Console.WriteLine("3. Quan Ly Giang Vien");
                Console.WriteLine("4. Quan Ly Hoc Ky");
                Console.WriteLine("5. Quan Ly Sinh Vien");
                Console.WriteLine("6. Quan Ly Mon Hoc");
                Console.WriteLine("7. Quan Ly Dang Ky Hoc");
                Console.WriteLine("8. Thoat");
                Console.Write("Chon mot tuy chon: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        QuanLyKhoa(khoaService);
                        break;
                    case "2":
                        QuanLyLopHoc(lopHocService);
                        break;
                    case "3":
                        QuanLyGiangVien(giangVienService);
                        break;
                    case "4":
                        QuanLyHocKy(hocKyService);
                        break;
                    case "5":
                        QuanLySinhVien(sinhVienService);
                        break;
                    case "6":
                        QuanLyMonHoc(monHocService);
                        break;
                    case "7":
                        QuanLyDangKyHoc(dangKyHocService);
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Tuy chon khong hop le. Nhan bat ky phim nao de tiep tuc.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void QuanLyKhoa(KhoaService service)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quan Ly Khoa");
                Console.WriteLine("1. Them Khoa");
                Console.WriteLine("2. Xem Tat Ca Khoa");
                Console.WriteLine("3. Cap Nhat Khoa");
                Console.WriteLine("4. Xoa Khoa");
                Console.WriteLine("5. Quay Lai");
                Console.Write("Chon mot tuy chon: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ThemKhoa(service);
                        break;
                    case "2":
                        XemKhoa(service);
                        break;
                    case "3":
                        CapNhatKhoa(service);
                        break;
                    case "4":
                        XoaKhoa(service);
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

        static void ThemKhoa(KhoaService service)
        {
            Console.Write("Ten khoa: ");
            var tenKhoa = Console.ReadLine();
            service.ThemKhoa(tenKhoa);
            Console.WriteLine("Khoa da duoc them. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void XemKhoa(KhoaService service)
        {
            var khoas = service.LayTatCaKhoa();
            foreach (var khoa in khoas)
            {
                Console.WriteLine(khoa);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void CapNhatKhoa(KhoaService service)
        {
            Console.Write("Ma khoa: ");
            var id = int.Parse(Console.ReadLine());
            var khoa = service.LayKhoaTheoId(id);
            if (khoa == null)
            {
                Console.WriteLine("Khoa khong ton tai.");
                Console.ReadKey();
                return;
            }

            Console.Write($"Ten khoa ({khoa.TenKhoa}): ");
            var tenKhoa = Console.ReadLine();
            if (string.IsNullOrEmpty(tenKhoa)) tenKhoa = khoa.TenKhoa;

            service.CapNhatKhoa(id, tenKhoa);
            Console.WriteLine("Khoa da duoc cap nhat. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void XoaKhoa(KhoaService service)
        {
            Console.Write("Ma khoa: ");
            var id = int.Parse(Console.ReadLine());
            service.XoaKhoa(id);
            Console.WriteLine("Khoa da duoc xoa. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void QuanLyLopHoc(LopHocService service)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quan Ly Lop Hoc");
                Console.WriteLine("1. Them Lop Hoc");
                Console.WriteLine("2. Xem Tat Ca Lop Hoc");
                Console.WriteLine("3. Cap Nhat Lop Hoc");
                Console.WriteLine("4. Xoa Lop Hoc");
                Console.WriteLine("5. Quay Lai");
                Console.Write("Chon mot tuy chon: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ThemLopHoc(service);
                        break;
                    case "2":
                        XemLopHoc(service);
                        break;
                    case "3":
                        CapNhatLopHoc(service);
                        break;
                    case "4":
                        XoaLopHoc(service);
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

        static void ThemLopHoc(LopHocService service)
        {
            Console.Write("Ten lop: ");
            var tenLop = Console.ReadLine();
            Console.Write("Ma khoa: ");
            var maKhoa = int.Parse(Console.ReadLine());
            service.ThemLopHoc(tenLop, maKhoa);
            Console.WriteLine("Lop hoc da duoc them. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void XemLopHoc(LopHocService service)
        {
            var lopHocs = service.LayTatCaLopHoc();
            foreach (var lopHoc in lopHocs)
            {
                Console.WriteLine(lopHoc);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void CapNhatLopHoc(LopHocService service)
        {
            Console.Write("Ma lop: ");
            var id = int.Parse(Console.ReadLine());
            var lopHoc = service.LayLopHocTheoId(id);
            if (lopHoc == null)
            {
                Console.WriteLine("Lop hoc khong ton tai.");
                Console.ReadKey();
                return;
            }

            Console.Write($"Ten lop ({lopHoc.TenLop}): ");
            var tenLop = Console.ReadLine();
            if (string.IsNullOrEmpty(tenLop)) tenLop = lopHoc.TenLop;
            Console.Write($"Ma khoa ({lopHoc.MaKhoa}): ");
            var maKhoaStr = Console.ReadLine();
            var maKhoa = string.IsNullOrEmpty(maKhoaStr) ? lopHoc.MaKhoa : int.Parse(maKhoaStr);

            service.CapNhatLopHoc(id, tenLop, maKhoa);
            Console.WriteLine("Lop hoc da duoc cap nhat. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void XoaLopHoc(LopHocService service)
        {
            Console.Write("Ma lop: ");
            var id = int.Parse(Console.ReadLine());
            service.XoaLopHoc(id);
            Console.WriteLine("Lop hoc da duoc xoa. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void QuanLyGiangVien(GiangVienService service)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quan Ly Giang Vien");
                Console.WriteLine("1. Them Giang Vien");
                Console.WriteLine("2. Xem Tat Ca Giang Vien");
                Console.WriteLine("3. Cap Nhat Giang Vien");
                Console.WriteLine("4. Xoa Giang Vien");
                Console.WriteLine("5. Quay Lai");
                Console.Write("Chon mot tuy chon: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ThemGiangVien(service);
                        break;
                    case "2":
                        XemGiangVien(service);
                        break;
                    case "3":
                        CapNhatGiangVien(service);
                        break;
                    case "4":
                        XoaGiangVien(service);
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

        static void ThemGiangVien(GiangVienService service)
        {
            Console.Write("Ho ten: ");
            var hoTen = Console.ReadLine();
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Ma khoa: ");
            var maKhoa = int.Parse(Console.ReadLine());
            service.ThemGiangVien(hoTen, email, maKhoa);
            Console.WriteLine("Giang vien da duoc them. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void XemGiangVien(GiangVienService service)
        {
            var giangViens = service.LayTatCaGiangVien();
            foreach (var giangVien in giangViens)
            {
                Console.WriteLine(giangVien);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void CapNhatGiangVien(GiangVienService service)
        {
            Console.Write("Ma giang vien: ");
            var id = int.Parse(Console.ReadLine());
            var giangVien = service.LayGiangVienTheoId(id);
            if (giangVien == null)
            {
                Console.WriteLine("Giang vien khong ton tai.");
                Console.ReadKey();
                return;
            }

            Console.Write($"Ho ten ({giangVien.HoTen}): ");
            var hoTen = Console.ReadLine();
            if (string.IsNullOrEmpty(hoTen)) hoTen = giangVien.HoTen;
            Console.Write($"Email ({giangVien.Email}): ");
            var email = Console.ReadLine();
            if (string.IsNullOrEmpty(email)) email = giangVien.Email;
            Console.Write($"Ma khoa ({giangVien.MaKhoa}): ");
            var maKhoaStr = Console.ReadLine();
            var maKhoa = string.IsNullOrEmpty(maKhoaStr) ? giangVien.MaKhoa : int.Parse(maKhoaStr);

            service.CapNhatGiangVien(id, hoTen, email, maKhoa);
            Console.WriteLine("Giang vien da duoc cap nhat. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void XoaGiangVien(GiangVienService service)
        {
            Console.Write("Ma giang vien: ");
            var id = int.Parse(Console.ReadLine());
            service.XoaGiangVien(id);
            Console.WriteLine("Giang vien da duoc xoa. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void QuanLyHocKy(HocKyService service)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quan Ly Hoc Ky");
                Console.WriteLine("1. Them Hoc Ky");
                Console.WriteLine("2. Xem Tat Ca Hoc Ky");
                Console.WriteLine("3. Cap Nhat Hoc Ky");
                Console.WriteLine("4. Xoa Hoc Ky");
                Console.WriteLine("5. Quay Lai");
                Console.Write("Chon mot tuy chon: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ThemHocKy(service);
                        break;
                    case "2":
                        XemHocKy(service);
                        break;
                    case "3":
                        CapNhatHocKy(service);
                        break;
                    case "4":
                        XoaHocKy(service);
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

        static void ThemHocKy(HocKyService service)
        {
            Console.Write("Ten hoc ky: ");
            var tenHocKy = Console.ReadLine();
            Console.Write("Nam hoc: ");
            var namHoc = int.Parse(Console.ReadLine());
            service.ThemHocKy(tenHocKy, namHoc);
            Console.WriteLine("Hoc ky da duoc them. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void XemHocKy(HocKyService service)
        {
            var hocKys = service.LayTatCaHocKy();
            foreach (var hocKy in hocKys)
            {
                Console.WriteLine(hocKy);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void CapNhatHocKy(HocKyService service)
        {
            Console.Write("Ma hoc ky: ");
            var id = int.Parse(Console.ReadLine());
            var hocKy = service.LayHocKyTheoId(id);
            if (hocKy == null)
            {
                Console.WriteLine("Hoc ky khong ton tai.");
                Console.ReadKey();
                return;
            }

            Console.Write($"Ten hoc ky ({hocKy.TenHocKy}): ");
            var tenHocKy = Console.ReadLine();
            if (string.IsNullOrEmpty(tenHocKy)) tenHocKy = hocKy.TenHocKy;
            Console.Write($"Nam hoc ({hocKy.NamHoc}): ");
            var namHocStr = Console.ReadLine();
            var namHoc = string.IsNullOrEmpty(namHocStr) ? hocKy.NamHoc : int.Parse(namHocStr);

            service.CapNhatHocKy(id, tenHocKy, namHoc);
            Console.WriteLine("Hoc ky da duoc cap nhat. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        static void XoaHocKy(HocKyService service)
        {
            Console.Write("Ma hoc ky: ");
            var id = int.Parse(Console.ReadLine());
            service.XoaHocKy(id);
            Console.WriteLine("Hoc ky da duoc xoa. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
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
            Console.Write("Ma khoa: ");
            var maKhoa = int.Parse(Console.ReadLine());
            Console.Write("Ma lop: ");
            var maLop = int.Parse(Console.ReadLine());

            service.ThemSinhVien(hoTen, email, ngaySinh, diaChi, maKhoa, maLop);
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
            Console.Write($"Ma khoa ({sinhVien.MaKhoa}): ");
            var maKhoaStr = Console.ReadLine();
            var maKhoa = string.IsNullOrEmpty(maKhoaStr) ? sinhVien.MaKhoa : int.Parse(maKhoaStr);
            Console.Write($"Ma lop ({sinhVien.MaLop}): ");
            var maLopStr = Console.ReadLine();
            var maLop = string.IsNullOrEmpty(maLopStr) ? sinhVien.MaLop : int.Parse(maLopStr);

            service.CapNhatSinhVien(id, hoTen, email, ngaySinh, diaChi, maKhoa, maLop);
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
            Console.Write("Ma khoa: ");
            var maKhoa = int.Parse(Console.ReadLine());
            Console.Write("Ma giang vien: ");
            var maGiangVien = int.Parse(Console.ReadLine());

            service.ThemMonHoc(tenMon, moTa, soTinChi, maKhoa, maGiangVien);
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
            Console.Write($"Ma khoa ({monHoc.MaKhoa}): ");
            var maKhoaStr = Console.ReadLine();
            var maKhoa = string.IsNullOrEmpty(maKhoaStr) ? monHoc.MaKhoa : int.Parse(maKhoaStr);
            Console.Write($"Ma giang vien ({monHoc.MaGiangVien}): ");
            var maGiangVienStr = Console.ReadLine();
            var maGiangVien = string.IsNullOrEmpty(maGiangVienStr) ? monHoc.MaGiangVien : int.Parse(maGiangVienStr);

            service.CapNhatMonHoc(id, tenMon, moTa, soTinChi, maKhoa, maGiangVien);
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
            Console.Write("Ma hoc ky: ");
            var maHocKy = int.Parse(Console.ReadLine());

            try
            {
                service.DangKyMonHoc(maSinhVien, maMonHoc, maHocKy);
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
