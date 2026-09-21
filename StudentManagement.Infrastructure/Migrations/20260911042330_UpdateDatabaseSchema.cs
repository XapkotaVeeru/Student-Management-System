using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherAssignments_Teachers_TeacherId",
                table: "TeacherAssignments");

            migrationBuilder.DropIndex(
                name: "IX_TeacherAssignments_TeacherId",
                table: "TeacherAssignments");

            migrationBuilder.DropColumn(
                name: "ApprovedByUserId",
                table: "StudentPromotions");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Teachers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Teachers",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectCode",
                table: "Subjects",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subjects",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Subjects",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Subjects",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "StudentPromotions",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ReviewedByUserId",
                table: "StudentPromotions",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SchoolClasses",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "SchoolClasses",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "RefreshTokens",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "ReExamApplications",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "ReExamApplications",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ReExamApplications",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "MarkApprovals",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Exams",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublished",
                table: "Exams",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Exams",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "ExamType",
                table: "Exams",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "ExamMarks",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxMarks",
                table: "ExamMarks",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "MarksObtained",
                table: "ExamMarks",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "AccessLogs",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HttpMethod",
                table: "AccessLogs",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Endpoint",
                table: "AccessLogs",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignments_SchoolClassId",
                table: "TeacherAssignments",
                column: "SchoolClassId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignments_SubjectId",
                table: "TeacherAssignments",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignments_TeacherId_SchoolClassId_SubjectId",
                table: "TeacherAssignments",
                columns: new[] { "TeacherId", "SchoolClassId", "SubjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SubjectCode",
                table: "Subjects",
                column: "SubjectCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentPromotions_FromSchoolClassId",
                table: "StudentPromotions",
                column: "FromSchoolClassId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPromotions_ReviewedByUserId",
                table: "StudentPromotions",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPromotions_StudentId",
                table: "StudentPromotions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPromotions_ToSchoolClassId",
                table: "StudentPromotions",
                column: "ToSchoolClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClasses_Name_AcademicYear",
                table: "SchoolClasses",
                columns: new[] { "Name", "AcademicYear" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReExamApplications_ExamId_SubjectId_StudentId",
                table: "ReExamApplications",
                columns: new[] { "ExamId", "SubjectId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReExamApplications_ReviewedByUserId",
                table: "ReExamApplications",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReExamApplications_StudentId",
                table: "ReExamApplications",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReExamApplications_SubjectId",
                table: "ReExamApplications",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkApprovals_ApprovedByUserId",
                table: "MarkApprovals",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkApprovals_ExamMarkId",
                table: "MarkApprovals",
                column: "ExamMarkId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_SchoolClassId",
                table: "Exams",
                column: "SchoolClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamMarks_EnteredByTeacherId",
                table: "ExamMarks",
                column: "EnteredByTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamMarks_ExamId_StudentId_SubjectId",
                table: "ExamMarks",
                columns: new[] { "ExamId", "StudentId", "SubjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamMarks_StudentId",
                table: "ExamMarks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamMarks_SubjectId",
                table: "ExamMarks",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjects_SchoolClassId_SubjectId",
                table: "ClassSubjects",
                columns: new[] { "SchoolClassId", "SubjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjects_SubjectId",
                table: "ClassSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessLogs_UserId",
                table: "AccessLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessLogs_Users_UserId",
                table: "AccessLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubjects_SchoolClasses_SchoolClassId",
                table: "ClassSubjects",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubjects_Subjects_SubjectId",
                table: "ClassSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamMarks_Exams_ExamId",
                table: "ExamMarks",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamMarks_Students_StudentId",
                table: "ExamMarks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamMarks_Subjects_SubjectId",
                table: "ExamMarks",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamMarks_Teachers_EnteredByTeacherId",
                table: "ExamMarks",
                column: "EnteredByTeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_SchoolClasses_SchoolClassId",
                table: "Exams",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MarkApprovals_ExamMarks_ExamMarkId",
                table: "MarkApprovals",
                column: "ExamMarkId",
                principalTable: "ExamMarks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MarkApprovals_Users_ApprovedByUserId",
                table: "MarkApprovals",
                column: "ApprovedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReExamApplications_Exams_ExamId",
                table: "ReExamApplications",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReExamApplications_Students_StudentId",
                table: "ReExamApplications",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReExamApplications_Subjects_SubjectId",
                table: "ReExamApplications",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReExamApplications_Users_ReviewedByUserId",
                table: "ReExamApplications",
                column: "ReviewedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPromotions_SchoolClasses_FromSchoolClassId",
                table: "StudentPromotions",
                column: "FromSchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPromotions_SchoolClasses_ToSchoolClassId",
                table: "StudentPromotions",
                column: "ToSchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPromotions_Students_StudentId",
                table: "StudentPromotions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPromotions_Users_ReviewedByUserId",
                table: "StudentPromotions",
                column: "ReviewedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherAssignments_SchoolClasses_SchoolClassId",
                table: "TeacherAssignments",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherAssignments_Subjects_SubjectId",
                table: "TeacherAssignments",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherAssignments_Teachers_TeacherId",
                table: "TeacherAssignments",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessLogs_Users_UserId",
                table: "AccessLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjects_SchoolClasses_SchoolClassId",
                table: "ClassSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjects_Subjects_SubjectId",
                table: "ClassSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamMarks_Exams_ExamId",
                table: "ExamMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamMarks_Students_StudentId",
                table: "ExamMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamMarks_Subjects_SubjectId",
                table: "ExamMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamMarks_Teachers_EnteredByTeacherId",
                table: "ExamMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_SchoolClasses_SchoolClassId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_MarkApprovals_ExamMarks_ExamMarkId",
                table: "MarkApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_MarkApprovals_Users_ApprovedByUserId",
                table: "MarkApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_ReExamApplications_Exams_ExamId",
                table: "ReExamApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_ReExamApplications_Students_StudentId",
                table: "ReExamApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_ReExamApplications_Subjects_SubjectId",
                table: "ReExamApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_ReExamApplications_Users_ReviewedByUserId",
                table: "ReExamApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPromotions_SchoolClasses_FromSchoolClassId",
                table: "StudentPromotions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPromotions_SchoolClasses_ToSchoolClassId",
                table: "StudentPromotions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPromotions_Students_StudentId",
                table: "StudentPromotions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPromotions_Users_ReviewedByUserId",
                table: "StudentPromotions");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherAssignments_SchoolClasses_SchoolClassId",
                table: "TeacherAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherAssignments_Subjects_SubjectId",
                table: "TeacherAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherAssignments_Teachers_TeacherId",
                table: "TeacherAssignments");

            migrationBuilder.DropIndex(
                name: "IX_TeacherAssignments_SchoolClassId",
                table: "TeacherAssignments");

            migrationBuilder.DropIndex(
                name: "IX_TeacherAssignments_SubjectId",
                table: "TeacherAssignments");

            migrationBuilder.DropIndex(
                name: "IX_TeacherAssignments_TeacherId_SchoolClassId_SubjectId",
                table: "TeacherAssignments");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SubjectCode",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentPromotions_FromSchoolClassId",
                table: "StudentPromotions");

            migrationBuilder.DropIndex(
                name: "IX_StudentPromotions_ReviewedByUserId",
                table: "StudentPromotions");

            migrationBuilder.DropIndex(
                name: "IX_StudentPromotions_StudentId",
                table: "StudentPromotions");

            migrationBuilder.DropIndex(
                name: "IX_StudentPromotions_ToSchoolClassId",
                table: "StudentPromotions");

            migrationBuilder.DropIndex(
                name: "IX_SchoolClasses_Name_AcademicYear",
                table: "SchoolClasses");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_ReExamApplications_ExamId_SubjectId_StudentId",
                table: "ReExamApplications");

            migrationBuilder.DropIndex(
                name: "IX_ReExamApplications_ReviewedByUserId",
                table: "ReExamApplications");

            migrationBuilder.DropIndex(
                name: "IX_ReExamApplications_StudentId",
                table: "ReExamApplications");

            migrationBuilder.DropIndex(
                name: "IX_ReExamApplications_SubjectId",
                table: "ReExamApplications");

            migrationBuilder.DropIndex(
                name: "IX_MarkApprovals_ApprovedByUserId",
                table: "MarkApprovals");

            migrationBuilder.DropIndex(
                name: "IX_MarkApprovals_ExamMarkId",
                table: "MarkApprovals");

            migrationBuilder.DropIndex(
                name: "IX_Exams_SchoolClassId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_ExamMarks_EnteredByTeacherId",
                table: "ExamMarks");

            migrationBuilder.DropIndex(
                name: "IX_ExamMarks_ExamId_StudentId_SubjectId",
                table: "ExamMarks");

            migrationBuilder.DropIndex(
                name: "IX_ExamMarks_StudentId",
                table: "ExamMarks");

            migrationBuilder.DropIndex(
                name: "IX_ExamMarks_SubjectId",
                table: "ExamMarks");

            migrationBuilder.DropIndex(
                name: "IX_ClassSubjects_SchoolClassId_SubjectId",
                table: "ClassSubjects");

            migrationBuilder.DropIndex(
                name: "IX_ClassSubjects_SubjectId",
                table: "ClassSubjects");

            migrationBuilder.DropIndex(
                name: "IX_AccessLogs_UserId",
                table: "AccessLogs");

            migrationBuilder.DropColumn(
                name: "ReviewedByUserId",
                table: "StudentPromotions");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Teachers",
                type: "integer",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Teachers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectCode",
                table: "Subjects",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subjects",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Subjects",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Subjects",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "StudentPromotions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedByUserId",
                table: "StudentPromotions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SchoolClasses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "SchoolClasses",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "RefreshTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ReExamApplications",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "ReExamApplications",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ReExamApplications",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "MarkApprovals",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Exams",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublished",
                table: "Exams",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Exams",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExamType",
                table: "Exams",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ExamMarks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxMarks",
                table: "ExamMarks",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MarksObtained",
                table: "ExamMarks",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "AccessLogs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HttpMethod",
                table: "AccessLogs",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Endpoint",
                table: "AccessLogs",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssignments_TeacherId",
                table: "TeacherAssignments",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherAssignments_Teachers_TeacherId",
                table: "TeacherAssignments",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
