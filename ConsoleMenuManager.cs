using System;
using StudentManagementSystem.Services;

namespace StudentManagementSystem
{
    public class ConsoleMenuManager
    {
        private readonly KhoaService _khoaService;
        private readonly LopHocService _lopHocService;
        private readonly GiangVienService _giangVienService;
        private readonly HocKyService _hocKyService;
        private readonly SinhVienService _sinhVienService;
        private readonly MonHocService _monHocService;
        private readonly DangKyHocService _dangKyHocService;

        public ConsoleMenuManager(
            KhoaService khoaService,
            LopHocService lopHocService,
            GiangVienService giangVienService,
            HocKyService hocKyService,
            SinhVienService sinhVienService,
            MonHocService monHocService,
            DangKyHocService dangKyHocService)
        {
            _khoaService = khoaService;
            _lopHocService = lopHocService;
            _giangVienService = giangVienService;
            _hocKyService = hocKyService;
            _sinhVienService = sinhVienService;
            _monHocService = monHocService;
            _dangKyHocService = dangKyHocService;
        }

        public void ShowMainMenu()
        {
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
                        QuanLyKhoa();
                        break;
                    case "2":
                        QuanLyLopHoc();
                        break;
                    case "3":
                        QuanLyGiangVien();
                        break;
                    case "4":
                        QuanLyHocKy();
                        break;
                    case "5":
                        QuanLySinhVien();
                        break;
                    case "6":
                        QuanLyMonHoc();
                        break;
                    case "7":
                        QuanLyDangKyHoc();
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

        private void QuanLyKhoa()
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
                        ThemKhoa();
                        break;
                    case "2":
                        XemKhoa();
                        break;
                    case "3":
                        CapNhatKhoa();
                        break;
                    case "4":
                        XoaKhoa();
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

        private void ThemKhoa()
        {
            Console.Write("Ten khoa: ");
            var tenKhoa = Console.ReadLine();
            _khoaService.ThemKhoa(tenKhoa);
            Console.WriteLine("Khoa da duoc them. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void XemKhoa()
        {
            var khoas = _khoaService.LayTatCaKhoa();
            foreach (var khoa in khoas)
            {
                Console.WriteLine(khoa);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void CapNhatKhoa()
        {
            Console.Write("Ma khoa: ");
            var id = int.Parse(Console.ReadLine());
            var khoa = _khoaService.LayKhoaTheoId(id);
            if (khoa == null)
            {
                Console.WriteLine("Khoa khong ton tai.");
                Console.ReadKey();
                return;
            }

            Console.Write($"Ten khoa ({khoa.TenKhoa}): ");
            var tenKhoa = Console.ReadLine();
            if (string.IsNullOrEmpty(tenKhoa)) tenKhoa = khoa.TenKhoa;

            _khoaService.CapNhatKhoa(id, tenKhoa);
            Console.WriteLine("Khoa da duoc cap nhat. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void XoaKhoa()
        {
            Console.Write("Ma khoa: ");
            var id = int.Parse(Console.ReadLine());
            _khoaService.XoaKhoa(id);
            Console.WriteLine("Khoa da duoc xoa. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void QuanLyLopHoc()
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
                        ThemLopHoc();
                        break;
                    case "2":
                        XemLopHoc();
                        break;
                    case "3":
                        CapNhatLopHoc();
                        break;
                    case "4":
                        XoaLopHoc();
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

        private void ThemLopHoc()
        {
            Console.Write("Ten lop: ");
            var tenLop = Console.ReadLine();
            Console.Write("Ma khoa: ");
            var maKhoa = int.Parse(Console.ReadLine());
            _lopHocService.ThemLopHoc(tenLop, maKhoa);
            Console.WriteLine("Lop hoc da duoc them. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void XemLopHoc()
        {
            var lopHocs = _lopHocService.LayTatCaLopHoc();
            foreach (var lopHoc in lopHocs)
            {
                Console.WriteLine(lopHoc);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void CapNhatLopHoc()
        {
            Console.Write("Ma lop: ");
            var id = int.Parse(Console.ReadLine());
            var lopHoc = _lopHocService.LayLopHocTheoId(id);
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

            _lopHocService.CapNhatLopHoc(id, tenLop, maKhoa);
            Console.WriteLine("Lop hoc da duoc cap nhat. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void XoaLopHoc()
        {
            Console.Write("Ma lop: ");
            var id = int.Parse(Console.ReadLine());
            _lopHocService.XoaLopHoc(id);
            Console.WriteLine("Lop hoc da duoc xoa. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void QuanLyGiangVien()
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
                        ThemGiangVien();
                        break;
                    case "2":
                        XemGiangVien();
                        break;
                    case "3":
                        CapNhatGiangVien();
                        break;
                    case "4":
                        XoaGiangVien();
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

        private void ThemGiangVien()
        {
            Console.Write("Ho ten: ");
            var hoTen = Console.ReadLine();
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Ma khoa: ");
            var maKhoa = int.Parse(Console.ReadLine());
            _giangVienService.ThemGiangVien(hoTen, email, maKhoa);
            Console.WriteLine("Giang vien da duoc them. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void XemGiangVien()
        {
            var giangViens = _giangVienService.LayTatCaGiangVien();
            foreach (var giangVien in giangViens)
            {
                Console.WriteLine(giangVien);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void CapNhatGiangVien()
        {
            Console.Write("Ma giang vien: ");
            var id = int.Parse(Console.ReadLine());
            var giangVien = _giangVienService.LayGiangVienTheoId(id);
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

            _giangVienService.CapNhatGiangVien(id, hoTen, email, maKhoa);
            Console.WriteLine("Giang vien da duoc cap nhat. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void XoaGiangVien()
        {
            Console.Write("Ma giang vien: ");
            var id = int.Parse(Console.ReadLine());
            _giangVienService.XoaGiangVien(id);
            Console.WriteLine("Giang vien da duoc xoa. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void QuanLyHocKy()
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
                        ThemHocKy();
                        break;
                    case "2":
                        XemHocKy();
                        break;
                    case "3":
                        CapNhatHocKy();
                        break;
                    case "4":
                        XoaHocKy();
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

        private void ThemHocKy()
        {
            Console.Write("Ten hoc ky: ");
            var tenHocKy = Console.ReadLine();
            Console.Write("Nam hoc: ");
            var namHoc = int.Parse(Console.ReadLine());
            _hocKyService.ThemHocKy(tenHocKy, namHoc);
            Console.WriteLine("Hoc ky da duoc them. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void XemHocKy()
        {
            var hocKys = _hocKyService.LayTatCaHocKy();
            foreach (var hocKy in hocKys)
            {
                Console.WriteLine(hocKy);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void CapNhatHocKy()
        {
            Console.Write("Ma hoc ky: ");
            var id = int.Parse(Console.ReadLine());
            var hocKy = _hocKyService.LayHocKyTheoId(id);
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

            _hocKyService.CapNhatHocKy(id, tenHocKy, namHoc);
            Console.WriteLine("Hoc ky da duoc cap nhat. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void XoaHocKy()
        {
            Console.Write("Ma hoc ky: ");
            var id = int.Parse(Console.ReadLine());
            _hocKyService.XoaHocKy(id);
            Console.WriteLine("Hoc ky da duoc xoa. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void QuanLySinhVien()
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
                        ThemSinhVien();
                        break;
                    case "2":
                        XemSinhVien();
                        break;
                    case "3":
                        CapNhatSinhVien();
                        break;
                    case "4":
                        XoaSinhVien();
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

        private void ThemSinhVien()
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

            _sinhVienService.ThemSinhVien(hoTen, email, ngaySinh, diaChi, maKhoa, maLop);
            Console.WriteLine("Sinh vien da duoc them. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void XemSinhVien()
        {
            var sinhViens = _sinhVienService.LayTatCaSinhVien();
            foreach (var sinhVien in sinhViens)
            {
                Console.WriteLine(sinhVien);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void CapNhatSinhVien()
        {
            Console.Write("Ma sinh vien: ");
            var id = int.Parse(Console.ReadLine());
            var sinhVien = _sinhVienService.LaySinhVienTheoId(id);
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

            _sinhVienService.CapNhatSinhVien(id, hoTen, email, ngaySinh, diaChi, maKhoa, maLop);
            Console.WriteLine("Sinh vien da duoc cap nhat. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void XoaSinhVien()
        {
            Console.Write("Ma sinh vien: ");
            var id = int.Parse(Console.ReadLine());
            _sinhVienService.XoaSinhVien(id);
            Console.WriteLine("Sinh vien da duoc xoa. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void QuanLyMonHoc()
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
                        ThemMonHoc();
                        break;
                    case "2":
                        XemMonHoc();
                        break;
                    case "3":
                        CapNhatMonHoc();
                        break;
                    case "4":
                        XoaMonHoc();
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

        private void ThemMonHoc()
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

            _monHocService.ThemMonHoc(tenMon, moTa, soTinChi, maKhoa, maGiangVien);
            Console.WriteLine("Mon hoc da duoc them. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void XemMonHoc()
        {
            var monHocs = _monHocService.LayTatCaMonHoc();
            foreach (var monHoc in monHocs)
            {
                Console.WriteLine(monHoc);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void CapNhatMonHoc()
        {
            Console.Write("Ma mon hoc: ");
            var id = int.Parse(Console.ReadLine());
            var monHoc = _monHocService.LayMonHocTheoId(id);
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

            _monHocService.CapNhatMonHoc(id, tenMon, moTa, soTinChi, maKhoa, maGiangVien);
            Console.WriteLine("Mon hoc da duoc cap nhat. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void XoaMonHoc()
        {
            Console.Write("Ma mon hoc: ");
            var id = int.Parse(Console.ReadLine());
            _monHocService.XoaMonHoc(id);
            Console.WriteLine("Mon hoc da duoc xoa. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void QuanLyDangKyHoc()
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
                        DangKyMonHoc();
                        break;
                    case "2":
                        XemDangKyHoc();
                        break;
                    case "3":
                        XemDangKyHocTheoSinhVien();
                        break;
                    case "4":
                        HuyDangKyHoc();
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

        private void DangKyMonHoc()
        {
            Console.Write("Ma sinh vien: ");
            var maSinhVien = int.Parse(Console.ReadLine());
            Console.Write("Ma mon hoc: ");
            var maMonHoc = int.Parse(Console.ReadLine());
            Console.Write("Ma hoc ky: ");
            var maHocKy = int.Parse(Console.ReadLine());

            try
            {
                _dangKyHocService.DangKyMonHoc(maSinhVien, maMonHoc, maHocKy);
                Console.WriteLine("Sinh vien da dang ky mon hoc. Nhan bat ky phim nao de tiep tuc.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private void XemDangKyHoc()
        {
            var dangKyHocs = _dangKyHocService.LayTatCaDangKyHoc();
            foreach (var dangKyHoc in dangKyHocs)
            {
                Console.WriteLine(dangKyHoc);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void XemDangKyHocTheoSinhVien()
        {
            Console.Write("Ma sinh vien: ");
            var maSinhVien = int.Parse(Console.ReadLine());
            var dangKyHocs = _dangKyHocService.LayDangKyHocTheoSinhVien(maSinhVien);
            foreach (var dangKyHoc in dangKyHocs)
            {
                Console.WriteLine(dangKyHoc);
            }
            Console.WriteLine("Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }

        private void HuyDangKyHoc()
        {
            Console.Write("Ma dang ky: ");
            var id = int.Parse(Console.ReadLine());
            _dangKyHocService.HuyDangKyHoc(id);
            Console.WriteLine("Da huy dang ky. Nhan bat ky phim nao de tiep tuc.");
            Console.ReadKey();
        }
    }
}
