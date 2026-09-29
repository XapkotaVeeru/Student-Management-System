using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Subjects;

namespace StudentManagement.Application.Features.Subjects.Commands.CreateSubject;

public class CreateSubjectCommand : IRequest<ResponseDto<SubjectResponseDto>>
{
    public CreateSubjectDto Request { get; }

    public CreateSubjectCommand(CreateSubjectDto request)
    {
        Request = request;
    }
}