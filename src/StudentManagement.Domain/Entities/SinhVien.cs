namespace StudentManagement.Domain.Entities;

public class SinhVien : BaseAuditableEntity
{
    public int SinhVienId { get; set; }
    public string MaSinhVien { get; set; } = string.Empty;
    public string HoTen { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime NgaySinh { get; set; }
    public int? LopHocId { get; set; }

    public LopHoc? LopHoc { get; set; }
    public ICollection<DangKyHoc> DangKyHocs { get; set; } = new List<DangKyHoc>();
}
