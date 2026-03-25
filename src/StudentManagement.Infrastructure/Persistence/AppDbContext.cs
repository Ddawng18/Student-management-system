using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Khoa> Khoas => Set<Khoa>();
    public DbSet<LopHoc> LopHocs => Set<LopHoc>();
    public DbSet<SinhVien> SinhViens => Set<SinhVien>();
    public DbSet<GiangVien> GiangViens => Set<GiangVien>();
    public DbSet<HocKy> HocKys => Set<HocKy>();
    public DbSet<MonHoc> MonHocs => Set<MonHoc>();
    public DbSet<DangKyHoc> DangKyHocs => Set<DangKyHoc>();
    public DbSet<TaiKhoanNguoiDung> TaiKhoanNguoiDungs => Set<TaiKhoanNguoiDung>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Khoa>(entity =>
        {
            entity.ToTable("Khoa");
            entity.HasKey(x => x.KhoaId);
            entity.Property(x => x.MaKhoa).HasMaxLength(20).IsRequired();
            entity.Property(x => x.TenKhoa).HasMaxLength(150).IsRequired();
            entity.HasIndex(x => x.MaKhoa).IsUnique();

            entity.HasOne(x => x.TruongKhoa)
                .WithMany(x => x.KhoaLamTruongKhoa)
                .HasForeignKey(x => x.TruongKhoaId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<GiangVien>(entity =>
        {
            entity.ToTable("GiangVien");
            entity.HasKey(x => x.GiangVienId);
            entity.Property(x => x.MaGiangVien).HasMaxLength(20).IsRequired();
            entity.Property(x => x.HoTen).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(255).IsRequired();
            entity.HasIndex(x => x.MaGiangVien).IsUnique();
            entity.HasIndex(x => x.Email).IsUnique();

            entity.HasOne(x => x.Khoa)
                .WithMany(x => x.GiangViens)
                .HasForeignKey(x => x.KhoaId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<LopHoc>(entity =>
        {
            entity.ToTable("LopHoc");
            entity.HasKey(x => x.LopHocId);
            entity.Property(x => x.MaLop).HasMaxLength(20).IsRequired();
            entity.Property(x => x.TenLop).HasMaxLength(150).IsRequired();
            entity.HasIndex(x => x.MaLop).IsUnique();

            entity.HasOne(x => x.Khoa)
                .WithMany(x => x.LopHocs)
                .HasForeignKey(x => x.KhoaId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.CoVanHocTap)
                .WithMany(x => x.LopHocCoVans)
                .HasForeignKey(x => x.CoVanHocTapId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.ToTable("SinhVien");
            entity.HasKey(x => x.SinhVienId);
            entity.Property(x => x.MaSinhVien).HasMaxLength(20).IsRequired();
            entity.Property(x => x.HoTen).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(255).IsRequired();
            entity.Property(x => x.AvatarUrl).HasMaxLength(2048);
            entity.Property(x => x.GioiTinh).HasMaxLength(20);
            entity.Property(x => x.QueQuan).HasMaxLength(255);
            entity.Property(x => x.SoDienThoai).HasMaxLength(20);
            entity.Property(x => x.DiaChiThuongTru).HasMaxLength(500);
            entity.Property(x => x.NoiTamTru).HasMaxLength(500);
            entity.Property(x => x.DanToc).HasMaxLength(100);
            entity.Property(x => x.TonGiao).HasMaxLength(100);
            entity.Property(x => x.Cccd).HasMaxLength(20);
            entity.Property(x => x.NoiCapCccd).HasMaxLength(255);
            entity.Property(x => x.HoTenPhuHuynh).HasMaxLength(150);
            entity.Property(x => x.SoDienThoaiPhuHuynh).HasMaxLength(20);
            entity.Property(x => x.NgheNghiepPhuHuynh).HasMaxLength(150);
            entity.Property(x => x.GhiChu).HasMaxLength(2000);
            entity.HasIndex(x => x.MaSinhVien).IsUnique();
            entity.HasIndex(x => x.Email).IsUnique();

            entity.HasOne(x => x.LopHoc)
                .WithMany(x => x.SinhViens)
                .HasForeignKey(x => x.LopHocId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<HocKy>(entity =>
        {
            entity.ToTable("HocKy", table =>
            {
                table.HasCheckConstraint("CK_HocKy_NamHoc", "NamHoc >= 2000 AND NamHoc <= 2100");
                table.HasCheckConstraint("CK_HocKy_ThoiGian", "NgayBatDau IS NULL OR NgayKetThuc IS NULL OR NgayBatDau <= NgayKetThuc");
            });
            entity.HasKey(x => x.HocKyId);
            entity.Property(x => x.MaHocKy).HasMaxLength(20).IsRequired();
            entity.Property(x => x.TenHocKy).HasMaxLength(50).IsRequired();
            entity.HasIndex(x => x.MaHocKy).IsUnique();
        });

        modelBuilder.Entity<MonHoc>(entity =>
        {
            entity.ToTable("MonHoc", table =>
            {
                table.HasCheckConstraint("CK_MonHoc_SoTinChi", "SoTinChi BETWEEN 1 AND 10");
            });
            entity.HasKey(x => x.MonHocId);
            entity.Property(x => x.MaMonHoc).HasMaxLength(20).IsRequired();
            entity.Property(x => x.TenMon).HasMaxLength(150).IsRequired();
            entity.HasIndex(x => x.MaMonHoc).IsUnique();

            entity.HasOne(x => x.Khoa)
                .WithMany(x => x.MonHocs)
                .HasForeignKey(x => x.KhoaId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(x => x.GiangVienGiangDay)
                .WithMany(x => x.MonHocGiangDays)
                .HasForeignKey(x => x.GiangVienGiangDayId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(x => x.HocKy)
                .WithMany(x => x.MonHocs)
                .HasForeignKey(x => x.HocKyId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<DangKyHoc>(entity =>
        {
            entity.ToTable("DangKyHoc", table =>
            {
                table.HasCheckConstraint("CK_DangKyHoc_Diem", "Diem IS NULL OR (Diem >= 0 AND Diem <= 10)");
            });
            entity.HasKey(x => x.DangKyHocId);
            entity.Property(x => x.Diem).HasColumnType("decimal(4,2)");
            entity.Property(x => x.KetQua).HasMaxLength(20).HasDefaultValue("Chua co diem").IsRequired();
            entity.HasIndex(x => new { x.SinhVienId, x.MonHocId, x.HocKyId }).IsUnique();

            entity.HasOne(x => x.SinhVien)
                .WithMany(x => x.DangKyHocs)
                .HasForeignKey(x => x.SinhVienId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.MonHoc)
                .WithMany(x => x.DangKyHocs)
                .HasForeignKey(x => x.MonHocId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.HocKy)
                .WithMany(x => x.DangKyHocs)
                .HasForeignKey(x => x.HocKyId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TaiKhoanNguoiDung>(entity =>
        {
            entity.ToTable("TaiKhoanNguoiDung");
            entity.HasKey(x => x.TaiKhoanNguoiDungId);
            entity.Property(x => x.HoTen).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(255).IsRequired();
            entity.Property(x => x.MatKhauHash).HasMaxLength(128).IsRequired();
            entity.Property(x => x.VaiTro).HasMaxLength(20).IsRequired();
            entity.HasIndex(x => x.Email).IsUnique();
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseAuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
