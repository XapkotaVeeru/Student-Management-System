using MediatR;
using StudentManagement.Application.DTOs.Classes;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Classes.Commands.CreateClass;

public class CreateClassCommand : IRequest<ResponseDto<ClassResponseDto>>
{
    public CreateClassDto Request { get; }

    public CreateClassCommand(CreateClassDto request)
    {
        Request = request;
    }
}