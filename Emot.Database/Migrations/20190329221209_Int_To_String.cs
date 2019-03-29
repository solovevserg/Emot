using Microsoft.EntityFrameworkCore.Migrations;

namespace Emot.Database.Migrations
{
    public partial class Int_To_String : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Opinions",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Text",
                table: "Opinions",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
