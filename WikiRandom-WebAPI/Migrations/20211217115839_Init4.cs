using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikiRandom_WebAPI.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__persons",
                table: "_persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK__articles",
                table: "_articles");

            migrationBuilder.RenameTable(
                name: "_persons",
                newName: "Persons");

            migrationBuilder.RenameTable(
                name: "_articles",
                newName: "Articles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articles",
                table: "Articles",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Articles",
                table: "Articles");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "_persons");

            migrationBuilder.RenameTable(
                name: "Articles",
                newName: "_articles");

            migrationBuilder.AddPrimaryKey(
                name: "PK__persons",
                table: "_persons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__articles",
                table: "_articles",
                column: "Id");
        }
    }
}
