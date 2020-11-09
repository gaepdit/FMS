using System;
using FMS.Infrastructure.DbScripts;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class AddSpGetNearbyFacilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacilityList",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FacilityNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    FacilityStatus = table.Column<string>(nullable: true),
                    FacilityType = table.Column<string>(nullable: true),
                    FileLabel = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(8, 6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9, 6)", nullable: false),
                    Distance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityList", x => x.Id);
                });

            migrationBuilder.Sql(StoredProcedures.CreateSpGetNearbyFacilities);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop procedure [dbo].[getNearbyFacilities]");
            
            migrationBuilder.DropTable(
                name: "FacilityList");
        }
    }
}
