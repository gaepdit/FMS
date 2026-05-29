using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToUserProgramAndPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UserPrograms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UserPositions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "UserPrograms");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "UserPositions");
        }
    }
}
