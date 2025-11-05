using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveContactTitleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactTitles_ContactTitleId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "ContactTitles");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ContactTitleId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactTitleId",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "ContactTitle",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactTitle",
                table: "Contacts");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactTitleId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactTitles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTitles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactTitleId",
                table: "Contacts",
                column: "ContactTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactTitles_ContactTitleId",
                table: "Contacts",
                column: "ContactTitleId",
                principalTable: "ContactTitles",
                principalColumn: "Id");
        }
    }
}
