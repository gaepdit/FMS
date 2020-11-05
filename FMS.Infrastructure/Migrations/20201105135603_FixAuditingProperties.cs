using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class FixAuditingProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "FacilityList");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "FacilityList");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "FacilityList");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "FacilityList");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "FacilityList",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "FacilityList",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "FacilityList",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "FacilityList",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
