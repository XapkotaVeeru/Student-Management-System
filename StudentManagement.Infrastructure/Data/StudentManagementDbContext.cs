using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Data;

public class StudentManagementDbContext :DbContext, IApplicationDbContext
{
    public StudentManagementDbContext(DbContextOptions<StudentManagementDbContext> options) : base(options)
    {
    }
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<ClassSubject> ClassSubjects { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<ExamMark>  ExamMarks { get; set; }
    public DbSet<AccessLog> AccessLogs { get; set; }
    public DbSet<MarkApproval> MarkApprovals { get; set; }
    public DbSet<ReExamApplication>  ReExamApplications { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<SchoolClass> SchoolClasses { get; set; }
    public DbSet<StudentPromotion> StudentPromotions { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<TeacherAssignment> TeacherAssignments { get; set; }
    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(StudentManagementDbContext).Assembly);
    }
}