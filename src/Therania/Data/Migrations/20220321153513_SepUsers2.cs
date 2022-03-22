using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Therania.Migrations
{
    public partial class SepUsers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPatient",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPatient",
                table: "AspNetUsers");
        }
    }
}
