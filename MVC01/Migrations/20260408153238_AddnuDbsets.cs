using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC01.Migrations
{
    /// <inheritdoc />
    public partial class AddnuDbsets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Department_DeptId",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Crs_Course_Crs_Id",
                table: "Instructor_Crs");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Crs_Instructor_Ins_Id",
                table: "Instructor_Crs");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Crs_Course_CourseId",
                table: "Student_Crs");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Department_DeptId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructor",
                table: "Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                table: "Department");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.RenameTable(
                name: "Instructor",
                newName: "Instructors");

            migrationBuilder.RenameTable(
                name: "Department",
                newName: "Departments");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Courses");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_DeptId",
                table: "Instructors",
                newName: "IX_Instructors_DeptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructors",
                table: "Instructors",
                column: "Ins_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "DeptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Crs_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Crs_Courses_Crs_Id",
                table: "Instructor_Crs",
                column: "Crs_Id",
                principalTable: "Courses",
                principalColumn: "Crs_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Crs_Instructors_Ins_Id",
                table: "Instructor_Crs",
                column: "Ins_Id",
                principalTable: "Instructors",
                principalColumn: "Ins_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Departments_DeptId",
                table: "Instructors",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "DeptId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Crs_Courses_CourseId",
                table: "Student_Crs",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Crs_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DeptId",
                table: "Students",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "DeptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Crs_Courses_Crs_Id",
                table: "Instructor_Crs");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Crs_Instructors_Ins_Id",
                table: "Instructor_Crs");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Departments_DeptId",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Crs_Courses_CourseId",
                table: "Student_Crs");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DeptId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructors",
                table: "Instructors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "Instructors",
                newName: "Instructor");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Department");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Course");

            migrationBuilder.RenameIndex(
                name: "IX_Instructors_DeptId",
                table: "Instructor",
                newName: "IX_Instructor_DeptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructor",
                table: "Instructor",
                column: "Ins_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                table: "Department",
                column: "DeptId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "Crs_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Department_DeptId",
                table: "Instructor",
                column: "DeptId",
                principalTable: "Department",
                principalColumn: "DeptId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Crs_Course_Crs_Id",
                table: "Instructor_Crs",
                column: "Crs_Id",
                principalTable: "Course",
                principalColumn: "Crs_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Crs_Instructor_Ins_Id",
                table: "Instructor_Crs",
                column: "Ins_Id",
                principalTable: "Instructor",
                principalColumn: "Ins_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Crs_Course_CourseId",
                table: "Student_Crs",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Crs_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Department_DeptId",
                table: "Students",
                column: "DeptId",
                principalTable: "Department",
                principalColumn: "DeptId");
        }
    }
}
