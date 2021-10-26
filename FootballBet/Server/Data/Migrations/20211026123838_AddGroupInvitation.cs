using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballBet.Server.Data.Migrations
{
    public partial class AddGroupInvitation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BettingGroupMembers_BettingGroups_BettingGroupId",
                table: "BettingGroupMembers");

            migrationBuilder.AlterColumn<Guid>(
                name: "BettingGroupId",
                table: "BettingGroupMembers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BettingGroupMembers_BettingGroups_BettingGroupId",
                table: "BettingGroupMembers",
                column: "BettingGroupId",
                principalTable: "BettingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BettingGroupMembers_BettingGroups_BettingGroupId",
                table: "BettingGroupMembers");

            migrationBuilder.AlterColumn<Guid>(
                name: "BettingGroupId",
                table: "BettingGroupMembers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_BettingGroupMembers_BettingGroups_BettingGroupId",
                table: "BettingGroupMembers",
                column: "BettingGroupId",
                principalTable: "BettingGroups",
                principalColumn: "Id");
        }
    }
}
