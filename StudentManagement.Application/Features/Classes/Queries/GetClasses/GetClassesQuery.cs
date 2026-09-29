using MediatR;
using StudentManagement.Application.DTOs.Classes;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Classes.Queries.GetClasses;

public class GetClassesQuery : IRequest<ResponseDto<IEnumerable<ClassResponseDto>>>
{
}