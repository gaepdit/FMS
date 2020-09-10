using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class UpdateCabinet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileCabinets_Counties_EndCountyId",
                table: "FileCabinets");

            migrationBuilder.DropForeignKey(
                name: "FK_FileCabinets_Counties_StartCountyId",
                table: "FileCabinets");

            migrationBuilder.DropIndex(
                name: "IX_FileCabinets_EndCountyId",
                table: "FileCabinets");

            migrationBuilder.DropIndex(
                name: "IX_FileCabinets_StartCountyId",
                table: "FileCabinets");

            migrationBuilder.DropColumn(
                name: "EndCountyId",
                table: "FileCabinets");

            migrationBuilder.DropColumn(
                name: "EndSequence",
                table: "FileCabinets");

            migrationBuilder.DropColumn(
                name: "StartCountyId",
                table: "FileCabinets");

            migrationBuilder.DropColumn(
                name: "StartSequence",
                table: "FileCabinets");

            migrationBuilder.CreateIndex(
                name: "IX_FileCabinets_Name",
                table: "FileCabinets",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileCabinets_Name",
                table: "FileCabinets");

            migrationBuilder.AddColumn<int>(
                name: "EndCountyId",
                table: "FileCabinets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndSequence",
                table: "FileCabinets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartCountyId",
                table: "FileCabinets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartSequence",
                table: "FileCabinets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FileCabinets_EndCountyId",
                table: "FileCabinets",
                column: "EndCountyId");

            migrationBuilder.CreateIndex(
                name: "IX_FileCabinets_StartCountyId",
                table: "FileCabinets",
                column: "StartCountyId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileCabinets_Counties_EndCountyId",
                table: "FileCabinets",
                column: "EndCountyId",
                principalTable: "Counties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FileCabinets_Counties_StartCountyId",
                table: "FileCabinets",
                column: "StartCountyId",
                principalTable: "Counties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
