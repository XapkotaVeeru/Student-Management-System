using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Subjects;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Subjects.Queries.GetSubjectById;

public class GetSubjectByIdQueryHandler : IRequestHandler<GetSubjectByIdQuery, ResponseDto<SubjectResponseDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetSubjectByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<SubjectResponseDto>> Handle(GetSubjectByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var subject = await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == request.SubjectId,
                cancellationToken);

        if (subject == null)
        {
            return new ResponseDto<SubjectResponseDto>
            {
                Status = false,
                Message = "Subject not found.",
                Data = null
            };
        }

        return new ResponseDto<SubjectResponseDto>
        {
            Status = true,
            Message = "Subject retrieved successfully.",
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