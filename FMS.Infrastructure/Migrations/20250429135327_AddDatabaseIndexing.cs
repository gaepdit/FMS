using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabaseIndexing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RetentionRecords_FacilityId",
                table: "RetentionRecords",
                column: "FacilityId")
                .Annotation("SqlServer:Include", new[] { "Active", "StartYear", "EndYear", "ConsignmentNumber", "BoxNumber", "ShelfNumber", "RetentionSchedule" });

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_Active",
                table: "Facilities",
                column: "Active")
                .Annotation("SqlServer:Include", new[] { "FileId" });

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_Active_FacilityTypeId_Address",
                table: "Facilities",
                columns: new[] { "Active", "FacilityTypeId", "Address" });

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_Active_FacilityTypeId_Address_City",
                table: "Facilities",
                columns: new[] { "Active", "FacilityTypeId", "Address", "City" });

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_Active_FileId",
                table: "Facilities",
                columns: new[] { "Active", "FileId" });

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_FileId",
                table: "Facilities",
                column: "FileId")
                .Annotation("SqlServer:Include", new[] { "Active", "FacilityNumber", "FacilityTypeId", "OrganizationalUnitId", "BudgetCodeId", "Name", "ComplianceOfficerId", "FacilityStatusId", "Location", "Address", "City", "State", "PostalCode", "Latitude", "Longitude", "CountyId", "IsRetained", "AdditionalDataRequested", "Comments", "DeferredOnSiteScoring", "DeterminationLetterDate", "HSInumber", "HasERecord", "HistoricalComplianceOfficer", "HistoricalUnit", "ImageChecked", "PreRQSMcleanup", "RNDateReceived", "VRPReferral" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RetentionRecords_FacilityId",
                table: "RetentionRecords");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_Active",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_Active_FacilityTypeId_Address",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_Active_FacilityTypeId_Address_City",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_Active_FileId",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_FileId",
                table: "Facilities");
        }
    }
}
