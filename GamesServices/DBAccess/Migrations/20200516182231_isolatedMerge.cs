using Microsoft.EntityFrameworkCore.Migrations;

namespace DBAccess.Migrations
{
    public partial class isolatedMerge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePricesMerge_Currencies_CurrencyId",
                table: "GamePricesMerge");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePricesMerge_Games_GameId",
                table: "GamePricesMerge");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePricesMerge_Platforms_PlatformId",
                table: "GamePricesMerge");

            migrationBuilder.DropIndex(
                name: "IX_GamePricesMerge_CurrencyId",
                table: "GamePricesMerge");

            migrationBuilder.DropIndex(
                name: "IX_GamePricesMerge_GameId",
                table: "GamePricesMerge");

            migrationBuilder.DropIndex(
                name: "IX_GamePricesMerge_PlatformId",
                table: "GamePricesMerge");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GamePricesMerge_CurrencyId",
                table: "GamePricesMerge",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePricesMerge_GameId",
                table: "GamePricesMerge",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePricesMerge_PlatformId",
                table: "GamePricesMerge",
                column: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePricesMerge_Currencies_CurrencyId",
                table: "GamePricesMerge",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePricesMerge_Games_GameId",
                table: "GamePricesMerge",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePricesMerge_Platforms_PlatformId",
                table: "GamePricesMerge",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "PlatformId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
