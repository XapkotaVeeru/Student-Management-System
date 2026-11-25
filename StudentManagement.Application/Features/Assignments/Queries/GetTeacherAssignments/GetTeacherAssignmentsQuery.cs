using MediatR;
using StudentManagement.Application.DTOs.Assignments;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Assignments.Queries.GetTeacherAssignments;

public class GetTeacherAssignmentsQuery : IRequest<ResponseDto<IEnumerable<TeacherAssignmentListItemDto>>>
{
    public int? TeacherId { get; }
    public int? SchoolClassId { get; }

    public GetTeacherAssignmentsQuery(int? teacherId, int? schoolClassId)
    {
        TeacherId = teacherId;
        SchoolClassId = schoolClassId;
    }
}