using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationSyncForDasboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserProgramId",
                table: "OrganizationalUnits",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "EventTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ReportEligible",
                table: "EventTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.CreateTable(
                name: "UserPositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPrograms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPrograms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalUnits_UserProgramId",
                table: "OrganizationalUnits",
                column: "UserProgramId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationalUnits_UserPrograms_UserProgramId",
                table: "OrganizationalUnits",
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

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationalUnits_UserPrograms_UserProgramId",
                table: "OrganizationalUnits");

            migrationBuilder.DropTable(
                name: "UserPositions");

            migrationBuilder.DropTable(
                name: "UserPrograms");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationalUnits_UserProgramId",
                table: "OrganizationalUnits");

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
                name: "UserProgramId",
                table: "OrganizationalUnits");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "EventTypes");

            migrationBuilder.DropColumn(
                name: "ReportEligible",
                table: "EventTypes");

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
