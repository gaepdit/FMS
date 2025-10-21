using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DBCorrections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FundingSources",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "FundingSources");
        }
    }
}
