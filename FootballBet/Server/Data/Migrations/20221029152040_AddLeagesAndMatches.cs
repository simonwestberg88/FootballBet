using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballBet.Server.Data.Migrations
{
    public partial class AddLeagesAndMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeagueEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Season = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeFulltimeGoals = table.Column<int>(type: "int", nullable: true),
                    AwayFulltimeGoals = table.Column<int>(type: "int", nullable: true),
                    HomeCurrentGoals = table.Column<int>(type: "int", nullable: true),
                    AwayCurrentGoals = table.Column<int>(type: "int", nullable: true),
                    HomePenaltyGoals = table.Column<int>(type: "int", nullable: true),
                    AwayPenaltyGoals = table.Column<int>(type: "int", nullable: true),
                    AwayTeamId = table.Column<int>(type: "int", nullable: true),
                    HomeTeamId = table.Column<int>(type: "int", nullable: true),
                    MatchStatus = table.Column<int>(type: "int", nullable: false),
                    Round = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Season = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchEntities_LeagueEntities_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "LeagueEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchEntities_TeamEntities_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "TeamEntities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MatchEntities_TeamEntities_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "TeamEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchEntities_AwayTeamId",
                table: "MatchEntities",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchEntities_HomeTeamId",
                table: "MatchEntities",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchEntities_LeagueId",
                table: "MatchEntities",
                column: "LeagueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchEntities");

            migrationBuilder.DropTable(
                name: "LeagueEntities");

            migrationBuilder.DropTable(
                name: "TeamEntities");
        }
    }
}
