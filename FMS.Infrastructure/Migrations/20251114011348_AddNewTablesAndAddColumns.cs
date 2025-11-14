using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTablesAndAddColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_ActionsTaken_ActionTakenId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_ComplianceOfficers_ComplianceOfficerId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventTypes_EventTypeId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_GroundwaterScores_Chemicals_ChemicalId",
                table: "GroundwaterScores");

            migrationBuilder.DropForeignKey(
                name: "FK_OnsiteScores_Chemicals_ChemicalId",
                table: "OnsiteScores");

            migrationBuilder.DropColumn(
                name: "EntityNameOrNumber",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "ChemicalId",
                table: "OnsiteScores",
                newName: "SubstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_OnsiteScores_ChemicalId",
                table: "OnsiteScores",
                newName: "IX_OnsiteScores_SubstanceId");

            migrationBuilder.RenameColumn(
                name: "ChemicalId",
                table: "GroundwaterScores",
                newName: "SubstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_GroundwaterScores_ChemicalId",
                table: "GroundwaterScores",
                newName: "IX_GroundwaterScores_SubstanceId");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventTypeId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ComplianceOfficerId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ActionTakenId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EventContractorId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventContractors",
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
                    table.PrimaryKey("PK_EventContractors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventContractorId",
                table: "Events",
                column: "EventContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_ActionsTaken_ActionTakenId",
                table: "Events",
                column: "ActionTakenId",
                principalTable: "ActionsTaken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_ComplianceOfficers_ComplianceOfficerId",
                table: "Events",
                column: "ComplianceOfficerId",
                principalTable: "ComplianceOfficers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventContractors_EventContractorId",
                table: "Events",
                column: "EventContractorId",
                principalTable: "EventContractors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventTypes_EventTypeId",
                table: "Events",
                column: "EventTypeId",
                principalTable: "EventTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroundwaterScores_Substances_SubstanceId",
                table: "GroundwaterScores",
                column: "SubstanceId",
                principalTable: "Substances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OnsiteScores_Substances_SubstanceId",
                table: "OnsiteScores",
                column: "SubstanceId",
                principalTable: "Substances",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_ActionsTaken_ActionTakenId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_ComplianceOfficers_ComplianceOfficerId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventContractors_EventContractorId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventTypes_EventTypeId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_GroundwaterScores_Substances_SubstanceId",
                table: "GroundwaterScores");

            migrationBuilder.DropForeignKey(
                name: "FK_OnsiteScores_Substances_SubstanceId",
                table: "OnsiteScores");

            migrationBuilder.DropTable(
                name: "EventContractors");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventContractorId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventContractorId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "SubstanceId",
                table: "OnsiteScores",
                newName: "ChemicalId");

            migrationBuilder.RenameIndex(
                name: "IX_OnsiteScores_SubstanceId",
                table: "OnsiteScores",
                newName: "IX_OnsiteScores_ChemicalId");

            migrationBuilder.RenameColumn(
                name: "SubstanceId",
                table: "GroundwaterScores",
                newName: "ChemicalId");

            migrationBuilder.RenameIndex(
                name: "IX_GroundwaterScores_SubstanceId",
                table: "GroundwaterScores",
                newName: "IX_GroundwaterScores_ChemicalId");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventTypeId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ComplianceOfficerId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ActionTakenId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "EntityNameOrNumber",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_ActionsTaken_ActionTakenId",
                table: "Events",
                column: "ActionTakenId",
                principalTable: "ActionsTaken",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_ComplianceOfficers_ComplianceOfficerId",
                table: "Events",
                column: "ComplianceOfficerId",
                principalTable: "ComplianceOfficers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventTypes_EventTypeId",
                table: "Events",
                column: "EventTypeId",
                principalTable: "EventTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroundwaterScores_Chemicals_ChemicalId",
                table: "GroundwaterScores",
                column: "ChemicalId",
                principalTable: "Chemicals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OnsiteScores_Chemicals_ChemicalId",
                table: "OnsiteScores",
                column: "ChemicalId",
                principalTable: "Chemicals",
                principalColumn: "Id");
        }
    }
}
