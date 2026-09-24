using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Results;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Students.Queries.GetMyResults;

public class GetMyResultsQueryHandler
    : IRequestHandler<
        GetMyResultsQuery,
        ResponseDto<IEnumerable<StudentResultResponseDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetMyResultsQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ResponseDto<IEnumerable<StudentResultResponseDto>>> Handle(
        GetMyResultsQuery request,
        CancellationToken cancellationToken)
    {
        if (_currentUserService.UserId == null)
        {
            return new ResponseDto<IEnumerable<StudentResultResponseDto>>
            {
                Status = false,
                Message = "User not authenticated",
                Data = null
            };
        }

        var student = await _context.Students
            .FirstOrDefaultAsync(
                x => x.UserId == _currentUserService.UserId,
                cancellationToken);

        if (student == null)
        {
            return new ResponseDto<IEnumerable<StudentResultResponseDto>>
            {
                Status = false,
                Message = "Student not found",
                Data = null
            };
        }

        var results = await _context.ExamMarks
            .Where(x =>
                x.StudentId == student.Id &&
                x.Status == MarkStatus.Approved)
            .Join(
                _context.Exams,
                mark => mark.ExamId,
                exam => exam.Id,
                (mark, exam) => new
                {
                    Mark = mark,
                    Exam = exam
                })
            .Where(x =>
                (!x.Mark.IsReExam && x.Exam.IsPublished) ||
                (x.Mark.IsReExam && x.Mark.IsPublished))
            .Join(
                _context.Subjects,
                x => x.Mark.SubjectId,
                subject => subject.Id,
                (x, subject) => new StudentResultResponseDto
                {
                    ExamId = x.Exam.Id,
                    ExamName = x.Exam.Name,

                    SubjectId = subject.Id,
                    SubjectName = subject.Name,

                    MarksObtained = x.Mark.MarksObtained,
                    MaxMarks = x.Mark.MaxMarks,

                    Status = x.Mark.Status,
                    IsReExam =  x.Mark.IsReExam
                })
            .ToListAsync(cancellationToken);

        return new ResponseDto<IEnumerable<StudentResultResponseDto>>
        {
            Status = true,
            Message = "Results retrieved successfully",
            Data = results
        };
    }
}