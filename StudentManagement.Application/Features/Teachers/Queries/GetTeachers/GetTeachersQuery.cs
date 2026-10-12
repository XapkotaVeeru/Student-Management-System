using MediatR;
using StudentManagement.Application.DTOs.Teachers;

namespace StudentManagement.Application.Features.Teachers.Queries.GetTeachers;

public class GetTeachersQuery : IRequest<List<TeacherResponseDto>>
{
    public bool? IsActive { get; }
    public string? Search { get; }

    public GetTeachersQuery(bool? isActive, string? search)
    {
        IsActive = isActive;
        Search = search;
    }
}