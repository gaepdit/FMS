using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_ParcelTypes_ParcelTypeId",
                table: "Parcels");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_FundingSources_FundingSourceId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_GroundwaterStatuses_GroundwaterStatusId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_OverallStatuses_OverallStatusId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_SoilStatuses_SoilStatusId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_SourceStatuses_SourceStatusId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "GroundwaterHWTF",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "SoilProjected",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "SourceProjected",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "AdditionalOrgUnit",
                table: "HsrpFacilityProperties");

            migrationBuilder.DropColumn(
                name: "Geologist",
                table: "HsrpFacilityProperties");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "HSPMScore",
                table: "Statuses",
                newName: "GAPSScore");

            migrationBuilder.RenameColumn(
                name: "ParcelDescription",
                table: "Parcels",
                newName: "SubListParcelName");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Parcels",
                newName: "FacilityId");

            migrationBuilder.RenameColumn(
                name: "ScoreId",
                table: "OnsiteScores",
                newName: "FacilityId");

            migrationBuilder.RenameColumn(
                name: "ScoreId",
                table: "GroundwaterScores",
                newName: "FacilityId");

            migrationBuilder.AlterColumn<Guid>(
                name: "SourceStatusId",
                table: "Statuses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "SoilStatusId",
                table: "Statuses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "OverallStatusId",
                table: "Statuses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "ISWQS",
                table: "Statuses",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GroundwaterStatusId",
                table: "Statuses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "FundingSourceId",
                table: "Statuses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AbandonSitesId",
                table: "Statuses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CostEstimate",
                table: "Statuses",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "CostEstimateDate",
                table: "Statuses",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GAPSAssessmentId",
                table: "Statuses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "GAPSModelDate",
                table: "Statuses",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GAPSNoOfUnknowns",
                table: "Statuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ScoredDate",
                table: "Scores",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<Guid>(
                name: "FacilityId",
                table: "Phones",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ParcelTypeId",
                table: "Parcels",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DeListDate",
                table: "Parcels",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ListDate",
                table: "Parcels",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChemicalId",
                table: "OnsiteScores",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "VRPDate",
                table: "HsrpFacilityProperties",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateListed",
                table: "HsrpFacilityProperties",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BrownfieldDate",
                table: "HsrpFacilityProperties",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<Guid>(
                name: "ComplianceOfficerId",
                table: "HsrpFacilityProperties",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateDeListed",
                table: "HsrpFacilityProperties",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationalUnitId",
                table: "HsrpFacilityProperties",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChemicalId",
                table: "GroundwaterScores",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "Events",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DueDate",
                table: "Events",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CompletionDate",
                table: "Events",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "AbandonSites",
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
                    table.PrimaryKey("PK_AbandonSites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GapsAssessments",
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
                    table.PrimaryKey("PK_GapsAssessments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Substances_FacilityId",
                table: "Substances",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_AbandonSitesId",
                table: "Statuses",
                column: "AbandonSitesId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_FacilityId",
                table: "Statuses",
                column: "FacilityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_GAPSAssessmentId",
                table: "Statuses",
                column: "GAPSAssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_FacilityId",
                table: "Scores",
                column: "FacilityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phones_ContactId",
                table: "Phones",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_FacilityId",
                table: "Phones",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_FacilityId",
                table: "Parcels",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_OnsiteScores_ChemicalId",
                table: "OnsiteScores",
                column: "ChemicalId");

            migrationBuilder.CreateIndex(
                name: "IX_OnsiteScores_FacilityId",
                table: "OnsiteScores",
                column: "FacilityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_FacilityId",
                table: "Locations",
                column: "FacilityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HsrpFacilityProperties_ComplianceOfficerId",
                table: "HsrpFacilityProperties",
                column: "ComplianceOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_HsrpFacilityProperties_FacilityId",
                table: "HsrpFacilityProperties",
                column: "FacilityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HsrpFacilityProperties_OrganizationalUnitId",
                table: "HsrpFacilityProperties",
                column: "OrganizationalUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_GroundwaterScores_ChemicalId",
                table: "GroundwaterScores",
                column: "ChemicalId");

            migrationBuilder.CreateIndex(
                name: "IX_GroundwaterScores_FacilityId",
                table: "GroundwaterScores",
                column: "FacilityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_FacilityId",
                table: "Events",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_FacilityId",
                table: "Contacts",
                column: "FacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Facilities_FacilityId",
                table: "Contacts",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Facilities_FacilityId",
                table: "Events",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroundwaterScores_Chemicals_ChemicalId",
                table: "GroundwaterScores",
                column: "ChemicalId",
                principalTable: "Chemicals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroundwaterScores_Facilities_FacilityId",
                table: "GroundwaterScores",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HsrpFacilityProperties_ComplianceOfficers_ComplianceOfficerId",
                table: "HsrpFacilityProperties",
                column: "ComplianceOfficerId",
                principalTable: "ComplianceOfficers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HsrpFacilityProperties_Facilities_FacilityId",
                table: "HsrpFacilityProperties",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HsrpFacilityProperties_OrganizationalUnits_OrganizationalUnitId",
                table: "HsrpFacilityProperties",
                column: "OrganizationalUnitId",
                principalTable: "OrganizationalUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Facilities_FacilityId",
                table: "Locations",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OnsiteScores_Chemicals_ChemicalId",
                table: "OnsiteScores",
                column: "ChemicalId",
                principalTable: "Chemicals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OnsiteScores_Facilities_FacilityId",
                table: "OnsiteScores",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Facilities_FacilityId",
                table: "Parcels",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_ParcelTypes_ParcelTypeId",
                table: "Parcels",
                column: "ParcelTypeId",
                principalTable: "ParcelTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Contacts_ContactId",
                table: "Phones",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Facilities_FacilityId",
                table: "Phones",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Facilities_FacilityId",
                table: "Scores",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_AbandonSites_AbandonSitesId",
                table: "Statuses",
                column: "AbandonSitesId",
                principalTable: "AbandonSites",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Facilities_FacilityId",
                table: "Statuses",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_FundingSources_FundingSourceId",
                table: "Statuses",
                column: "FundingSourceId",
                principalTable: "FundingSources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_GapsAssessments_GAPSAssessmentId",
                table: "Statuses",
                column: "GAPSAssessmentId",
                principalTable: "GapsAssessments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_GroundwaterStatuses_GroundwaterStatusId",
                table: "Statuses",
                column: "GroundwaterStatusId",
                principalTable: "GroundwaterStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_OverallStatuses_OverallStatusId",
                table: "Statuses",
                column: "OverallStatusId",
                principalTable: "OverallStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_SoilStatuses_SoilStatusId",
                table: "Statuses",
                column: "SoilStatusId",
                principalTable: "SoilStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_SourceStatuses_SourceStatusId",
                table: "Statuses",
                column: "SourceStatusId",
                principalTable: "SourceStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Substances_Facilities_FacilityId",
                table: "Substances",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Facilities_FacilityId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Facilities_FacilityId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_GroundwaterScores_Chemicals_ChemicalId",
                table: "GroundwaterScores");

            migrationBuilder.DropForeignKey(
                name: "FK_GroundwaterScores_Facilities_FacilityId",
                table: "GroundwaterScores");

            migrationBuilder.DropForeignKey(
                name: "FK_HsrpFacilityProperties_ComplianceOfficers_ComplianceOfficerId",
                table: "HsrpFacilityProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_HsrpFacilityProperties_Facilities_FacilityId",
                table: "HsrpFacilityProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_HsrpFacilityProperties_OrganizationalUnits_OrganizationalUnitId",
                table: "HsrpFacilityProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Facilities_FacilityId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_OnsiteScores_Chemicals_ChemicalId",
                table: "OnsiteScores");

            migrationBuilder.DropForeignKey(
                name: "FK_OnsiteScores_Facilities_FacilityId",
                table: "OnsiteScores");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Facilities_FacilityId",
                table: "Parcels");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_ParcelTypes_ParcelTypeId",
                table: "Parcels");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Contacts_ContactId",
                table: "Phones");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Facilities_FacilityId",
                table: "Phones");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Facilities_FacilityId",
                table: "Scores");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_AbandonSites_AbandonSitesId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Facilities_FacilityId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_FundingSources_FundingSourceId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_GapsAssessments_GAPSAssessmentId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_GroundwaterStatuses_GroundwaterStatusId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_OverallStatuses_OverallStatusId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_SoilStatuses_SoilStatusId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_SourceStatuses_SourceStatusId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Substances_Facilities_FacilityId",
                table: "Substances");

            migrationBuilder.DropTable(
                name: "AbandonSites");

            migrationBuilder.DropTable(
                name: "GapsAssessments");

            migrationBuilder.DropIndex(
                name: "IX_Substances_FacilityId",
                table: "Substances");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_AbandonSitesId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_FacilityId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_GAPSAssessmentId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Scores_FacilityId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Phones_ContactId",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Phones_FacilityId",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_FacilityId",
                table: "Parcels");

            migrationBuilder.DropIndex(
                name: "IX_OnsiteScores_ChemicalId",
                table: "OnsiteScores");

            migrationBuilder.DropIndex(
                name: "IX_OnsiteScores_FacilityId",
                table: "OnsiteScores");

            migrationBuilder.DropIndex(
                name: "IX_Locations_FacilityId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_HsrpFacilityProperties_ComplianceOfficerId",
                table: "HsrpFacilityProperties");

            migrationBuilder.DropIndex(
                name: "IX_HsrpFacilityProperties_FacilityId",
                table: "HsrpFacilityProperties");

            migrationBuilder.DropIndex(
                name: "IX_HsrpFacilityProperties_OrganizationalUnitId",
                table: "HsrpFacilityProperties");

            migrationBuilder.DropIndex(
                name: "IX_GroundwaterScores_ChemicalId",
                table: "GroundwaterScores");

            migrationBuilder.DropIndex(
                name: "IX_GroundwaterScores_FacilityId",
                table: "GroundwaterScores");

            migrationBuilder.DropIndex(
                name: "IX_Events_FacilityId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_FacilityId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "AbandonSitesId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "CostEstimate",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "CostEstimateDate",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "GAPSAssessmentId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "GAPSModelDate",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "GAPSNoOfUnknowns",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "DeListDate",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "ListDate",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "ComplianceOfficerId",
                table: "HsrpFacilityProperties");

            migrationBuilder.DropColumn(
                name: "DateDeListed",
                table: "HsrpFacilityProperties");

            migrationBuilder.DropColumn(
                name: "OrganizationalUnitId",
                table: "HsrpFacilityProperties");

            migrationBuilder.RenameColumn(
                name: "GAPSScore",
                table: "Statuses",
                newName: "HSPMScore");

            migrationBuilder.RenameColumn(
                name: "SubListParcelName",
                table: "Parcels",
                newName: "ParcelDescription");

            migrationBuilder.RenameColumn(
                name: "FacilityId",
                table: "Parcels",
                newName: "LocationId");

            migrationBuilder.RenameColumn(
                name: "FacilityId",
                table: "OnsiteScores",
                newName: "ScoreId");

            migrationBuilder.RenameColumn(
                name: "FacilityId",
                table: "GroundwaterScores",
                newName: "ScoreId");

            migrationBuilder.AlterColumn<Guid>(
                name: "SourceStatusId",
                table: "Statuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SoilStatusId",
                table: "Statuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OverallStatusId",
                table: "Statuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ISWQS",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroundwaterStatusId",
                table: "Statuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FundingSourceId",
                table: "Statuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroundwaterHWTF",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoilProjected",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceProjected",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ScoredDate",
                table: "Scores",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ParcelTypeId",
                table: "Parcels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Parcels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Parcels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChemicalId",
                table: "OnsiteScores",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "VRPDate",
                table: "HsrpFacilityProperties",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateListed",
                table: "HsrpFacilityProperties",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BrownfieldDate",
                table: "HsrpFacilityProperties",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalOrgUnit",
                table: "HsrpFacilityProperties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Geologist",
                table: "HsrpFacilityProperties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChemicalId",
                table: "GroundwaterScores",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "Events",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DueDate",
                table: "Events",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CompletionDate",
                table: "Events",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_ParcelTypes_ParcelTypeId",
                table: "Parcels",
                column: "ParcelTypeId",
                principalTable: "ParcelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_FundingSources_FundingSourceId",
                table: "Statuses",
                column: "FundingSourceId",
                principalTable: "FundingSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_GroundwaterStatuses_GroundwaterStatusId",
                table: "Statuses",
                column: "GroundwaterStatusId",
                principalTable: "GroundwaterStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_OverallStatuses_OverallStatusId",
                table: "Statuses",
                column: "OverallStatusId",
                principalTable: "OverallStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_SoilStatuses_SoilStatusId",
                table: "Statuses",
                column: "SoilStatusId",
                principalTable: "SoilStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_SourceStatuses_SourceStatusId",
                table: "Statuses",
                column: "SourceStatusId",
                principalTable: "SourceStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
