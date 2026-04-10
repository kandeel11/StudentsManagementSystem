using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC01.Migrations
{
    /// <inheritdoc />
    public partial class Addnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Department_DeptId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "DeptId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Department_DeptId",
                table: "Students",
                column: "DeptId",
                principalTable: "Department",
                principalColumn: "DeptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Department_DeptId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "DeptId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Department_DeptId",
                table: "Students",
                column: "DeptId",
                principalTable: "Department",
                principalColumn: "DeptId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
