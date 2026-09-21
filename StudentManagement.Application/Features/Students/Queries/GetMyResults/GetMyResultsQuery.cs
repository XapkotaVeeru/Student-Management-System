using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Results;

namespace StudentManagement.Application.Features.Students.Queries.GetMyResults;

public class GetMyResultsQuery
    : IRequest<ResponseDto<IEnumerable<StudentResultResponseDto>>>
{
}