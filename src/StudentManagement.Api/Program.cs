using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using StudentManagement.Application;
using StudentManagement.Infrastructure;
using StudentManagement.Infrastructure.Persistence;
using StudentManagement.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:3000",
                "http://127.0.0.1:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();
var webClientPublicPath = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "..", "web-client", "public"));
var webClientFileProvider = new PhysicalFileProvider(webClientPublicPath);

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
    dbContext.Database.ExecuteSqlRaw(@"
        CREATE TABLE IF NOT EXISTS TaiKhoanNguoiDung (
            TaiKhoanNguoiDungId INTEGER PRIMARY KEY AUTOINCREMENT,
            HoTen TEXT NOT NULL,
            Email TEXT NOT NULL,
            MatKhauHash TEXT NOT NULL,
            VaiTro TEXT NOT NULL,
            CreatedAt TEXT NOT NULL,
            UpdatedAt TEXT NOT NULL
        );
    ");
    dbContext.Database.ExecuteSqlRaw("CREATE UNIQUE INDEX IF NOT EXISTS IX_TaiKhoanNguoiDung_Email ON TaiKhoanNguoiDung(Email);");
    EnsureSinhVienProfileColumns(dbContext);

    EnsureExclusiveAdmin(dbContext);
    SeedDemoData(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseCors("FrontendPolicy");
var defaultFilesOptions = new DefaultFilesOptions();
defaultFilesOptions.DefaultFileNames.Clear();
defaultFilesOptions.DefaultFileNames.Add("dashboard.html");
defaultFilesOptions.FileProvider = webClientFileProvider;
app.UseDefaultFiles(defaultFilesOptions);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = webClientFileProvider
});
app.UseAuthorization();

app.MapControllers();

app.MapGet("/error", () => Results.Problem("An unexpected error occurred."));
app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));

app.Run();

static void EnsureExclusiveAdmin(AppDbContext dbContext)
{
    const string adminLogin = "admin123";
    const string adminPassword = "123";
    const string adminDisplayName = "Admin";

    var admin = dbContext.TaiKhoanNguoiDungs.FirstOrDefault(x => x.Email == adminLogin);
    var hash = HashPassword(adminPassword);

    if (admin is null)
    {
        dbContext.TaiKhoanNguoiDungs.Add(new TaiKhoanNguoiDung
        {
            HoTen = adminDisplayName,
            Email = adminLogin,
            MatKhauHash = hash,
            VaiTro = "Admin"
        });
        dbContext.SaveChanges();
        return;
    }

    var changed = false;
    if (!string.Equals(admin.MatKhauHash, hash, StringComparison.Ordinal))
    {
        admin.MatKhauHash = hash;
        changed = true;
    }

    if (!string.Equals(admin.VaiTro, "Admin", StringComparison.Ordinal))
    {
        admin.VaiTro = "Admin";
        changed = true;
    }

    if (!string.Equals(admin.HoTen, adminDisplayName, StringComparison.Ordinal))
    {
        admin.HoTen = adminDisplayName;
        changed = true;
    }

    if (changed)
    {
        dbContext.SaveChanges();
    }
}

