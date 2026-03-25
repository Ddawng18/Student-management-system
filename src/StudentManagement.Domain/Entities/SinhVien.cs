namespace StudentManagement.Domain.Entities;

public class SinhVien : BaseAuditableEntity
{
    public int SinhVienId { get; set; }
    public string MaSinhVien { get; set; } = string.Empty;
    public string HoTen { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime NgaySinh { get; set; }
    public int? LopHocId { get; set; }
    public string? AvatarUrl { get; set; }
    public string? GioiTinh { get; set; }
    public string? QueQuan { get; set; }
    public string? SoDienThoai { get; set; }
    public string? DiaChiThuongTru { get; set; }
    public string? NoiTamTru { get; set; }
    public string? DanToc { get; set; }
    public string? TonGiao { get; set; }
    public string? Cccd { get; set; }
    public DateTime? NgayCapCccd { get; set; }
    public string? NoiCapCccd { get; set; }
    public string? HoTenPhuHuynh { get; set; }
    public string? SoDienThoaiPhuHuynh { get; set; }
    public string? NgheNghiepPhuHuynh { get; set; }
    public string? GhiChu { get; set; }

    public LopHoc? LopHoc { get; set; }
    public ICollection<DangKyHoc> DangKyHocs { get; set; } = new List<DangKyHoc>();
}
