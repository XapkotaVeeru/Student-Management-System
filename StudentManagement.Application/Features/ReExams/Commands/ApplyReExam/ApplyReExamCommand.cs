using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.ReExam;

namespace StudentManagement.Application.Features.ReExams.Commands.ApplyReExam;

public class ApplyReExamCommand
    : IRequest<ResponseDto<ReExamResponseDto>>
{
    public ApplyReExamDto Request { get; }

    public ApplyReExamCommand(ApplyReExamDto request)
    {
        Request = request;
    }
}