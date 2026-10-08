using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Features.Exams.Commands.CreateExam;

public class CreateExamCommandHandler : IRequestHandler<CreateExamCommand, ResponseDto<int>>
{
    private readonly IApplicationDbContext _context;

    public CreateExamCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<int>> Handle(CreateExamCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Exam;

        var classExists = await _context.SchoolClasses.AnyAsync(
                x => x.Id == dto.SchoolClassId && x.IsActive, cancellationToken);

        if (!classExists)
        {
            return new ResponseDto<int>
            {
                Status = false,
                Message = "Class not found",
                Data = 0
            };
        }

        var exam = new Exam
        {
            Name = dto.Name,
            ExamType = dto.ExamType,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            SchoolClassId = dto.SchoolClassId,
            IsActive = true,
            IsPublished = false
        };

        await _context.Exams.AddAsync(exam, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<int>
        {
            Status = true,
            Message = "Exam created successfully",
            Data = exam.Id
        };
    }
}