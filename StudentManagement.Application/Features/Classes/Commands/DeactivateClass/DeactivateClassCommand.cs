using MediatR;
using StudentManagement.Application.DTOs.Classes;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Classes.Commands.DeactivateClass;

public class DeactivateClassCommand : IRequest<ResponseDto<ClassResponseDto>>
{
    public int ClassId { get; }

    public DeactivateClassCommand(int classId)
    {
        ClassId = classId;
    }
}