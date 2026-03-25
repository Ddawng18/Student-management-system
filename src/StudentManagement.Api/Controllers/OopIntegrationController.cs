using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/v1/oop")]
public class OopIntegrationController : ControllerBase
{
    private static readonly SinhVienService SinhVienService = new SinhVienService();
    private static readonly GiangVienService GiangVienService = new GiangVienService();
    private static readonly MonHocService MonHocService = new MonHocService();
    private static readonly HocKyService HocKyService = new HocKyService();
    private static readonly LopHocService LopHocService = new LopHocService();
    private static readonly KhoaService KhoaService = new KhoaService();
    private static readonly DangKyHocService DangKyHocService = new DangKyHocService();
    private static bool _seeded;

    [HttpPost("seed")]
    public IActionResult SeedSampleData()
    {
        if (_seeded)
        {
            return Ok(new { message = "OOP data already seeded." });
        }

        KhoaCongNgheThongTin khoa = new KhoaCongNgheThongTin("CNTT", "Khoa Cong nghe thong tin");
        LopHoc lop = new LopHoc("CTK46", "Cong nghe phan mem 46");
        HocKy hocKy = new HocKy("HK2026", "Hoc ky 1", 2026);

        MonHoc monHoc1 = new MonHoc("CS101", "Lap trinh huong doi tuong", 3);
        MonHoc monHoc2 = new MonHoc("CS202", "Co so du lieu", 3);

        SinhVien sinhVien = new SinhVien("SV001", "Nguyen Van A", "a@student.edu", new DateTime(2005, 1, 10));
        GiangVien giangVien = new GiangVien("GV001", "Tran Thi B", "b@teacher.edu");

        KhoaService.Them(khoa);
        LopHocService.Them(lop);
        HocKyService.Them(hocKy);
        MonHocService.Them(monHoc1);
        MonHocService.Them(monHoc2);
        SinhVienService.Them(sinhVien);
        GiangVienService.Them(giangVien);

        KhoaService.GanLopVaoKhoa(khoa.MaKhoa, lop);
        lop.ThemSinhVien(sinhVien);

        DangKyHoc dangKyThuong = DangKyHocService.DangKyMonHoc(sinhVien, monHoc1, hocKy);
        DangKyHoc dangKyClc = DangKyHocService.DangKyMonHocHeChatLuongCao(sinhVien, monHoc2, hocKy);

        giangVien.NhapDiem(dangKyThuong, 7.0f);
        giangVien.NhapDiem(dangKyClc, 6.0f);

        _seeded = true;
        return Ok(new { message = "Seeded OOP sample data successfully." });
    }

    [HttpGet("summary")]
    public IActionResult GetSummary()
    {
        int totalStudents = SinhVienService.LayTatCa().Count;
        int totalLecturers = GiangVienService.LayTatCa().Count;
        int totalCourses = MonHocService.LayTatCa().Count;
        int totalSemesters = HocKyService.LayTatCa().Count;
        int totalClasses = LopHocService.LayTatCa().Count;
        int totalDepartments = KhoaService.LayTatCa().Count;
        int totalEnrollments = DangKyHocService.LayTatCa().Count;

        return Ok(new
        {
            seeded = _seeded,
            totalDepartments,
            totalClasses,
            totalStudents,
            totalLecturers,
            totalCourses,
            totalSemesters,
            totalEnrollments
        });
    }
}
