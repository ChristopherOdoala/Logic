using Microsoft.EntityFrameworkCore.Migrations;

namespace Logic.Migrations
{
    public partial class EmployeeUpdateNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoOfExempted",
                table: "status",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoOfExempted",
                table: "status");
        }
    }
}
