using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Exams;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Exams.Queries.GetExams;

public class GetExamsQueryHandler : IRequestHandler<GetExamsQuery, List<ExamResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetExamsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<List<ExamResponseDto>> Handle(GetExamsQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var userRole = _currentUserService.UserRole;

        if (!userId.HasValue || !userRole.HasValue)
        {
            return new List<ExamResponseDto>();
        }

        var examsQuery = _context.Exams.AsQueryable();

        if (userRole == UserRole.Admin)
        {
            // Admin can see all exams.
        }
        else if (userRole == UserRole.Teacher)
        {
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(
                    x => x.UserId == userId.Value,
                    cancellationToken);

            if (teacher == null)
            {
                return new List<ExamResponseDto>();
            }

            var teacherClassIds = _context.TeacherAssignments
                .Where(x => x.TeacherId == teacher.Id)
                .Select(x => x.SchoolClassId);

            examsQuery = examsQuery
                .Where(x =>
                    x.IsPublished &&
                    teacherClassIds.Contains(x.SchoolClassId));
        }
        else if (userRole == UserRole.Student)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(
                    x => x.UserId == userId.Value,
                    cancellationToken);

            if (student == null || !student.SchoolClassId.HasValue)
            {
                return new List<ExamResponseDto>();
            }

            examsQuery = examsQuery
                .Where(x =>
                    x.IsPublished &&
                    x.SchoolClassId == student.SchoolClassId.Value);
        }
        else
        {
            return new List<ExamResponseDto>();
        }

        var exams = await examsQuery
            .Select(exam => new ExamResponseDto
            {
                Id = exam.Id,
                StartDate = exam.StartDate,
                EndDate = exam.EndDate,
                SchoolClassId = exam.SchoolClassId,
                Name = exam.Name,
                ExamType = exam.ExamType,
                IsActive = exam.IsActive,
                IsPublished = exam.IsPublished
            })
            .ToListAsync(cancellationToken);

        return exams;
    }
}