using Microsoft.EntityFrameworkCore.Migrations;

namespace DBAccess.Migrations
{
    public partial class gamePostfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BaseUrl",
                table: "Platforms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GameLinkPostfix",
                table: "GamePrices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "GamePrices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseUrl",
                table: "Platforms");

            migrationBuilder.DropColumn(
                name: "GameLinkPostfix",
                table: "GamePrices");

            migrationBuilder.DropColumn(
                name: "Review",
                table: "GamePrices");
        }
    }
}
