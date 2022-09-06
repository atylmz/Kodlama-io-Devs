using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KodlamaDevs.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgrammingLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProgrammingLanguages",
                columns: new[] { "Id", "CreatedDate", "IsActive", "ModifiedDate", "Name" },
                values: new object[] { 1, new DateTime(2022, 9, 6, 16, 38, 41, 975, DateTimeKind.Local).AddTicks(3379), true, new DateTime(2022, 9, 6, 16, 38, 41, 975, DateTimeKind.Local).AddTicks(3387), "C#" });

            migrationBuilder.InsertData(
                table: "ProgrammingLanguages",
                columns: new[] { "Id", "CreatedDate", "IsActive", "ModifiedDate", "Name" },
                values: new object[] { 2, new DateTime(2022, 9, 6, 16, 38, 41, 975, DateTimeKind.Local).AddTicks(3390), true, new DateTime(2022, 9, 6, 16, 38, 41, 975, DateTimeKind.Local).AddTicks(3391), "Python" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgrammingLanguages");
        }
    }
}
