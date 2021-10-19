using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballBet.Server.Data.Migrations
{
    public partial class AddedBettingGroupandMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BettingGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BettingGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BettingGroupMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BettingGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BettingGroupMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BettingGroupMembers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BettingGroupMembers_BettingGroups_BettingGroupId",
                        column: x => x.BettingGroupId,
                        principalTable: "BettingGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BettingGroupMembers_BettingGroupId",
                table: "BettingGroupMembers",
                column: "BettingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BettingGroupMembers_UserId",
                table: "BettingGroupMembers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BettingGroupMembers");

            migrationBuilder.DropTable(
                name: "BettingGroups");
        }
    }
}
