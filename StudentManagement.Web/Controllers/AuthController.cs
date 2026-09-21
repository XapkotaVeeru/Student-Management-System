using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Auth;
using StudentManagement.Application.Features.Authentication.Commands.Register;
using StudentManagement.Application.Features.Authentication.Commands.Login;
using StudentManagement.Application.Features.Authentication.Commands.Logout;
using StudentManagement.Application.Features.Authentication.Commands.RefreshToken;

namespace Student_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new RegisterCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new RefreshTokenCommand(dto);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(RefreshTokenRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new LogoutCommand(dto);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            BadRequest(result);
        }
        return Ok(result);
    }
}