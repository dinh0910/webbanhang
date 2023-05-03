using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webbanhang.Migrations
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "HinhAnh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MoTa",
                columns: table => new
                {
                    MoTaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanPhamID = table.Column<int>(type: "int", nullable: false),
                    NoiDungMoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoTa", x => x.MoTaID);
                    table.ForeignKey(
                        name: "FK_MoTa_SanPham_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPham",
                        principalColumn: "SanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoTa_SanPhamID",
                table: "MoTa",
                column: "SanPhamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoTa");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "HinhAnh");
        }
    }
}
