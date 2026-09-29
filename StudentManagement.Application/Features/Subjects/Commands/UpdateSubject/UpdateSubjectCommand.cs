using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Subjects;

namespace StudentManagement.Application.Features.Subjects.Commands.UpdateSubject;

public class UpdateSubjectCommand : IRequest<ResponseDto<SubjectResponseDto>>
{
    public int SubjectId { get; }
    
    public UpdateSubjectDto Request { get; }
    
    public UpdateSubjectCommand(int subjectId, UpdateSubjectDto request)
    {
        SubjectId = subjectId;
        Request = request;
    }
}