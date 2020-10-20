using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Infrastructure.Migrations
{
    public partial class AddCabinetNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CabinetNumber",
                table: "Cabinets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstFileLabel",
                table: "Cabinets",
                maxLength: 9,
                nullable: true);

            migrationBuilder.Sql("update Cabinets set CabinetNumber = convert(int, right(Name, 3))");
            migrationBuilder.Sql(@"with f as (
                select c.Id as cId, min(f.FileLabel) as FirstFileLabel
                from Cabinets c
                    inner join CabinetFileJoin cf
                    on c.Id = cf.CabinetId
                    inner join Files f
                    on f.Id = cf.FileId
                group by c.Id
            )
            update c
            set c.FirstFileLabel = f.FirstFileLabel
            from Cabinets c
                inner join f
                on c.Id = f.cId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CabinetNumber",
                table: "Cabinets");

            migrationBuilder.DropColumn(
                name: "FirstFileLabel",
                table: "Cabinets");
        }
    }
}
