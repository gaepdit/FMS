using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDropDownsAndFieldsForStatusAllowedActionsClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "Locations");

            migrationBuilder.AddColumn<Guid>(
                name: "AbandonedInactiveId",
                table: "Statuses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportComments",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationClassId",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CompletionDateRequired",
                table: "AllowedActionsTaken",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DueDateRequired",
                table: "AllowedActionsTaken",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StartDateRequired",
                table: "AllowedActionsTaken",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AbandonedInactives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbandonedInactives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationClasses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_AbandonedInactiveId",
                table: "Statuses",
                column: "AbandonedInactiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_LocationClassId",
                table: "Locations",
                column: "LocationClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_LocationClasses_LocationClassId",
                table: "Locations",
                column: "LocationClassId",
                principalTable: "LocationClasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_AbandonedInactives_AbandonedInactiveId",
                table: "Statuses",
                column: "AbandonedInactiveId",
                principalTable: "AbandonedInactives",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_LocationClasses_LocationClassId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_AbandonedInactives_AbandonedInactiveId",
                table: "Statuses");

            migrationBuilder.DropTable(
                name: "AbandonedInactives");

            migrationBuilder.DropTable(
                name: "LocationClasses");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_AbandonedInactiveId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Locations_LocationClassId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "AbandonedInactiveId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "ReportComments",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "LocationClassId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CompletionDateRequired",
                table: "AllowedActionsTaken");

            migrationBuilder.DropColumn(
                name: "DueDateRequired",
                table: "AllowedActionsTaken");

            migrationBuilder.DropColumn(
                name: "StartDateRequired",
                table: "AllowedActionsTaken");

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
