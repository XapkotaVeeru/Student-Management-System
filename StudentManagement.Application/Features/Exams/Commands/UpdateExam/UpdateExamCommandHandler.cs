using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Exams;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Exams.Commands.UpdateExam;

public class UpdateExamCommandHandler
    : IRequestHandler<UpdateExamCommand, ResponseDto<ExamResponseDto>>
{
    private readonly IApplicationDbContext _context;

    public UpdateExamCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<ExamResponseDto>> Handle(
        UpdateExamCommand request,
        CancellationToken cancellationToken)
    {
        var dto = request.Exam;

        var exam = await _context.Exams
            .FirstOrDefaultAsync(x => x.Id == request.ExamId, cancellationToken);

        if (exam == null)
            return Fail("Exam not found");

        if (exam.IsPublished)
            return Fail("A published exam cannot be edited");

        if (string.IsNullOrWhiteSpace(dto.Name))
            return Fail("Exam name is required");

        if (dto.EndDate < dto.StartDate)
            return Fail("End date cannot be before the start date");

        if (dto.SchoolClassId != exam.SchoolClassId)
        {
            var classExists = await _context.SchoolClasses.AnyAsync(
                x => x.Id == dto.SchoolClassId && x.IsActive, cancellationToken);

            if (!classExists)
                return Fail("Class not found");

            var hasMarks = await _context.ExamMarks.AnyAsync(
                x => x.ExamId == exam.Id, cancellationToken);

            if (hasMarks)
                return Fail("Cannot change the class because marks already exist for this exam");
        }

        exam.Name = dto.Name.Trim();
        exam.ExamType = dto.ExamType;
        exam.StartDate = dto.StartDate;
        exam.EndDate = dto.EndDate;
        exam.SchoolClassId = dto.SchoolClassId;

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<ExamResponseDto>
        {
            Status = true,
            Message = "Exam updated successfully",
            Data = new ExamResponseDto
            {
                Id = exam.Id,
                Name = exam.Name,
                ExamType = exam.ExamType,
                StartDate = exam.StartDate,
                EndDate = exam.EndDate,
                SchoolClassId = exam.SchoolClassId,
                IsActive = exam.IsActive,
                IsPublished = exam.IsPublished
            }
        };
    }

    private static ResponseDto<ExamResponseDto> Fail(string message) => new()
    {
        Status = false,
        Message = message,
        Data = null
    };
}