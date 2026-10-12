using MediatR;
using StudentManagement.Application.DTOs.Students;

namespace StudentManagement.Application.Features.Students.Queries.GetStudents;

public class GetStudentsQuery : IRequest<List<StudentResponseDto>>
{
    public int? SchoolClassId { get; set; }
    public bool? IsActive { get; set; }
    public string Search { get; set; }

    public GetStudentsQuery(int? schoolClassId, bool? isActive, string search)
    {
        SchoolClassId = schoolClassId;
        IsActive = isActive;
        Search = search;
    }
}