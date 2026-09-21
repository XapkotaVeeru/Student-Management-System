using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Marks;
using StudentManagement.Application.Features.Exams.Commands.PublishExam;
using StudentManagement.Application.Features.Marks.Commands.ReviewMark;

namespace Student_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("marks/{markId}/review")]
    public async Task<IActionResult> ReviewMark(
        int markId,
        ReviewMarkDto dto,
        CancellationToken cancellationToken)
    {
        var command = new ReviewMarkCommand(markId, dto);

        var result = await _mediator.Send(
            command,
            cancellationToken);

        if (!result.Status)
            return BadRequest(result);

        return Ok(result);
    }
    
    [HttpPut("exams/{examId}/publish")]
    public async Task<IActionResult> PublishExam(
        int examId,
        CancellationToken cancellationToken)
    {
        var command = new PublishExamCommand(examId);

        var result = await _mediator.Send(
            command,
            cancellationToken);

        if (!result.Status)
            return BadRequest(result);

        return Ok(result);
    }
}