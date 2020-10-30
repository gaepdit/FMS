using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class MakeCabinetNameAString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cabinets",
                nullable: true);

            migrationBuilder.Sql("update dbo.Cabinets set Name = 'C' + FORMAT (CabinetNumber, '000')");

            migrationBuilder.DropIndex(
                name: "IX_Cabinets_CabinetNumber",
                table: "Cabinets");

            migrationBuilder.DropColumn(
                name: "CabinetNumber",
                table: "Cabinets");

            migrationBuilder.CreateIndex(
                name: "IX_Cabinets_Name",
                table: "Cabinets",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cabinets_Name",
                table: "Cabinets");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cabinets");

            migrationBuilder.AddColumn<int>(
                name: "CabinetNumber",
                table: "Cabinets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cabinets_CabinetNumber",
                table: "Cabinets",
                column: "CabinetNumber",
                unique: true);
        }
    }
}
