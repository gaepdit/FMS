using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class FixNullableFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_BudgetCodes_BudgetCodeId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_ComplianceOfficers_ComplianceOfficerId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_EnvironmentalInterests_EnvironmentalInterestId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_FacilityStatuses_FacilityStatusId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_FacilityTypes_FacilityTypeId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_OrganizationalUnits_OrganizationalUnitId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_RetentionRecords_Facilities_FacilityId",
                table: "RetentionRecords");

            migrationBuilder.AlterColumn<Guid>(
                name: "FacilityId",
                table: "RetentionRecords",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationalUnitId",
                table: "Facilities",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "FacilityTypeId",
                table: "Facilities",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "FacilityStatusId",
                table: "Facilities",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnvironmentalInterestId",
                table: "Facilities",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ComplianceOfficerId",
                table: "Facilities",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BudgetCodeId",
                table: "Facilities",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_BudgetCodes_BudgetCodeId",
                table: "Facilities",
                column: "BudgetCodeId",
                principalTable: "BudgetCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_ComplianceOfficers_ComplianceOfficerId",
                table: "Facilities",
                column: "ComplianceOfficerId",
                principalTable: "ComplianceOfficers",
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
                name: "FK_Facilities_FacilityStatuses_FacilityStatusId",
                table: "Facilities",
                column: "FacilityStatusId",
                principalTable: "FacilityStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_FacilityTypes_FacilityTypeId",
                table: "Facilities",
                column: "FacilityTypeId",
                principalTable: "FacilityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_OrganizationalUnits_OrganizationalUnitId",
                table: "Facilities",
                column: "OrganizationalUnitId",
                principalTable: "OrganizationalUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RetentionRecords_Facilities_FacilityId",
                table: "RetentionRecords",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_BudgetCodes_BudgetCodeId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_ComplianceOfficers_ComplianceOfficerId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_EnvironmentalInterests_EnvironmentalInterestId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_FacilityStatuses_FacilityStatusId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_FacilityTypes_FacilityTypeId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_OrganizationalUnits_OrganizationalUnitId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_RetentionRecords_Facilities_FacilityId",
                table: "RetentionRecords");

            migrationBuilder.AlterColumn<Guid>(
                name: "FacilityId",
                table: "RetentionRecords",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationalUnitId",
                table: "Facilities",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FacilityTypeId",
                table: "Facilities",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FacilityStatusId",
                table: "Facilities",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EnvironmentalInterestId",
                table: "Facilities",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ComplianceOfficerId",
                table: "Facilities",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BudgetCodeId",
                table: "Facilities",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_BudgetCodes_BudgetCodeId",
                table: "Facilities",
                column: "BudgetCodeId",
                principalTable: "BudgetCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_ComplianceOfficers_ComplianceOfficerId",
                table: "Facilities",
                column: "ComplianceOfficerId",
                principalTable: "ComplianceOfficers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_EnvironmentalInterests_EnvironmentalInterestId",
                table: "Facilities",
                column: "EnvironmentalInterestId",
                principalTable: "EnvironmentalInterests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_FacilityStatuses_FacilityStatusId",
                table: "Facilities",
                column: "FacilityStatusId",
                principalTable: "FacilityStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_FacilityTypes_FacilityTypeId",
                table: "Facilities",
                column: "FacilityTypeId",
                principalTable: "FacilityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_OrganizationalUnits_OrganizationalUnitId",
                table: "Facilities",
                column: "OrganizationalUnitId",
                principalTable: "OrganizationalUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetentionRecords_Facilities_FacilityId",
                table: "RetentionRecords",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
