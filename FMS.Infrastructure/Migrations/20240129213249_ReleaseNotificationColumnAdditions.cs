using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReleaseNotificationColumnAdditions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdditionalDataRequested",
                table: "Facilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Facilities",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DeferredOnSiteScoring",
                table: "Facilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DeterminationLetterDate",
                table: "Facilities",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HSInumber",
                table: "Facilities",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasERecord",
                table: "Facilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HistoricalComplianceOfficer",
                table: "Facilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoricalUnit",
                table: "Facilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ImageChecked",
                table: "Facilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PreRQSMcleanup",
                table: "Facilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "RNDateReceived",
                table: "Facilities",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxId",
                table: "Facilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VRPReferral",
                table: "Facilities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalDataRequested",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "DeferredOnSiteScoring",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "DeterminationLetterDate",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "HSInumber",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "HasERecord",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "HistoricalComplianceOfficer",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "HistoricalUnit",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "ImageChecked",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "PreRQSMcleanup",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "RNDateReceived",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "VRPReferral",
                table: "Facilities");
        }
    }
}
