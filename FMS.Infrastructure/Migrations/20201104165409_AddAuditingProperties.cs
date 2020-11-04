using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class AddAuditingProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "RetentionRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "RetentionRecords",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "RetentionRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "RetentionRecords",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "OrganizationalUnits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "OrganizationalUnits",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "OrganizationalUnits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "OrganizationalUnits",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "FacilityTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "FacilityTypes",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "FacilityTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "FacilityTypes",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "FacilityStatuses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "FacilityStatuses",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "FacilityStatuses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "FacilityStatuses",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "FacilityList",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "FacilityList",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "FacilityList",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "FacilityList",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "Facilities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "Facilities",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "Facilities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "Facilities",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "Counties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "Counties",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "Counties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "Counties",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "ComplianceOfficers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "ComplianceOfficers",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "ComplianceOfficers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "ComplianceOfficers",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "Cabinets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "Cabinets",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "Cabinets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "Cabinets",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "CabinetFileJoin",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "CabinetFileJoin",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "CabinetFileJoin",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "CabinetFileJoin",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "BudgetCodes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "BudgetCodes",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "BudgetCodes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "BudgetCodes",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "AppUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "AppUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "AppUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "AppUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "AppUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "AppUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "AppUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "AppUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "AppUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "AppUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "AppUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "AppUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "AppUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "AppUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "AppUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "AppUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "AppRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "AppRoles",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "AppRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "AppRoles",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InsertDateTime",
                table: "AppRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsertUser",
                table: "AppRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateDateTime",
                table: "AppRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "AppRoleClaims",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "RetentionRecords");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "RetentionRecords");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "RetentionRecords");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "RetentionRecords");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "OrganizationalUnits");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "OrganizationalUnits");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "OrganizationalUnits");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "OrganizationalUnits");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "FacilityTypes");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "FacilityTypes");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "FacilityTypes");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "FacilityTypes");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "FacilityStatuses");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "FacilityStatuses");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "FacilityStatuses");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "FacilityStatuses");

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

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "ComplianceOfficers");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "ComplianceOfficers");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "ComplianceOfficers");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "ComplianceOfficers");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "Cabinets");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "Cabinets");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "Cabinets");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "Cabinets");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "CabinetFileJoin");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "CabinetFileJoin");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "CabinetFileJoin");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "CabinetFileJoin");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "BudgetCodes");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "BudgetCodes");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "BudgetCodes");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "BudgetCodes");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "AppUserTokens");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "AppUserTokens");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "AppUserTokens");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "AppUserTokens");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "AppUserRoles");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "AppUserRoles");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "AppUserRoles");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "AppUserRoles");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "AppUserLogins");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "AppUserLogins");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "AppUserLogins");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "AppUserLogins");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "AppUserClaims");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "AppUserClaims");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "AppUserClaims");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "AppUserClaims");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "AppRoles");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "AppRoles");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "AppRoles");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "AppRoles");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "AppRoleClaims");

            migrationBuilder.DropColumn(
                name: "InsertUser",
                table: "AppRoleClaims");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "AppRoleClaims");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "AppRoleClaims");
        }
    }
}
