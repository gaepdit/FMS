using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class UpdateComplianceOfficer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ComplianceOfficers",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "ComplianceOfficers");
        }
    }
}