static void SeedDemoData(AppDbContext dbContext)
{
    var khoaCntt = dbContext.Khoas.FirstOrDefault(x => x.MaKhoa == "CNTT");
    if (khoaCntt is null)
    {
        khoaCntt = new Khoa { MaKhoa = "CNTT", TenKhoa = "Công nghệ thông tin" };
        dbContext.Khoas.Add(khoaCntt);
    }

    var khoaQtkd = dbContext.Khoas.FirstOrDefault(x => x.MaKhoa == "QTKD");
    if (khoaQtkd is null)
    {
        khoaQtkd = new Khoa { MaKhoa = "QTKD", TenKhoa = "Quản trị kinh doanh" };
        dbContext.Khoas.Add(khoaQtkd);
    }

    SaveIfChanged(dbContext);

    var gv01 = dbContext.GiangViens.FirstOrDefault(x => x.MaGiangVien == "GV001");
    if (gv01 is null)
    {
        gv01 = new GiangVien
        {
            MaGiangVien = "GV001",
            HoTen = "Nguyễn Văn Giang",
            Email = "gv001@sms.local",
            KhoaId = khoaCntt.KhoaId
        };
        dbContext.GiangViens.Add(gv01);
    }

    var gv02 = dbContext.GiangViens.FirstOrDefault(x => x.MaGiangVien == "GV002");
    if (gv02 is null)
    {
        gv02 = new GiangVien
        {
            MaGiangVien = "GV002",
            HoTen = "Trần Thị Minh",
            Email = "gv002@sms.local",
            KhoaId = khoaQtkd.KhoaId
        };
        dbContext.GiangViens.Add(gv02);
    }

    var hk = dbContext.HocKys.FirstOrDefault(x => x.MaHocKy == "HK2026A");
    if (hk is null)
    {
        hk = new HocKy
        {
            MaHocKy = "HK2026A",
            TenHocKy = "Học kỳ 1",
            NamHoc = 2026,
            NgayBatDau = new DateTime(2026, 1, 5),
            NgayKetThuc = new DateTime(2026, 5, 30)
        };
        dbContext.HocKys.Add(hk);
    }

    SaveIfChanged(dbContext);

    var lop = dbContext.LopHocs.FirstOrDefault(x => x.MaLop == "SE26A");
    if (lop is null)
    {
        lop = new LopHoc
        {
            MaLop = "SE26A",
            TenLop = "Kỹ thuật phần mềm K26A",
            KhoaId = khoaCntt.KhoaId,
            CoVanHocTapId = gv01.GiangVienId
        };
        dbContext.LopHocs.Add(lop);
    }

    var mon = dbContext.MonHocs.FirstOrDefault(x => x.MaMonHoc == "CS101");
    if (mon is null)
    {
        mon = new MonHoc
        {
            MaMonHoc = "CS101",
            TenMon = "Cơ sở lập trình",
            SoTinChi = 3,
            KhoaId = khoaCntt.KhoaId,
            GiangVienGiangDayId = gv01.GiangVienId,
            HocKyId = hk.HocKyId
        };
        dbContext.MonHocs.Add(mon);
    }

    var mon2 = dbContext.MonHocs.FirstOrDefault(x => x.MaMonHoc == "BA201");
    if (mon2 is null)
    {
        mon2 = new MonHoc
        {
            MaMonHoc = "BA201",
            TenMon = "Nguyên lý quản trị",
            SoTinChi = 2,
            KhoaId = khoaQtkd.KhoaId,
            GiangVienGiangDayId = gv02.GiangVienId,
            HocKyId = hk.HocKyId
        };
        dbContext.MonHocs.Add(mon2);
    }

    SaveIfChanged(dbContext);

    var sv = dbContext.SinhViens.FirstOrDefault(x => x.MaSinhVien == "SV001");
    if (sv is null)
    {
        sv = new SinhVien
        {
            MaSinhVien = "SV001",
            HoTen = "Lê Hoàng Anh",
            Email = "sv001@sms.local",
            NgaySinh = new DateTime(2005, 9, 15),
            LopHocId = lop.LopHocId
        };
        dbContext.SinhViens.Add(sv);
    }

    SaveIfChanged(dbContext);

    var dangKyExists = dbContext.DangKyHocs.Any(x => x.SinhVienId == sv.SinhVienId && x.MonHocId == mon.MonHocId && x.HocKyId == hk.HocKyId);
    if (!dangKyExists)
    {
        dbContext.DangKyHocs.Add(new DangKyHoc
        {
            SinhVienId = sv.SinhVienId,
            MonHocId = mon.MonHocId,
            HocKyId = hk.HocKyId,
            Diem = 8.5m,
            KetQua = "Dat"
        });
        SaveIfChanged(dbContext);
    }

    // Ensure there are at least 100 demo students for UI testing.
    const int targetStudentCount = 100;
    for (var i = 1; i <= targetStudentCount; i++)
    {
        var maSinhVien = $"SV{i:000}";
        var exists = dbContext.SinhViens.Any(x => x.MaSinhVien == maSinhVien);
        if (exists)
        {
            continue;
        }

        dbContext.SinhViens.Add(new SinhVien
        {
            MaSinhVien = maSinhVien,
            HoTen = $"Sinh Vien {i:000}",
            Email = $"sv{i:000}@sms.local",
            NgaySinh = new DateTime(2004 + (i % 3), ((i - 1) % 12) + 1, ((i - 1) % 28) + 1),
            LopHocId = lop.LopHocId
        });
    }

    SaveIfChanged(dbContext);
}

