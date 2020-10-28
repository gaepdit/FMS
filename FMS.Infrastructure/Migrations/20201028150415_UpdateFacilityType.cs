using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class UpdateFacilityType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "FacilityTypes");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FacilityTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacilityTypes_Name",
                table: "FacilityTypes",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FacilityTypes_Name",
                table: "FacilityTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FacilityTypes");

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "FacilityTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
