using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Marks;
using StudentManagement.Application.DTOs.Teachers;
using StudentManagement.Application.Features.Marks.Commands.EnterMark;
using StudentManagement.Application.Features.Marks.Commands.SubmitMark;
using StudentManagement.Application.Features.ReExams.Commands.EnterReExamMark;
using StudentManagement.Application.Features.Teachers.Commands.UpdateTeacher;

namespace StudentManagement.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TeacherController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeacherController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("exams/{examId}/marks")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> EnterMark(
        int examId,
        EnterMarkDto dto,
        CancellationToken cancellationToken)
    {
        var command = new EnterMarkCommand(examId, dto);

        var result = await _mediator.Send(
            command,
            cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    
    [HttpPut("marks/{markId}/submit")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> SubmitMark(
        int markId,
        CancellationToken cancellationToken)
    {
        var command = new SubmitMarkCommand(markId);

        var result = await _mediator.Send(
            command,
            cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    
    [HttpPost("re-exams/{applicationId}/marks")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> EnterReExamMark(
        int applicationId,
        EnterReExamMarkDto dto,
        CancellationToken cancellationToken)
    {
        var command = new EnterReExamMarkCommand(
            applicationId,
            dto.MarksObtained,
            dto.MaxMarks);

        var result = await _mediator.Send(
            command,
            cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> UpdateTeacher(
        int id,
        UpdateTeacherDto dto,
        CancellationToken cancellationToken)
    {
        var command = new UpdateTeacherCommand(id, dto);

        var result = await _mediator.Send(
            command,
            cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}