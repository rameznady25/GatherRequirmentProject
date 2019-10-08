using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gather_Requirement_Project.Migrations
{
    public partial class up91 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainPortalID",
                table: "Portal",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MainPortal",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnglishName = table.Column<string>(nullable: true),
                    ArabicName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainPortal", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Portal_MainPortalID",
                table: "Portal",
                column: "MainPortalID");

            migrationBuilder.AddForeignKey(
                name: "FK_Portal_MainPortal_MainPortalID",
                table: "Portal",
                column: "MainPortalID",
                principalTable: "MainPortal",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portal_MainPortal_MainPortalID",
                table: "Portal");

            migrationBuilder.DropTable(
                name: "MainPortal");

            migrationBuilder.DropIndex(
                name: "IX_Portal_MainPortalID",
                table: "Portal");

            migrationBuilder.DropColumn(
                name: "MainPortalID",
                table: "Portal");
        }
    }
}
