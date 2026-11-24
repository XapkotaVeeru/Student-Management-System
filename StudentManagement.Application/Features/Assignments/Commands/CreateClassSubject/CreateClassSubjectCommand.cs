using MediatR;
using StudentManagement.Application.DTOs.Assignments;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Assignments.Commands.CreateClassSubject;

public class CreateClassSubjectCommand : IRequest<ResponseDto<ClassSubjectResponseDto>>
{
    public CreateClassSubjectDto ClassSubject { get; }

    public CreateClassSubjectCommand(CreateClassSubjectDto classSubject)
    {
        ClassSubject = classSubject;
    }
}