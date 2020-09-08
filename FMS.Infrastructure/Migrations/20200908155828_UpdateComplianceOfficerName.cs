using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class UpdateComplianceOfficerName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ComplianceOfficers");

            migrationBuilder.AddColumn<string>(
                name: "FamilyName",
                table: "ComplianceOfficers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GivenName",
                table: "ComplianceOfficers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FamilyName",
                table: "ComplianceOfficers");

            migrationBuilder.DropColumn(
                name: "GivenName",
                table: "ComplianceOfficers");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ComplianceOfficers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
