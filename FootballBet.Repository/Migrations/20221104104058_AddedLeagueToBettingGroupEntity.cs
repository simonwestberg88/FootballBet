using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballBet.Repository.Migrations
{
    public partial class AddedLeagueToBettingGroupEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "BettingGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BettingGroups_LeagueId",
                table: "BettingGroups",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_BettingGroups_LeagueEntities_LeagueId",
                table: "BettingGroups",
                column: "LeagueId",
                principalTable: "LeagueEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BettingGroups_LeagueEntities_LeagueId",
                table: "BettingGroups");

            migrationBuilder.DropIndex(
                name: "IX_BettingGroups_LeagueId",
                table: "BettingGroups");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "BettingGroups");
        }
    }
}
