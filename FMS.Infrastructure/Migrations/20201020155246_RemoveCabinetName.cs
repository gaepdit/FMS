using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class RemoveCabinetName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cabinets_Name",
                table: "Cabinets");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cabinets");

            migrationBuilder.CreateIndex(
                name: "IX_Cabinets_CabinetNumber",
                table: "Cabinets",
                column: "CabinetNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cabinets_CabinetNumber",
                table: "Cabinets");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cabinets",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.Sql("update Cabinets set Cabinets.Name = 'C' + format (Cabinets.CabinetNumber, '000')");

            migrationBuilder.CreateIndex(
                name: "IX_Cabinets_Name",
                table: "Cabinets",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
