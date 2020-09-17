using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class AddCabinetFileJoin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileCabinets_Files_FileId",
                table: "FileCabinets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileCabinets",
                table: "FileCabinets");

            migrationBuilder.DropIndex(
                name: "IX_FileCabinets_FileId",
                table: "FileCabinets");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "FileCabinets");

            migrationBuilder.RenameTable(
                name: "FileCabinets",
                newName: "Cabinets");

            migrationBuilder.RenameIndex(
                name: "IX_FileCabinets_Name",
                table: "Cabinets",
                newName: "IX_Cabinets_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cabinets",
                table: "Cabinets",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CabinetFileJoin",
                columns: table => new
                {
                    CabinetId = table.Column<Guid>(nullable: false),
                    FileId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinetFileJoin", x => new { x.CabinetId, x.FileId });
                    table.ForeignKey(
                        name: "FK_CabinetFileJoin_Cabinets_CabinetId",
                        column: x => x.CabinetId,
                        principalTable: "Cabinets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CabinetFileJoin_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileLabel",
                table: "Files",
                column: "FileLabel",
                unique: true,
                filter: "[FileLabel] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CabinetFileJoin_FileId",
                table: "CabinetFileJoin",
                column: "FileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CabinetFileJoin");

            migrationBuilder.DropIndex(
                name: "IX_Files_FileLabel",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cabinets",
                table: "Cabinets");

            migrationBuilder.RenameTable(
                name: "Cabinets",
                newName: "FileCabinets");

            migrationBuilder.RenameIndex(
                name: "IX_Cabinets_Name",
                table: "FileCabinets",
                newName: "IX_FileCabinets_Name");

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "FileCabinets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileCabinets",
                table: "FileCabinets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FileCabinets_FileId",
                table: "FileCabinets",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileCabinets_Files_FileId",
                table: "FileCabinets",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
