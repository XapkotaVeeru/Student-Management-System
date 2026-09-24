using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExamMarkUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamMarks_ExamId_StudentId_SubjectId",
                table: "ExamMarks");

            migrationBuilder.CreateIndex(
                name: "IX_ExamMarks_ExamId_StudentId_SubjectId_IsReExam",
                table: "ExamMarks",
                columns: new[] { "ExamId", "StudentId", "SubjectId", "IsReExam" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamMarks_ExamId_StudentId_SubjectId_IsReExam",
                table: "ExamMarks");

            migrationBuilder.CreateIndex(
                name: "IX_ExamMarks_ExamId_StudentId_SubjectId",
                table: "ExamMarks",
                columns: new[] { "ExamId", "StudentId", "SubjectId" },
                unique: true);
        }
    }
}
