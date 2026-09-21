using StudentManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    
    DbSet<RefreshToken> RefreshTokens { get; }
    
    DbSet<Student> Students { get; }
    
    DbSet<ClassSubject> ClassSubjects { get; }
    
    DbSet<Subject> Subjects { get; }
    
    DbSet<ExamMark> ExamMarks { get; }
    
    DbSet<Exam> Exams { get; }
    
    DbSet<TeacherAssignment> TeacherAssignments { get; }
    
    DbSet<Teacher> Teachers { get; }
    
    DbSet<MarkApproval> MarkApprovals { get; }
    
    DbSet<ReExamApplication> ReExamApplications { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}