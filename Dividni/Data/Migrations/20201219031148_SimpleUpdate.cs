using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dividni.Data.Migrations
{
    public partial class SimpleUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Simple",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Marks = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionText = table.Column<string>(type: "TEXT", nullable: true),
                    CorrectAnswers = table.Column<string>(type: "TEXT", nullable: true),
                    IncorrectAnswers = table.Column<string>(type: "TEXT", nullable: true),
                    UserEmail = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simple", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Simple");
        }
    }
}
