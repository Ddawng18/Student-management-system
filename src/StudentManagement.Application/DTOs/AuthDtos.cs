namespace StudentManagement.Application.DTOs;

public record RegisterRequest(string HoTen, string Email, string MatKhau);
public record LoginRequest(string Email, string MatKhau);
public record AuthResponse(string AccessToken, string HoTen, string Email, string VaiTro);
