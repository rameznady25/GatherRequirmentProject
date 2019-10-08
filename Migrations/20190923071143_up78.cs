using Microsoft.EntityFrameworkCore.Migrations;

namespace Gather_Requirement_Project.Migrations
{
    public partial class up78 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DevName",
                table: "Screens",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DevName",
                table: "Screens",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
