using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhaseIIITables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionsTaken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionsTaken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chemicals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CasNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChemicalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToxValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MCLs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chemicals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactTitles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FundingSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroundwaterScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GWScore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A = table.Column<int>(type: "int", nullable: false),
                    B1 = table.Column<int>(type: "int", nullable: false),
                    B2 = table.Column<int>(type: "int", nullable: false),
                    C = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    D2 = table.Column<int>(type: "int", nullable: false),
                    D3 = table.Column<int>(type: "int", nullable: false),
                    ChemicalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CASNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    E1 = table.Column<int>(type: "int", nullable: false),
                    E2 = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroundwaterScores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroundwaterStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroundwaterStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HsrpFacilityProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateListed = table.Column<DateOnly>(type: "date", nullable: false),
                    AdditionalOrgUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Geologist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VRPDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BrownfieldDate = table.Column<DateOnly>(type: "date", nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HsrpFacilityProperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OnsiteScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OnsiteScoreValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A = table.Column<int>(type: "int", nullable: false),
                    B = table.Column<int>(type: "int", nullable: false),
                    C = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChemName1D = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other1D = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    D2 = table.Column<int>(type: "int", nullable: false),
                    D3 = table.Column<int>(type: "int", nullable: false),
                    ChemicalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CASNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    E1 = table.Column<int>(type: "int", nullable: false),
                    E2 = table.Column<int>(type: "int", nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnsiteScores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OverallStatuses",
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
                    table.PrimaryKey("PK_OverallStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParcelTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PhoneType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScoredDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ScoredById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UseComments = table.Column<bool>(type: "bit", nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scores_ComplianceOfficers_ScoredById",
                        column: x => x.ScoredById,
                        principalTable: "ComplianceOfficers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SoilStatuses",
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
                    table.PrimaryKey("PK_SoilStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SourceStatuses",
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
                    table.PrimaryKey("PK_SourceStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Substances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChemicalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Groundwater = table.Column<bool>(type: "bit", nullable: false),
                    Soil = table.Column<bool>(type: "bit", nullable: false),
                    UseForScoring = table.Column<bool>(type: "bit", nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Substances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Substances_Chemicals_ChemicalId",
                        column: x => x.ChemicalId,
                        principalTable: "Chemicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GivenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactTitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactTitles_ContactTitleId",
                        column: x => x.ContactTitleId,
                        principalTable: "ContactTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactTypes_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalTable: "ContactTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllowedActionsTaken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionTakenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowedActionsTaken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllowedActionsTaken_ActionsTaken_ActionTakenId",
                        column: x => x.ActionTakenId,
                        principalTable: "ActionsTaken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllowedActionsTaken_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EventTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ActionTakenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CompletionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ComplianceOfficerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EventAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EntityNameOrNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_ActionsTaken_ActionTakenId",
                        column: x => x.ActionTakenId,
                        principalTable: "ActionsTaken",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_ComplianceOfficers_ComplianceOfficerId",
                        column: x => x.ComplianceOfficerId,
                        principalTable: "ComplianceOfficers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParcelNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParcelDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParcelTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Acres = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcels_ParcelTypes_ParcelTypeId",
                        column: x => x.ParcelTypeId,
                        principalTable: "ParcelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SourceProjected = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoilStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoilDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SoilProjected = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroundwaterStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroundwaterDate = table.Column<DateOnly>(type: "date", nullable: true),
                    GroundwaterHWTF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverallStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OverallDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ISWQS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FundingSourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LandFill = table.Column<bool>(type: "bit", nullable: false),
                    SolidWastePermitNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HSPMScore = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lien = table.Column<bool>(type: "bit", nullable: false),
                    FinancialAssurance = table.Column<bool>(type: "bit", nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InsertUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statuses_FundingSources_FundingSourceId",
                        column: x => x.FundingSourceId,
                        principalTable: "FundingSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statuses_GroundwaterStatuses_GroundwaterStatusId",
                        column: x => x.GroundwaterStatusId,
                        principalTable: "GroundwaterStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statuses_OverallStatuses_OverallStatusId",
                        column: x => x.OverallStatusId,
                        principalTable: "OverallStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statuses_SoilStatuses_SoilStatusId",
                        column: x => x.SoilStatusId,
                        principalTable: "SoilStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statuses_SourceStatuses_SourceStatusId",
                        column: x => x.SourceStatusId,
                        principalTable: "SourceStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllowedActionsTaken_ActionTakenId",
                table: "AllowedActionsTaken",
                column: "ActionTakenId");

            migrationBuilder.CreateIndex(
                name: "IX_AllowedActionsTaken_EventTypeId",
                table: "AllowedActionsTaken",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactTitleId",
                table: "Contacts",
                column: "ContactTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactTypeId",
                table: "Contacts",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ActionTakenId",
                table: "Events",
                column: "ActionTakenId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ComplianceOfficerId",
                table: "Events",
                column: "ComplianceOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_ParcelTypeId",
                table: "Parcels",
                column: "ParcelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_ScoredById",
                table: "Scores",
                column: "ScoredById");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_FundingSourceId",
                table: "Statuses",
                column: "FundingSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_GroundwaterStatusId",
                table: "Statuses",
                column: "GroundwaterStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_OverallStatusId",
                table: "Statuses",
                column: "OverallStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_SoilStatusId",
                table: "Statuses",
                column: "SoilStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_SourceStatusId",
                table: "Statuses",
                column: "SourceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Substances_ChemicalId",
                table: "Substances",
                column: "ChemicalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllowedActionsTaken");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "GroundwaterScores");

            migrationBuilder.DropTable(
                name: "HsrpFacilityProperties");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "OnsiteScores");

            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Substances");

            migrationBuilder.DropTable(
                name: "ContactTitles");

            migrationBuilder.DropTable(
                name: "ContactTypes");

            migrationBuilder.DropTable(
                name: "ActionsTaken");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "ParcelTypes");

            migrationBuilder.DropTable(
                name: "FundingSources");

            migrationBuilder.DropTable(
                name: "GroundwaterStatuses");

            migrationBuilder.DropTable(
                name: "OverallStatuses");

            migrationBuilder.DropTable(
                name: "SoilStatuses");

            migrationBuilder.DropTable(
                name: "SourceStatuses");

            migrationBuilder.DropTable(
                name: "Chemicals");
        }
    }
}
