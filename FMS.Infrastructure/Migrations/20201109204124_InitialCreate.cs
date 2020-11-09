using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    GivenName = table.Column<string>(maxLength: 150, nullable: true),
                    FamilyName = table.Column<string>(maxLength: 150, nullable: true),
                    SubjectId = table.Column<string>(nullable: true),
                    ObjectId = table.Column<string>(nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BudgetCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    OrganizationNumber = table.Column<string>(maxLength: 20, nullable: true),
                    ProjectNumber = table.Column<string>(maxLength: 20, nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cabinets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FirstFileLabel = table.Column<string>(maxLength: 9, nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabinets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComplianceOfficers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    GivenName = table.Column<string>(nullable: true),
                    FamilyName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceOfficers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FacilityStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FacilityTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
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
                    FileLabel = table.Column<string>(maxLength: 9, nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
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
                    Name = table.Column<string>(nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRoleClaims_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserClaims_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AppUserLogins_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AppUserRoles_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserRoles_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AppUserTokens_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CabinetFileJoin",
                columns: table => new
                {
                    CabinetId = table.Column<Guid>(nullable: false),
                    FileId = table.Column<Guid>(nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    FacilityNumber = table.Column<string>(nullable: true),
                    FileId = table.Column<Guid>(nullable: false),
                    FacilityTypeId = table.Column<Guid>(nullable: true),
                    OrganizationalUnitId = table.Column<Guid>(nullable: true),
                    BudgetCodeId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ComplianceOfficerId = table.Column<Guid>(nullable: true),
                    FacilityStatusId = table.Column<Guid>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 30, nullable: true),
                    State = table.Column<string>(maxLength: 20, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(8, 6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9, 6)", nullable: false),
                    CountyId = table.Column<int>(nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facilities_BudgetCodes_BudgetCodeId",
                        column: x => x.BudgetCodeId,
                        principalTable: "BudgetCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facilities_ComplianceOfficers_ComplianceOfficerId",
                        column: x => x.ComplianceOfficerId,
                        principalTable: "ComplianceOfficers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facilities_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facilities_FacilityStatuses_FacilityStatusId",
                        column: x => x.FacilityStatusId,
                        principalTable: "FacilityStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facilities_FacilityTypes_FacilityTypeId",
                        column: x => x.FacilityTypeId,
                        principalTable: "FacilityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetentionRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    FacilityId = table.Column<Guid>(nullable: false),
                    StartYear = table.Column<int>(nullable: false),
                    EndYear = table.Column<int>(nullable: false),
                    ConsignmentNumber = table.Column<string>(nullable: true),
                    BoxNumber = table.Column<string>(nullable: true),
                    ShelfNumber = table.Column<string>(nullable: true),
                    RetentionSchedule = table.Column<string>(nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: true),
                    InsertUser = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetentionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetentionRecords_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Counties",
                columns: new[] { "Id", "InsertDateTime", "InsertUser", "Name", "UpdateDateTime", "UpdateUser" },
                values: new object[,]
                {
                    { 131, null, null, "Appling", null, null },
                    { 134, null, null, "Montgomery", null, null },
                    { 197, null, null, "Morgan", null, null },
                    { 205, null, null, "Murray", null, null },
                    { 258, null, null, "Muscogee", null, null },
                    { 231, null, null, "Newton", null, null },
                    { 191, null, null, "Oconee", null, null },
                    { 127, null, null, "Oglethorpe", null, null },
                    { 184, null, null, "Monroe", null, null },
                    { 219, null, null, "Paulding", null, null },
                    { 185, null, null, "Pickens", null, null },
                    { 116, null, null, "Pierce", null, null },
                    { 149, null, null, "Pike", null, null },
                    { 233, null, null, "Polk", null, null },
                    { 155, null, null, "Pulaski", null, null },
                    { 183, null, null, "Putnam", null, null },
                    { 100, null, null, "Quitman", null, null },
                    { 221, null, null, "Peach", null, null },
                    { 167, null, null, "Rabun", null, null },
                    { 215, null, null, "Mitchell", null, null },
                    { 196, null, null, "Meriwether", null, null },
                    { 140, null, null, "Johnson", null, null },
                    { 161, null, null, "Jones", null, null },
                    { 181, null, null, "Lamar", null, null },
                    { 141, null, null, "Lanier", null, null },
                    { 223, null, null, "Laurens", null, null },
                    { 172, null, null, "Lee", null, null },
                    { 148, null, null, "Liberty", null, null },
                    { 154, null, null, "Miller", null, null },
                    { 125, null, null, "Lincoln", null, null },
                    { 254, null, null, "Lowndes", null, null },
                    { 192, null, null, "Lumpkin", null, null },
                    { 182, null, null, "Macon", null, null },
                    { 166, null, null, "Madison", null, null },
                    { 133, null, null, "Marion", null, null },
                    { 210, null, null, "McDuffie", null, null },
                    { 126, null, null, "McIntosh", null, null },
                    { 102, null, null, "Long", null, null },
                    { 153, null, null, "Jenkins", null, null },
                    { 156, null, null, "Randolph", null, null },
                    { 251, null, null, "Rockdale", null, null },
                    { 130, null, null, "Twiggs", null, null },
                    { 168, null, null, "Union", null, null },
                    { 202, null, null, "Upson", null, null },
                    { 224, null, null, "Walker", null, null },
                    { 228, null, null, "Walton", null, null },
                    { 177, null, null, "Ware", null, null },
                    { 162, null, null, "Warren", null, null },
                    { 186, null, null, "Turner", null, null },
                    { 206, null, null, "Washington", null, null },
                    { 120, null, null, "Webster", null, null },
                    { 106, null, null, "Wheeler", null, null },
                    { 193, null, null, "White", null, null },
                    { 255, null, null, "Whitfield", null, null },
                    { 144, null, null, "Wilcox", null, null },
                    { 163, null, null, "Wilkes", null, null },
                    { 207, null, null, "Wilkinson", null, null },
                    { 143, null, null, "Wayne", null, null },
                    { 260, null, null, "Richmond", null, null },
                    { 247, null, null, "Troup", null, null },
                    { 129, null, null, "Towns", null, null },
                    { 135, null, null, "Schley", null, null },
                    { 157, null, null, "Screven", null, null },
                    { 150, null, null, "Seminole", null, null },
                    { 240, null, null, "Spalding", null, null },
                    { 229, null, null, "Stephens", null, null },
                    { 117, null, null, "Stewart", null, null },
                    { 236, null, null, "Sumter", null, null },
                    { 101, null, null, "Treutlen", null, null },
                    { 128, null, null, "Talbot", null, null },
                    { 136, null, null, "Tattnall", null, null },
                    { 142, null, null, "Taylor", null, null },
                    { 164, null, null, "Telfair", null, null },
                    { 173, null, null, "Terrell", null, null },
                    { 234, null, null, "Thomas", null, null },
                    { 237, null, null, "Tift", null, null },
                    { 151, null, null, "Toombs", null, null },
                    { 118, null, null, "Taliaferro", null, null },
                    { 199, null, null, "Jefferson", null, null },
                    { 160, null, null, "Jeff Davis", null, null },
                    { 147, null, null, "Jasper", null, null },
                    { 241, null, null, "Carroll", null, null },
                    { 225, null, null, "Catoosa", null, null },
                    { 114, null, null, "Charlton", null, null },
                    { 242, null, null, "Chatham", null, null },
                    { 137, null, null, "Chattahoochee", null, null },
                    { 174, null, null, "Chattooga", null, null },
                    { 243, null, null, "Cherokee", null, null },
                    { 113, null, null, "Candler", null, null },
                    { 252, null, null, "Clarke", null, null },
                    { 204, null, null, "Clayton", null, null },
                    { 178, null, null, "Clinch", null, null },
                    { 245, null, null, "Cobb", null, null },
                    { 146, null, null, "Coffee", null, null },
                    { 227, null, null, "Colquitt", null, null },
                    { 232, null, null, "Columbia", null, null },
                    { 212, null, null, "Cook", null, null },
                    { 104, null, null, "Clay", null, null },
                    { 250, null, null, "Coweta", null, null },
                    { 169, null, null, "Camden", null, null },
                    { 188, null, null, "Butts", null, null },
                    { 122, null, null, "Atkinson", null, null },
                    { 110, null, null, "Bacon", null, null },
                    { 105, null, null, "Baker", null, null },
                    { 213, null, null, "Baldwin", null, null },
                    { 179, null, null, "Banks", null, null },
                    { 217, null, null, "Barrow", null, null },
                    { 249, null, null, "Bartow", null, null },
                    { 112, null, null, "Calhoun", null, null },
                    { 211, null, null, "Ben Hill", null, null },
                    { 259, null, null, "Bibb", null, null },
                    { 145, null, null, "Bleckley", null, null },
                    { 108, null, null, "Brantley", null, null },
                    { 158, null, null, "Brooks", null, null },
                    { 111, null, null, "Bryan", null, null },
                    { 159, null, null, "Bulloch", null, null },
                    { 203, null, null, "Burke", null, null },
                    { 194, null, null, "Berrien", null, null },
                    { 115, null, null, "Crawford", null, null },
                    { 220, null, null, "Crisp", null, null },
                    { 170, null, null, "Dade", null, null },
                    { 216, null, null, "Glynn", null, null },
                    { 230, null, null, "Gordon", null, null },
                    { 208, null, null, "Grady", null, null },
                    { 171, null, null, "Greene", null, null },
                    { 226, null, null, "Gwinnett", null, null },
                    { 214, null, null, "Habersham", null, null },
                    { 256, null, null, "Hall", null, null },
                    { 124, null, null, "Glascock", null, null },
                    { 107, null, null, "Hancock", null, null },
                    { 138, null, null, "Harris", null, null },
                    { 190, null, null, "Hart", null, null },
                    { 152, null, null, "Heard", null, null },
                    { 248, null, null, "Henry", null, null },
                    { 239, null, null, "Houston", null, null },
                    { 139, null, null, "Irwin", null, null },
                    { 222, null, null, "Jackson", null, null },
                    { 209, null, null, "Haralson", null, null },
                    { 165, null, null, "Gilmer", null, null },
                    { 261, null, null, "Fulton", null, null },
                    { 201, null, null, "Franklin", null, null },
                    { 180, null, null, "Dawson", null, null },
                    { 244, null, null, "Decatur", null, null },
                    { 218, null, null, "DeKalb", null, null },
                    { 175, null, null, "Dodge", null, null },
                    { 189, null, null, "Dooly", null, null },
                    { 257, null, null, "Dougherty", null, null },
                    { 253, null, null, "Douglas", null, null },
                    { 195, null, null, "Early", null, null },
                    { 103, null, null, "Echols", null, null },
                    { 123, null, null, "Effingham", null, null },
                    { 200, null, null, "Elbert", null, null },
                    { 198, null, null, "Emanuel", null, null },
                    { 109, null, null, "Evans", null, null },
                    { 176, null, null, "Fannin", null, null },
                    { 235, null, null, "Fayette", null, null },
                    { 246, null, null, "Floyd", null, null },
                    { 238, null, null, "Forsyth", null, null },
                    { 187, null, null, "Worth", null, null },
                    { 99, null, null, "OUT OF STATE", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRoleClaims_RoleId",
                table: "AppRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AppRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserClaims_UserId",
                table: "AppUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLogins_UserId",
                table: "AppUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_RoleId",
                table: "AppUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AppUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AppUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CabinetFileJoin_FileId",
                table: "CabinetFileJoin",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Cabinets_Name",
                table: "Cabinets",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

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
                name: "IX_FacilityTypes_Name",
                table: "FacilityTypes",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileLabel",
                table: "Files",
                column: "FileLabel",
                unique: true,
                filter: "[FileLabel] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RetentionRecords_FacilityId",
                table: "RetentionRecords",
                column: "FacilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "CabinetFileJoin");

            migrationBuilder.DropTable(
                name: "RetentionRecords");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Cabinets");

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
        }
    }
}
