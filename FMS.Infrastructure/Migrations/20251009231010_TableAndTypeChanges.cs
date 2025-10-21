using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TableAndTypeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_AbandonSites_AbandonSitesId",
                table: "Statuses");

            migrationBuilder.DropTable(
                name: "AbandonSites");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_AbandonSitesId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "AbandonSitesId",
                table: "Statuses");

            migrationBuilder.AlterColumn<decimal>(
                name: "CostEstimate",
                table: "Statuses",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OnsiteScoreValue",
                table: "OnsiteScores",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "BrownfieldTerminated",
                table: "HsrpFacilityProperties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VRPTerminated",
                table: "HsrpFacilityProperties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "GWScore",
                table: "GroundwaterScores",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrownfieldTerminated",
                table: "HsrpFacilityProperties");

            migrationBuilder.DropColumn(
                name: "VRPTerminated",
                table: "HsrpFacilityProperties");

            migrationBuilder.AlterColumn<double>(
                name: "CostEstimate",
                table: "Statuses",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AbandonSitesId",
                table: "Statuses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OnsiteScoreValue",
                table: "OnsiteScores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "GWScore",
                table: "GroundwaterScores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "AbandonSites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbandonSites", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_AbandonSitesId",
                table: "Statuses",
                column: "AbandonSitesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_AbandonSites_AbandonSitesId",
                table: "Statuses",
                column: "AbandonSitesId",
                principalTable: "AbandonSites",
                principalColumn: "Id");
        }
    }
}
