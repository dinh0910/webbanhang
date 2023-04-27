using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webbanhang.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThongSo",
                columns: table => new
                {
                    ThongSoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanPhamID = table.Column<int>(type: "int", nullable: false),
                    TenThongSo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongSo", x => x.ThongSoID);
                    table.ForeignKey(
                        name: "FK_ThongSo_SanPham_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPham",
                        principalColumn: "SanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThongSo_SanPhamID",
                table: "ThongSo",
                column: "SanPhamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongSo");
        }
    }
}
