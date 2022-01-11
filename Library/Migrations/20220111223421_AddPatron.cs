using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class AddPatron : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatronId",
                table: "Copies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Patrons",
                columns: table => new
                {
                    PatronId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patrons", x => x.PatronId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Copies_PatronId",
                table: "Copies",
                column: "PatronId");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Patrons_PatronId",
                table: "Copies",
                column: "PatronId",
                principalTable: "Patrons",
                principalColumn: "PatronId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Patrons_PatronId",
                table: "Copies");

            migrationBuilder.DropTable(
                name: "Patrons");

            migrationBuilder.DropIndex(
                name: "IX_Copies_PatronId",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "PatronId",
                table: "Copies");
        }
    }
}
