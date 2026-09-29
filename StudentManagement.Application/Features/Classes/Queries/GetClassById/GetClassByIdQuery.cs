using MediatR;
using StudentManagement.Application.DTOs.Classes;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Classes.Queries.GetClassById;

public class GetClassByIdQuery : IRequest<ResponseDto<ClassResponseDto>>
{
    public int ClassId { get; }

    public GetClassByIdQuery(int classId)
    {
        ClassId = classId;
    }
}