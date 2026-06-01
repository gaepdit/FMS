using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProgramUnitAndPostionToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserPositionId",
                table: "AppUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserProgramId",
                table: "AppUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserUnitId",
                table: "AppUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_UserPositionId",
                table: "AppUsers",
                column: "UserPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_UserProgramId",
                table: "AppUsers",
                column: "UserProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_UserUnitId",
                table: "AppUsers",
                column: "UserUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_OrganizationalUnits_UserUnitId",
                table: "AppUsers",
                column: "UserUnitId",
                principalTable: "OrganizationalUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_UserPositions_UserPositionId",
                table: "AppUsers",
                column: "UserPositionId",
                principalTable: "UserPositions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_UserPrograms_UserProgramId",
                table: "AppUsers",
                column: "UserProgramId",
                principalTable: "UserPrograms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_OrganizationalUnits_UserUnitId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_UserPositions_UserPositionId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_UserPrograms_UserProgramId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_UserPositionId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_UserProgramId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_UserUnitId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "UserPositionId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "UserProgramId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "UserUnitId",
                table: "AppUsers");
        }
    }
}
