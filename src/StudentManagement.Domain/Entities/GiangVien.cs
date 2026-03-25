namespace StudentManagement.Domain.Entities;

public class GiangVien : BaseAuditableEntity
{
    public int GiangVienId { get; set; }
    public string MaGiangVien { get; set; } = string.Empty;
    public string HoTen { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int? KhoaId { get; set; }

    public Khoa? Khoa { get; set; }
    public ICollection<LopHoc> LopHocCoVans { get; set; } = new List<LopHoc>();
    public ICollection<MonHoc> MonHocGiangDays { get; set; } = new List<MonHoc>();
    public ICollection<Khoa> KhoaLamTruongKhoa { get; set; } = new List<Khoa>();
}
