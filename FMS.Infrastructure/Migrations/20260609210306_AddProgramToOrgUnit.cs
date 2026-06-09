using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProgramToOrgUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserProgramId",
                table: "OrganizationalUnits",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalUnits_UserProgramId",
                table: "OrganizationalUnits",
                column: "UserProgramId");

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
                name: "FK_OrganizationalUnits_UserPrograms_UserProgramId",
                table: "OrganizationalUnits");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationalUnits_UserProgramId",
                table: "OrganizationalUnits");

            migrationBuilder.DropColumn(
                name: "UserProgramId",
                table: "OrganizationalUnits");
        }
    }
}
