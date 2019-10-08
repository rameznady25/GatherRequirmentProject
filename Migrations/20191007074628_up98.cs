using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gather_Requirement_Project.Migrations
{
    public partial class up98 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectionID",
                table: "CustomerPrograms",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnglishName = table.Column<string>(nullable: true),
                    ArabicName = table.Column<string>(nullable: true),
                    PortalID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Section_Portal_PortalID",
                        column: x => x.PortalID,
                        principalTable: "Portal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPrograms_SectionID",
                table: "CustomerPrograms",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Section_PortalID",
                table: "Section",
                column: "PortalID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPrograms_Section_SectionID",
                table: "CustomerPrograms",
                column: "SectionID",
                principalTable: "Section",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPrograms_Section_SectionID",
                table: "CustomerPrograms");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPrograms_SectionID",
                table: "CustomerPrograms");

            migrationBuilder.DropColumn(
                name: "SectionID",
                table: "CustomerPrograms");
        }
    }
}