static void SaveIfChanged(AppDbContext dbContext)
{
    if (dbContext.ChangeTracker.HasChanges())
    {
        dbContext.SaveChanges();
    }
}

static void EnsureSinhVienProfileColumns(AppDbContext dbContext)
{
    var connection = dbContext.Database.GetDbConnection();
    if (connection.State != ConnectionState.Open)
    {
        connection.Open();
    }

    var existingColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    using (var command = connection.CreateCommand())
    {
        command.CommandText = "PRAGMA table_info('SinhVien');";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            existingColumns.Add(reader.GetString(1));
        }
    }

    var addColumnStatements = new List<string>();

    void AddColumnIfMissing(string columnName, string sql)
    {
        if (!existingColumns.Contains(columnName))
        {
            addColumnStatements.Add(sql);
        }
    }

    AddColumnIfMissing("AvatarUrl", "ALTER TABLE SinhVien ADD COLUMN AvatarUrl TEXT NULL;");
    AddColumnIfMissing("GioiTinh", "ALTER TABLE SinhVien ADD COLUMN GioiTinh TEXT NULL;");
    AddColumnIfMissing("QueQuan", "ALTER TABLE SinhVien ADD COLUMN QueQuan TEXT NULL;");
    AddColumnIfMissing("SoDienThoai", "ALTER TABLE SinhVien ADD COLUMN SoDienThoai TEXT NULL;");
    AddColumnIfMissing("DiaChiThuongTru", "ALTER TABLE SinhVien ADD COLUMN DiaChiThuongTru TEXT NULL;");
    AddColumnIfMissing("NoiTamTru", "ALTER TABLE SinhVien ADD COLUMN NoiTamTru TEXT NULL;");
    AddColumnIfMissing("DanToc", "ALTER TABLE SinhVien ADD COLUMN DanToc TEXT NULL;");
    AddColumnIfMissing("TonGiao", "ALTER TABLE SinhVien ADD COLUMN TonGiao TEXT NULL;");
    AddColumnIfMissing("Cccd", "ALTER TABLE SinhVien ADD COLUMN Cccd TEXT NULL;");
    AddColumnIfMissing("NgayCapCccd", "ALTER TABLE SinhVien ADD COLUMN NgayCapCccd TEXT NULL;");
    AddColumnIfMissing("NoiCapCccd", "ALTER TABLE SinhVien ADD COLUMN NoiCapCccd TEXT NULL;");
    AddColumnIfMissing("HoTenPhuHuynh", "ALTER TABLE SinhVien ADD COLUMN HoTenPhuHuynh TEXT NULL;");
    AddColumnIfMissing("SoDienThoaiPhuHuynh", "ALTER TABLE SinhVien ADD COLUMN SoDienThoaiPhuHuynh TEXT NULL;");
    AddColumnIfMissing("NgheNghiepPhuHuynh", "ALTER TABLE SinhVien ADD COLUMN NgheNghiepPhuHuynh TEXT NULL;");
    AddColumnIfMissing("GhiChu", "ALTER TABLE SinhVien ADD COLUMN GhiChu TEXT NULL;");

    foreach (var statement in addColumnStatements)
    {
        dbContext.Database.ExecuteSqlRaw(statement);
    }
}

static string HashPassword(string password)
{
    var bytes = Encoding.UTF8.GetBytes(password);
    var hash = SHA256.HashData(bytes);
    return Convert.ToHexString(hash);
}
