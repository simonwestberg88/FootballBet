using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballBet.Server.Data.Migrations
{
    public partial class AddedDescriptionToBettingGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BettingGroups",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "BettingGroups");
        }
    }
}
