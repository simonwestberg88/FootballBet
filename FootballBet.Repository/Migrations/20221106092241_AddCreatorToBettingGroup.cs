using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballBet.Repository.Migrations
{
    public partial class AddCreatorToBettingGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "BettingGroups",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BettingGroups_CreatorId",
                table: "BettingGroups",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_BettingGroups_AspNetUsers_CreatorId",
                table: "BettingGroups",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BettingGroups_AspNetUsers_CreatorId",
                table: "BettingGroups");

            migrationBuilder.DropIndex(
                name: "IX_BettingGroups_CreatorId",
                table: "BettingGroups");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "BettingGroups");
        }
    }
}
