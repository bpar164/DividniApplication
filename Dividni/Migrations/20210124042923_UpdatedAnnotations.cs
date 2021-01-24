using Microsoft.EntityFrameworkCore.Migrations;

namespace Dividni.Migrations
{
    public partial class UpdatedAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CoverPage",
                table: "Assessment",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Appendix",
                table: "Assessment",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CoverPage",
                table: "Assessment",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Appendix",
                table: "Assessment",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
