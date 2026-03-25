namespace StudentManagement.Domain.Entities;

public class MonHoc : BaseAuditableEntity
{
    public int MonHocId { get; set; }
    public string MaMonHoc { get; set; } = string.Empty;
    public string TenMon { get; set; } = string.Empty;
    public int SoTinChi { get; set; }
    public int? KhoaId { get; set; }
    public int? GiangVienGiangDayId { get; set; }
    public int? HocKyId { get; set; }

    public Khoa? Khoa { get; set; }
    public GiangVien? GiangVienGiangDay { get; set; }
    public HocKy? HocKy { get; set; }
    public ICollection<DangKyHoc> DangKyHocs { get; set; } = new List<DangKyHoc>();
}
