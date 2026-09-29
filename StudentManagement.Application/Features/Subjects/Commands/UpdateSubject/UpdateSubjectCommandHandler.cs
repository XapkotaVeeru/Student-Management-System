using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Subjects;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Subjects.Commands.UpdateSubject;

public class UpdateSubjectCommandHandler
    : IRequestHandler<
        UpdateSubjectCommand,
        ResponseDto<SubjectResponseDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateSubjectCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<SubjectResponseDto>> Handle(UpdateSubjectCommand request,
        CancellationToken cancellationToken)
    {
        var subject = await _dbContext.Subjects.FirstOrDefaultAsync(
                x => x.Id == request.SubjectId, cancellationToken);

        if (subject == null)
        {
            return new ResponseDto<SubjectResponseDto>
            {
                Status = false,
                Message = "Subject not found.",
                Data = null
            };
        }

        var subjectExists = await _dbContext.Subjects.AnyAsync(
                x => x.Id != request.SubjectId &&
                     x.SubjectCode == request.Request.SubjectCode, cancellationToken);

        if (subjectExists)
        {
            return new ResponseDto<SubjectResponseDto>
            {
                Status = false,
                Message = "Another subject with this code already exists.",
                Data = null
            };
        }

        subject.Name = request.Request.Name;
        subject.Description = request.Request.Description;
        subject.SubjectCode = request.Request.SubjectCode;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ResponseDto<SubjectResponseDto>
        {
            Status = true,
            Message = "Subject updated successfully.",
            Data = new SubjectResponseDto
            {
                Id = subject.Id,
                Name = subject.Name,
                Description = subject.Description,
                SubjectCode = subject.SubjectCode,
                IsActive = subject.IsActive
            }
        };
    }
}