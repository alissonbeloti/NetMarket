using Microsoft.EntityFrameworkCore.Migrations;

namespace NetMarket.BusinessLogic.Identity.Migrations
{
    public partial class SegurancaImagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagem",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "AspNetUsers");
        }
    }
}
