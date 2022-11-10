using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballBet.Repository.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BetEntities_AspNetUsers_ApplicationUserId",
                table: "BetEntities");

            migrationBuilder.DropIndex(
                name: "IX_BetEntities_ApplicationUserId",
                table: "BetEntities");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "BetEntities");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "HasBeenPayed",
                table: "BetEntities",
                newName: "Processed");

            migrationBuilder.CreateTable(
                name: "UserBalanceEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBalanceEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBalanceEntities");

            migrationBuilder.RenameColumn(
                name: "Processed",
                table: "BetEntities",
                newName: "HasBeenPayed");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "BetEntities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_BetEntities_ApplicationUserId",
                table: "BetEntities",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BetEntities_AspNetUsers_ApplicationUserId",
                table: "BetEntities",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
