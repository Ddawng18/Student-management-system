namespace StudentManagement.Domain.Entities;

public class LopHoc : BaseAuditableEntity
{
    public int LopHocId { get; set; }
    public string MaLop { get; set; } = string.Empty;
    public string TenLop { get; set; } = string.Empty;
    public int KhoaId { get; set; }
    public int? CoVanHocTapId { get; set; }

    public Khoa Khoa { get; set; } = null!;
    public GiangVien? CoVanHocTap { get; set; }
    public ICollection<SinhVien> SinhViens { get; set; } = new List<SinhVien>();
}
