using Microsoft.EntityFrameworkCore.Migrations;

namespace Logic.Migrations
{
    public partial class TruckNewUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Validation",
                table: "employees",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Validation",
                table: "employees");
        }
    }
}
