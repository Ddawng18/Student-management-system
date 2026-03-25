namespace StudentManagement.Domain.Entities;

public class HocKy : BaseAuditableEntity
{
    public int HocKyId { get; set; }
    public string MaHocKy { get; set; } = string.Empty;
    public string TenHocKy { get; set; } = string.Empty;
    public int NamHoc { get; set; }
    public DateTime? NgayBatDau { get; set; }
    public DateTime? NgayKetThuc { get; set; }

    public ICollection<MonHoc> MonHocs { get; set; } = new List<MonHoc>();
    public ICollection<DangKyHoc> DangKyHocs { get; set; } = new List<DangKyHoc>();
}
