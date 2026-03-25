namespace StudentManagement.Domain.Entities;

public class TaiKhoanNguoiDung : BaseAuditableEntity
{
    public int TaiKhoanNguoiDungId { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MatKhauHash { get; set; } = string.Empty;
    public string VaiTro { get; set; } = "User";
}
