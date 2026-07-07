using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppUsers_ObjectId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "AppUsers");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AccountCreatedAt",
                table: "AppUsers",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AccountUpdatedAt",
                table: "AppUsers",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AppUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountCreatedAt",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "AccountUpdatedAt",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "AppUsers");

            migrationBuilder.AddColumn<string>(
                name: "ObjectId",
                table: "AppUsers",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_ObjectId",
                table: "AppUsers",
                column: "ObjectId",
                unique: true,
                filter: "[ObjectId] IS NOT NULL");
        }
    }
}
