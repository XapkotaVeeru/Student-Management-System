using MediatR;
using StudentManagement.Application.DTOs.Classes;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Classes.Commands.ActivateClass;

public class ActivateClassCommand : IRequest<ResponseDto<ClassResponseDto>>
{
    public int ClassId { get; }

    public ActivateClassCommand(int classId)
    {
        ClassId = classId;
    }
}