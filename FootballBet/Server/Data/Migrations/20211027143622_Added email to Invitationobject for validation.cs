using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballBet.Server.Data.Migrations
{
    public partial class AddedemailtoInvitationobjectforvalidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BettingGroupInvitations",
                columns: table => new
                {
                    BettingGroupInvitationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BettingGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvitedUserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvitingUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BettingGroupInvitations", x => x.BettingGroupInvitationId);
                    table.ForeignKey(
                        name: "FK_BettingGroupInvitations_AspNetUsers_InvitingUserId",
                        column: x => x.InvitingUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BettingGroupInvitations_BettingGroups_BettingGroupId",
                        column: x => x.BettingGroupId,
                        principalTable: "BettingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BettingGroupInvitations_BettingGroupId",
                table: "BettingGroupInvitations",
                column: "BettingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BettingGroupInvitations_InvitingUserId",
                table: "BettingGroupInvitations",
                column: "InvitingUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BettingGroupInvitations");
        }
    }
}
