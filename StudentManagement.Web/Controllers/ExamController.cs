using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Exams;
using StudentManagement.Application.Features.Exams.Commands.CreateExam;
using StudentManagement.Application.Features.Exams.Commands.SetExamActive;
using StudentManagement.Application.Features.Exams.Commands.UpdateExam;
using StudentManagement.Application.Features.Exams.Queries.GetExams;

namespace Student_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExamController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExamController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateExam(CreateExamDto dto, CancellationToken cancellationToken)
    {
        var command = new CreateExamCommand(dto);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin,Teacher,Student")]
    public async Task<IActionResult> GetExams(
        CancellationToken cancellationToken)
    {
        var query = new GetExamsQuery();

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateExam(int id, UpdateExamDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateExamCommand(id, dto), cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpPut("{id}/activate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ActivateExam(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SetExamActiveCommand
            (id, true), cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPut("{id}/deactivate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeactivateExam(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send
            (new SetExamActiveCommand(id, false), cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}