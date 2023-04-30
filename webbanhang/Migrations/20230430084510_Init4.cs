using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webbanhang.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoTen",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sdt",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "TaiKhoan");

            migrationBuilder.DropColumn(
                name: "HoTen",
                table: "TaiKhoan");

            migrationBuilder.DropColumn(
                name: "Sdt",
                table: "TaiKhoan");
        }
    }
}
