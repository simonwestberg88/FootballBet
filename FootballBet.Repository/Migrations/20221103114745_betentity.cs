using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballBet.Repository.Migrations
{
    public partial class betentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BetEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Wager = table.Column<int>(type: "int", nullable: false),
                    WagerAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaybackAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsWinningBet = table.Column<bool>(type: "bit", nullable: false),
                    HasBeenPayed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BetEntities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BetEntities_MatchEntities_MatchId",
                        column: x => x.MatchId,
                        principalTable: "MatchEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BetEntities_MatchId",
                table: "BetEntities",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_BetEntities_UserId",
                table: "BetEntities",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BetEntities");
        }
    }
}
