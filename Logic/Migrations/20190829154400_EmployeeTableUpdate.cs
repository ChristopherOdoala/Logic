using Microsoft.EntityFrameworkCore.Migrations;

namespace Logic.Migrations
{
    public partial class EmployeeTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stage",
                table: "status",
                newName: "FinalStage");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "employees",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "employees");

            migrationBuilder.RenameColumn(
                name: "FinalStage",
                table: "status",
                newName: "Stage");
        }
    }
}
