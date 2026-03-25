namespace StudentManagement.Domain.Entities;

public class DangKyHoc : BaseAuditableEntity
{
    public int DangKyHocId { get; set; }
    public int SinhVienId { get; set; }
    public int MonHocId { get; set; }
    public int HocKyId { get; set; }
    public decimal? Diem { get; set; }
    public string KetQua { get; set; } = "Chua co diem";
    public DateTime NgayDangKy { get; set; } = DateTime.UtcNow;

    public SinhVien SinhVien { get; set; } = null!;
    public MonHoc MonHoc { get; set; } = null!;
    public HocKy HocKy { get; set; } = null!;
}
