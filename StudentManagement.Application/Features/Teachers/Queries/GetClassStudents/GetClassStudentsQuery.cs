using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Teachers;

namespace StudentManagement.Application.Features.Teachers.Queries.GetClassStudents;

public class GetClassStudentsQuery : IRequest<ResponseDto<PagedResult<ClassStudentResponseDto>>>
{
    public int SchoolClassId { get; set; }
    public string? Search { get; set; }
    public int? Page { get; set; } = 1;
    public int? PageSize { get; set; } = 10;

    public GetClassStudentsQuery(int schoolClassId, string? search, int? page, int? pageSize)
    {
        SchoolClassId = schoolClassId;
        Search = search;
        Page = page;
        PageSize = pageSize;
    }
}