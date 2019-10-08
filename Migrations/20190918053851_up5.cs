using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gather_Requirement_Project.Migrations
{
    public partial class up5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DevName",
                table: "CustomerPrograms",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PortalID",
                table: "CustomerPrograms",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Portal",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnglishName = table.Column<string>(nullable: true),
                    ArabicName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portal", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPrograms_PortalID",
                table: "CustomerPrograms",
                column: "PortalID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPrograms_Portal_PortalID",
                table: "CustomerPrograms",
                column: "PortalID",
                principalTable: "Portal",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPrograms_Portal_PortalID",
                table: "CustomerPrograms");

            migrationBuilder.DropTable(
                name: "Portal");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPrograms_PortalID",
                table: "CustomerPrograms");

            migrationBuilder.DropColumn(
                name: "DevName",
                table: "CustomerPrograms");

            migrationBuilder.DropColumn(
                name: "PortalID",
                table: "CustomerPrograms");
        }
    }
}
