using Microsoft.EntityFrameworkCore.Migrations;

namespace Universal.EBI.Educators.API.Migrations
{
    public partial class ParamFunctionType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Function",
                table: "Educators",
                newName: "FunctionType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FunctionType",
                table: "Educators",
                newName: "Function");
        }
    }
}
