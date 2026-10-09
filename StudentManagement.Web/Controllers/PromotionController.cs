using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Promotions;
using StudentManagement.Application.Features.Promotions.Commands.PromoteStudent;
using StudentManagement.Application.Features.Promotions.Queries.GetPromotions;

namespace Student_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class PromotionController : ControllerBase
{
    private readonly IMediator _mediator;

    public PromotionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PromoteStudent(PromoteStudentDto dto,
        CancellationToken cancellationToken)
    {
        var command = new PromoteStudentCommand(dto);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPromotions([FromQuery] int? studentId, 
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPromotionsQuery(studentId), cancellationToken);

        return Ok(result);
    }
}