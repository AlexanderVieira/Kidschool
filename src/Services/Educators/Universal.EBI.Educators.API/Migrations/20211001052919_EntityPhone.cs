using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Universal.EBI.Educators.API.Migrations
{
    public partial class EntityPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Celular",
                table: "Educators");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Educators");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Educators",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "Function",
                table: "Educators",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "varchar(13)", nullable: false),
                    PhoneType = table.Column<int>(type: "int", nullable: false),
                    EducatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Educators_EducatorId",
                        column: x => x.EducatorId,
                        principalTable: "Educators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phones_EducatorId",
                table: "Phones",
                column: "EducatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropColumn(
                name: "Function",
                table: "Educators");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Educators",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Celular",
                table: "Educators",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Educators",
                type: "varchar(100)",
                nullable: true);
        }
    }
}
