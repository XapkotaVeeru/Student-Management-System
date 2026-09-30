using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Students;

namespace StudentManagement.Application.Features.Students.Commands.UpdateStudent;

public record UpdateStudentCommand(int StudentId, UpdateStudentDto Student ) : IRequest<ResponseDto<bool>>;