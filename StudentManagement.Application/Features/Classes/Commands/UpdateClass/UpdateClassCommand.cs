using MediatR;
using StudentManagement.Application.DTOs.Classes;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Classes.Commands.UpdateClass;

public class UpdateClassCommand : IRequest<ResponseDto<ClassResponseDto>>
{
    public int ClassId { get; }
    public UpdateClassDto Request { get; }

    public UpdateClassCommand(int classId, UpdateClassDto request)
    {
        ClassId = classId;
        Request = request;
    }
}