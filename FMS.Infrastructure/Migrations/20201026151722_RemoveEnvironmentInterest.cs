using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class RemoveEnvironmentInterest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetCodes_EnvironmentalInterests_EnvironmentalInterestId",
                table: "BudgetCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_EnvironmentalInterests_EnvironmentalInterestId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_FacilityStatuses_EnvironmentalInterests_EnvironmentalInterestId",
                table: "FacilityStatuses");

            migrationBuilder.DropTable(
                name: "EnvironmentalInterests");

            migrationBuilder.DropIndex(
                name: "IX_FacilityStatuses_EnvironmentalInterestId",
                table: "FacilityStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_EnvironmentalInterestId",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_BudgetCodes_EnvironmentalInterestId",
                table: "BudgetCodes");

            migrationBuilder.DropColumn(
                name: "EnvironmentalInterestId",
                table: "FacilityStatuses");

            migrationBuilder.DropColumn(
                name: "EnvironmentalInterestId",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "EnvironmentalInterestId",
                table: "BudgetCodes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EnvironmentalInterestId",
                table: "FacilityStatuses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EnvironmentalInterestId",
                table: "Facilities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EnvironmentalInterestId",
                table: "BudgetCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EnvironmentalInterests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalInterests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacilityStatuses_EnvironmentalInterestId",
                table: "FacilityStatuses",
                column: "EnvironmentalInterestId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_EnvironmentalInterestId",
                table: "Facilities",
                column: "EnvironmentalInterestId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCodes_EnvironmentalInterestId",
                table: "BudgetCodes",
                column: "EnvironmentalInterestId");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetCodes_EnvironmentalInterests_EnvironmentalInterestId",
                table: "BudgetCodes",
                column: "EnvironmentalInterestId",
                principalTable: "EnvironmentalInterests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_EnvironmentalInterests_EnvironmentalInterestId",
                table: "Facilities",
                column: "EnvironmentalInterestId",
                principalTable: "EnvironmentalInterests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FacilityStatuses_EnvironmentalInterests_EnvironmentalInterestId",
                table: "FacilityStatuses",
                column: "EnvironmentalInterestId",
                principalTable: "EnvironmentalInterests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
