namespace StudentManagement.Domain.Entities;

public class Khoa : BaseAuditableEntity
{
    public int KhoaId { get; set; }
    public string MaKhoa { get; set; } = string.Empty;
    public string TenKhoa { get; set; } = string.Empty;
    public int? TruongKhoaId { get; set; }

    public GiangVien? TruongKhoa { get; set; }
    public ICollection<LopHoc> LopHocs { get; set; } = new List<LopHoc>();
    public ICollection<GiangVien> GiangViens { get; set; } = new List<GiangVien>();
    public ICollection<MonHoc> MonHocs { get; set; } = new List<MonHoc>();
}
