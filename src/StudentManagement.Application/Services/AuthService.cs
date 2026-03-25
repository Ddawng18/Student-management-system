using System.Security.Cryptography;
using System.Text;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Interfaces.Repositories;
using StudentManagement.Application.Interfaces.Services;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly ITaiKhoanNguoiDungRepository _taiKhoanRepository;

    public AuthService(ITaiKhoanNguoiDungRepository taiKhoanRepository)
    {
        _taiKhoanRepository = taiKhoanRepository;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var hoTen = request.HoTen.Trim();

        if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(request.MatKhau))
        {
            throw new InvalidOperationException("Vui lòng nhập đầy đủ họ tên, email và mật khẩu.");
        }

        if (request.MatKhau.Length < 3)
        {
            throw new InvalidOperationException("Mật khẩu phải có ít nhất 3 ký tự.");
        }

        var existing = await _taiKhoanRepository.GetByEmailAsync(email);
        if (existing is not null)
        {
            throw new InvalidOperationException("Email đã được sử dụng.");
        }

        var entity = new TaiKhoanNguoiDung
        {
            HoTen = hoTen,
            Email = email,
            MatKhauHash = HashPassword(request.MatKhau),
            VaiTro = "User"
        };

        await _taiKhoanRepository.AddAsync(entity);
        await _taiKhoanRepository.SaveChangesAsync();

        return new AuthResponse(
            AccessToken: GenerateToken(entity.Email, entity.VaiTro),
            HoTen: entity.HoTen,
            Email: entity.Email,
            VaiTro: entity.VaiTro);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(request.MatKhau))
        {
            throw new InvalidOperationException("Tên đăng nhập/email và mật khẩu không được để trống.");
        }

        var entity = await _taiKhoanRepository.GetByEmailAsync(email);
        if (entity is null)
        {
            throw new InvalidOperationException("Thông tin đăng nhập không hợp lệ.");
        }

        var incomingHash = HashPassword(request.MatKhau);
        if (!string.Equals(incomingHash, entity.MatKhauHash, StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Thông tin đăng nhập không hợp lệ.");
        }

        return new AuthResponse(
            AccessToken: GenerateToken(entity.Email, entity.VaiTro),
            HoTen: entity.HoTen,
            Email: entity.Email,
            VaiTro: entity.VaiTro);
    }

    private static string HashPassword(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = SHA256.HashData(bytes);
        return Convert.ToHexString(hash);
    }

    private static string GenerateToken(string email, string vaiTro)
    {
        var payload = $"{email}|{vaiTro}|{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(payload));
    }
}
