using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Therania.Migrations
{
    public partial class RemovedUnusedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPatient",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsTherapist",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPatient",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTherapist",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }
    }
}
