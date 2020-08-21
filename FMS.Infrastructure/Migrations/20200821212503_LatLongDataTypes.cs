using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class LatLongDataTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Facilities",
                type: "decimal(9, 6)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Facilities",
                type: "decimal(8, 6)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Facilities",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9, 6)");

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "Facilities",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8, 6)");
        }
    }
}
