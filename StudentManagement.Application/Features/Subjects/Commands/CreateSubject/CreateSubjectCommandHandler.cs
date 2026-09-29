using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Subjects;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Features.Subjects.Commands.CreateSubject;

public class CreateSubjectCommandHandler
    : IRequestHandler<
        CreateSubjectCommand,
        ResponseDto<SubjectResponseDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateSubjectCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<SubjectResponseDto>> Handle(CreateSubjectCommand request,
        CancellationToken cancellationToken)
    {
        var subjectExists = await _dbContext.Subjects.AnyAsync(
                x => x.SubjectCode == request.Request.SubjectCode, cancellationToken);

        if (subjectExists)
        {
            return new ResponseDto<SubjectResponseDto>
            {
                Status = false,
                Message = "Subject with this code already exists.",
                Data = null
            };
        }

        var subject = new Subject
        {
            Name = request.Request.Name,
            SubjectCode = request.Request.SubjectCode,
            Description = request.Request.Description,
            IsActive = true
        };

        await _dbContext.Subjects.AddAsync(subject, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ResponseDto<SubjectResponseDto>
        {
            Status = true,
            Message = "Subject created successfully.",
            Data = new SubjectResponseDto
            {
                Id = subject.Id,
                Name = subject.Name,
                SubjectCode = subject.SubjectCode,
                Description = subject.Description,
                IsActive = subject.IsActive
            }
        };
    }
}