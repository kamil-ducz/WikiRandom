using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikiRandom_WebAPI.Migrations
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Articles_PersonId",
                table: "Articles",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Persons_PersonId",
                table: "Articles",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Persons_PersonId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_PersonId",
                table: "Articles");
        }
    }
}
