using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Marks;
using StudentManagement.Application.Features.Marks.Commands.EnterMark;
using StudentManagement.Application.Features.Marks.Commands.SubmitMark;

namespace StudentManagement.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Teacher")]
public class TeacherController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeacherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("exams/{examId}/marks")]
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
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("marks/{markId}/submit")]
    public async Task<IActionResult> SubmitMark(
        int markId,
        CancellationToken cancellationToken)
    {
        var command = new SubmitMarkCommand(markId);

        var result = await _mediator.Send(
            command,
            cancellationToken);

        if (!result.Status)
            return BadRequest(result);

        return Ok(result);
    }
}