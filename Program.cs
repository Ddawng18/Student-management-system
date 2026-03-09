using System;
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

            // Khởi tạo và hiển thị menu
            var menuManager = new ConsoleMenuManager(
                khoaService,
                lopHocService,
                giangVienService,
                hocKyService,
                sinhVienService,
                monHocService,
                dangKyHocService
            );

            menuManager.ShowMainMenu();
        }
    }
}
