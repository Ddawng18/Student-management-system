using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HocKy",
                columns: table => new
                {
                    HocKyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHocKy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenHocKy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NamHoc = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocKy", x => x.HocKyId);
                    table.CheckConstraint("CK_HocKy_NamHoc", "NamHoc >= 2000 AND NamHoc <= 2100");
                    table.CheckConstraint("CK_HocKy_ThoiGian", "NgayBatDau IS NULL OR NgayKetThuc IS NULL OR NgayBatDau <= NgayKetThuc");
                });

            migrationBuilder.CreateTable(
                name: "DangKyHoc",
                columns: table => new
                {
                    DangKyHocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SinhVienId = table.Column<int>(type: "int", nullable: false),
                    MonHocId = table.Column<int>(type: "int", nullable: false),
                    HocKyId = table.Column<int>(type: "int", nullable: false),
                    Diem = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    KetQua = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Chua co diem"),
                    NgayDangKy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangKyHoc", x => x.DangKyHocId);
                    table.CheckConstraint("CK_DangKyHoc_Diem", "Diem IS NULL OR (Diem >= 0 AND Diem <= 10)");
                    table.ForeignKey(
                        name: "FK_DangKyHoc_HocKy_HocKyId",
                        column: x => x.HocKyId,
                        principalTable: "HocKy",
                        principalColumn: "HocKyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GiangVien",
                columns: table => new
                {
                    GiangVienId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGiangVien = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangVien", x => x.GiangVienId);
                });

            migrationBuilder.CreateTable(
                name: "Khoa",
                columns: table => new
                {
                    KhoaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhoa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenKhoa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TruongKhoaId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoa", x => x.KhoaId);
                    table.ForeignKey(
                        name: "FK_Khoa_GiangVien_TruongKhoaId",
                        column: x => x.TruongKhoaId,
                        principalTable: "GiangVien",
                        principalColumn: "GiangVienId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "LopHoc",
                columns: table => new
                {
                    LopHocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLop = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenLop = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: false),
                    CoVanHocTapId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHoc", x => x.LopHocId);
                    table.ForeignKey(
                        name: "FK_LopHoc_GiangVien_CoVanHocTapId",
                        column: x => x.CoVanHocTapId,
                        principalTable: "GiangVien",
                        principalColumn: "GiangVienId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_LopHoc_Khoa_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoa",
                        principalColumn: "KhoaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MonHoc",
                columns: table => new
                {
                    MonHocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaMonHoc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenMon = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SoTinChi = table.Column<int>(type: "int", nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: true),
                    GiangVienGiangDayId = table.Column<int>(type: "int", nullable: true),
                    HocKyId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHoc", x => x.MonHocId);
                    table.CheckConstraint("CK_MonHoc_SoTinChi", "SoTinChi BETWEEN 1 AND 10");
                    table.ForeignKey(
                        name: "FK_MonHoc_GiangVien_GiangVienGiangDayId",
                        column: x => x.GiangVienGiangDayId,
                        principalTable: "GiangVien",
                        principalColumn: "GiangVienId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_MonHoc_HocKy_HocKyId",
                        column: x => x.HocKyId,
                        principalTable: "HocKy",
                        principalColumn: "HocKyId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_MonHoc_Khoa_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoa",
                        principalColumn: "KhoaId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    SinhVienId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSinhVien = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LopHocId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien", x => x.SinhVienId);
                    table.ForeignKey(
                        name: "FK_SinhVien_LopHoc_LopHocId",
                        column: x => x.LopHocId,
                        principalTable: "LopHoc",
                        principalColumn: "LopHocId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DangKyHoc_HocKyId",
                table: "DangKyHoc",
                column: "HocKyId");

            migrationBuilder.CreateIndex(
                name: "IX_DangKyHoc_MonHocId",
                table: "DangKyHoc",
                column: "MonHocId");

            migrationBuilder.CreateIndex(
                name: "IX_DangKyHoc_SinhVienId_MonHocId_HocKyId",
                table: "DangKyHoc",
                columns: new[] { "SinhVienId", "MonHocId", "HocKyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiangVien_Email",
                table: "GiangVien",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiangVien_KhoaId",
                table: "GiangVien",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_GiangVien_MaGiangVien",
                table: "GiangVien",
                column: "MaGiangVien",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HocKy_MaHocKy",
                table: "HocKy",
                column: "MaHocKy",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Khoa_MaKhoa",
                table: "Khoa",
                column: "MaKhoa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Khoa_TruongKhoaId",
                table: "Khoa",
                column: "TruongKhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_CoVanHocTapId",
                table: "LopHoc",
                column: "CoVanHocTapId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_KhoaId",
                table: "LopHoc",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_MaLop",
                table: "LopHoc",
                column: "MaLop",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonHoc_GiangVienGiangDayId",
                table: "MonHoc",
                column: "GiangVienGiangDayId");

            migrationBuilder.CreateIndex(
                name: "IX_MonHoc_HocKyId",
                table: "MonHoc",
                column: "HocKyId");

            migrationBuilder.CreateIndex(
                name: "IX_MonHoc_KhoaId",
                table: "MonHoc",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_MonHoc_MaMonHoc",
                table: "MonHoc",
                column: "MaMonHoc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_Email",
                table: "SinhVien",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_LopHocId",
                table: "SinhVien",
                column: "LopHocId");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_MaSinhVien",
                table: "SinhVien",
                column: "MaSinhVien",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DangKyHoc_MonHoc_MonHocId",
                table: "DangKyHoc",
                column: "MonHocId",
                principalTable: "MonHoc",
                principalColumn: "MonHocId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DangKyHoc_SinhVien_SinhVienId",
                table: "DangKyHoc",
                column: "SinhVienId",
                principalTable: "SinhVien",
                principalColumn: "SinhVienId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GiangVien_Khoa_KhoaId",
                table: "GiangVien",
                column: "KhoaId",
                principalTable: "Khoa",
                principalColumn: "KhoaId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiangVien_Khoa_KhoaId",
                table: "GiangVien");

            migrationBuilder.DropTable(
                name: "DangKyHoc");

            migrationBuilder.DropTable(
                name: "MonHoc");

            migrationBuilder.DropTable(
                name: "SinhVien");

            migrationBuilder.DropTable(
                name: "HocKy");

            migrationBuilder.DropTable(
                name: "LopHoc");

            migrationBuilder.DropTable(
                name: "Khoa");

            migrationBuilder.DropTable(
                name: "GiangVien");
        }
    }
}
