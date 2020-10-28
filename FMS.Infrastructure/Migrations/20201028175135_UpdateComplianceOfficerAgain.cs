using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class UpdateComplianceOfficerAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplianceOfficers_OrganizationalUnits_UnitId",
                table: "ComplianceOfficers");

            migrationBuilder.DropIndex(
                name: "IX_ComplianceOfficers_UnitId",
                table: "ComplianceOfficers");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "ComplianceOfficers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "ComplianceOfficers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComplianceOfficers_UnitId",
                table: "ComplianceOfficers",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplianceOfficers_OrganizationalUnits_UnitId",
                table: "ComplianceOfficers",
                column: "UnitId",
                principalTable: "OrganizationalUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
