using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentalInterests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalInterests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FacilityTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    FileLabel = table.Column<string>(maxLength: 9, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationalUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BudgetCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    EnvironmentalInterestId = table.Column<Guid>(nullable: true),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OrganizationNumber = table.Column<string>(maxLength: 20, nullable: true),
                    ProjectNumber = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetCodes_EnvironmentalInterests_EnvironmentalInterestId",
                        column: x => x.EnvironmentalInterestId,
                        principalTable: "EnvironmentalInterests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacilityStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    EnvironmentalInterestId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacilityStatuses_EnvironmentalInterests_EnvironmentalInterestId",
                        column: x => x.EnvironmentalInterestId,
                        principalTable: "EnvironmentalInterests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileCabinets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 5, nullable: true),
                    StartCountyId = table.Column<int>(nullable: true),
                    EndCountyId = table.Column<int>(nullable: true),
                    StartSequence = table.Column<int>(nullable: false),
                    EndSequence = table.Column<int>(nullable: false),
                    FileId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileCabinets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileCabinets_Counties_EndCountyId",
                        column: x => x.EndCountyId,
                        principalTable: "Counties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileCabinets_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileCabinets_Counties_StartCountyId",
                        column: x => x.StartCountyId,
                        principalTable: "Counties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComplianceOfficers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UnitId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceOfficers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplianceOfficers_OrganizationalUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "OrganizationalUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    FacilityNumber = table.Column<string>(nullable: true),
                    FileId = table.Column<Guid>(nullable: false),
                    EnvironmentalInterestId = table.Column<Guid>(nullable: false),
                    FacilityTypeId = table.Column<Guid>(nullable: false),
                    OrganizationalUnitId = table.Column<Guid>(nullable: false),
                    BudgetCodeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ComplianceOfficerId = table.Column<Guid>(nullable: false),
                    FacilityStatusId = table.Column<Guid>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 30, nullable: true),
                    State = table.Column<string>(maxLength: 2, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    CountyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facilities_BudgetCodes_BudgetCodeId",
                        column: x => x.BudgetCodeId,
                        principalTable: "BudgetCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facilities_ComplianceOfficers_ComplianceOfficerId",
                        column: x => x.ComplianceOfficerId,
                        principalTable: "ComplianceOfficers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facilities_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facilities_EnvironmentalInterests_EnvironmentalInterestId",
                        column: x => x.EnvironmentalInterestId,
                        principalTable: "EnvironmentalInterests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facilities_FacilityStatuses_FacilityStatusId",
                        column: x => x.FacilityStatusId,
                        principalTable: "FacilityStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facilities_FacilityTypes_FacilityTypeId",
                        column: x => x.FacilityTypeId,
                        principalTable: "FacilityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facilities_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facilities_OrganizationalUnits_OrganizationalUnitId",
                        column: x => x.OrganizationalUnitId,
                        principalTable: "OrganizationalUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetentionRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    FacilityId = table.Column<Guid>(nullable: true),
                    StartYear = table.Column<int>(nullable: false),
                    EndYear = table.Column<int>(nullable: false),
                    ConsignmentNumber = table.Column<string>(nullable: true),
                    BoxNumber = table.Column<string>(nullable: true),
                    ShelfNumber = table.Column<string>(nullable: true),
                    RetentionSchedule = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetentionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetentionRecords_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Counties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 131, "Appling" },
                    { 134, "Montgomery" },
                    { 197, "Morgan" },
                    { 205, "Murray" },
                    { 258, "Muscogee" },
                    { 231, "Newton" },
                    { 191, "Oconee" },
                    { 127, "Oglethorpe" },
                    { 184, "Monroe" },
                    { 219, "Paulding" },
                    { 185, "Pickens" },
                    { 116, "Pierce" },
                    { 149, "Pike" },
                    { 233, "Polk" },
                    { 155, "Pulaski" },
                    { 183, "Putnam" },
                    { 100, "Quitman" },
                    { 221, "Peach" },
                    { 167, "Rabun" },
                    { 215, "Mitchell" },
                    { 196, "Meriwether" },
                    { 140, "Johnson" },
                    { 161, "Jones" },
                    { 181, "Lamar" },
                    { 141, "Lanier" },
                    { 223, "Laurens" },
                    { 172, "Lee" },
                    { 148, "Liberty" },
                    { 154, "Miller" },
                    { 125, "Lincoln" },
                    { 254, "Lowndes" },
                    { 192, "Lumpkin" },
                    { 182, "Macon" },
                    { 166, "Madison" },
                    { 133, "Marion" },
                    { 210, "McDuffie" },
                    { 126, "McIntosh" },
                    { 102, "Long" },
                    { 153, "Jenkins" },
                    { 156, "Randolph" },
                    { 251, "Rockdale" },
                    { 130, "Twiggs" },
                    { 168, "Union" },
                    { 202, "Upson" },
                    { 224, "Walker" },
                    { 228, "Walton" },
                    { 177, "Ware" },
                    { 162, "Warren" },
                    { 186, "Turner" },
                    { 206, "Washington" },
                    { 120, "Webster" },
                    { 106, "Wheeler" },
                    { 193, "White" },
                    { 255, "Whitfield" },
                    { 144, "Wilcox" },
                    { 163, "Wilkes" },
                    { 207, "Wilkinson" },
                    { 143, "Wayne" },
                    { 260, "Richmond" },
                    { 247, "Troup" },
                    { 129, "Towns" },
                    { 135, "Schley" },
                    { 157, "Screven" },
                    { 150, "Seminole" },
                    { 240, "Spalding" },
                    { 229, "Stephens" },
                    { 117, "Stewart" },
                    { 236, "Sumter" },
                    { 101, "Treutlen" },
                    { 128, "Talbot" },
                    { 136, "Tattnall" },
                    { 142, "Taylor" },
                    { 164, "Telfair" },
                    { 173, "Terrell" },
                    { 234, "Thomas" },
                    { 237, "Tift" },
                    { 151, "Toombs" },
                    { 118, "Taliaferro" },
                    { 199, "Jefferson" },
                    { 160, "Jeff Davis" },
                    { 147, "Jasper" },
                    { 241, "Carroll" },
                    { 225, "Catoosa" },
                    { 114, "Charlton" },
                    { 242, "Chatham" },
                    { 137, "Chattahoochee" },
                    { 174, "Chattooga" },
                    { 243, "Cherokee" },
                    { 113, "Candler" },
                    { 252, "Clarke" },
                    { 204, "Clayton" },
                    { 178, "Clinch" },
                    { 245, "Cobb" },
                    { 146, "Coffee" },
                    { 227, "Colquitt" },
                    { 232, "Columbia" },
                    { 212, "Cook" },
                    { 104, "Clay" },
                    { 250, "Coweta" },
                    { 169, "Camden" },
                    { 188, "Butts" },
                    { 122, "Atkinson" },
                    { 110, "Bacon" },
                    { 105, "Baker" },
                    { 213, "Baldwin" },
                    { 179, "Banks" },
                    { 217, "Barrow" },
                    { 249, "Bartow" },
                    { 112, "Calhoun" },
                    { 211, "Ben Hill" },
                    { 259, "Bibb" },
                    { 145, "Bleckley" },
                    { 108, "Brantley" },
                    { 158, "Brooks" },
                    { 111, "Bryan" },
                    { 159, "Bulloch" },
                    { 203, "Burke" },
                    { 194, "Berrien" },
                    { 115, "Crawford" },
                    { 220, "Crisp" },
                    { 170, "Dade" },
                    { 216, "Glynn" },
                    { 230, "Gordon" },
                    { 208, "Grady" },
                    { 171, "Greene" },
                    { 226, "Gwinnett" },
                    { 214, "Habersham" },
                    { 256, "Hall" },
                    { 124, "Glascock" },
                    { 107, "Hancock" },
                    { 138, "Harris" },
                    { 190, "Hart" },
                    { 152, "Heard" },
                    { 248, "Henry" },
                    { 239, "Houston" },
                    { 139, "Irwin" },
                    { 222, "Jackson" },
                    { 209, "Haralson" },
                    { 165, "Gilmer" },
                    { 261, "Fulton" },
                    { 201, "Franklin" },
                    { 180, "Dawson" },
                    { 244, "Decatur" },
                    { 218, "DeKalb" },
                    { 175, "Dodge" },
                    { 189, "Dooly" },
                    { 257, "Dougherty" },
                    { 253, "Douglas" },
                    { 195, "Early" },
                    { 103, "Echols" },
                    { 123, "Effingham" },
                    { 200, "Elbert" },
                    { 198, "Emanuel" },
                    { 109, "Evans" },
                    { 176, "Fannin" },
                    { 235, "Fayette" },
                    { 246, "Floyd" },
                    { 238, "Forsyth" },
                    { 187, "Worth" },
                    { 99, "OUT OF STATE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCodes_EnvironmentalInterestId",
                table: "BudgetCodes",
                column: "EnvironmentalInterestId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplianceOfficers_UnitId",
                table: "ComplianceOfficers",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_BudgetCodeId",
                table: "Facilities",
                column: "BudgetCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_ComplianceOfficerId",
                table: "Facilities",
                column: "ComplianceOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_CountyId",
                table: "Facilities",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_EnvironmentalInterestId",
                table: "Facilities",
                column: "EnvironmentalInterestId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_FacilityStatusId",
                table: "Facilities",
                column: "FacilityStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_FacilityTypeId",
                table: "Facilities",
                column: "FacilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_FileId",
                table: "Facilities",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_OrganizationalUnitId",
                table: "Facilities",
                column: "OrganizationalUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityStatuses_EnvironmentalInterestId",
                table: "FacilityStatuses",
                column: "EnvironmentalInterestId");

            migrationBuilder.CreateIndex(
                name: "IX_FileCabinets_EndCountyId",
                table: "FileCabinets",
                column: "EndCountyId");

            migrationBuilder.CreateIndex(
                name: "IX_FileCabinets_FileId",
                table: "FileCabinets",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileCabinets_StartCountyId",
                table: "FileCabinets",
                column: "StartCountyId");

            migrationBuilder.CreateIndex(
                name: "IX_RetentionRecords_FacilityId",
                table: "RetentionRecords",
                column: "FacilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileCabinets");

            migrationBuilder.DropTable(
                name: "RetentionRecords");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "BudgetCodes");

            migrationBuilder.DropTable(
                name: "ComplianceOfficers");

            migrationBuilder.DropTable(
                name: "Counties");

            migrationBuilder.DropTable(
                name: "FacilityStatuses");

            migrationBuilder.DropTable(
                name: "FacilityTypes");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "OrganizationalUnits");

            migrationBuilder.DropTable(
                name: "EnvironmentalInterests");
        }
    }
}
