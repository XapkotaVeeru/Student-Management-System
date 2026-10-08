using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Exams;
using StudentManagement.Application.Features.Exams.Commands.CreateExam;
using StudentManagement.Application.Features.Exams.Queries.GetExams;

namespace StudentManagement.Web.Controllers;

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
}