using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyScoreAndSubstanceTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_ComplianceOfficers_ScoredById",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_ScoredById",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "ScoredById",
                table: "Scores");

            migrationBuilder.RenameColumn(
                name: "UseForScoring",
                table: "Substances",
                newName: "UseForSoilScoring");

            migrationBuilder.AddColumn<bool>(
                name: "UseForGroundwaterScoring",
                table: "Substances",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseForGroundwaterScoring",
                table: "Substances");

            migrationBuilder.RenameColumn(
                name: "UseForSoilScoring",
                table: "Substances",
                newName: "UseForScoring");

            migrationBuilder.AddColumn<Guid>(
                name: "ScoredById",
                table: "Scores",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_ScoredById",
                table: "Scores",
                column: "ScoredById");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_ComplianceOfficers_ScoredById",
                table: "Scores",
                column: "ScoredById",
                principalTable: "ComplianceOfficers",
                principalColumn: "Id");
        }
    }
}
