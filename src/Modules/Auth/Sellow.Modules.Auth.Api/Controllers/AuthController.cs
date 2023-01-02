using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sellow.Modules.Auth.Core.Features;

namespace Sellow.Modules.Auth.Api.Controllers;

[ApiController]
[Route("/api/v{version:apiVersion}")]
[ApiVersion("1.0")]
internal sealed class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create a new user.
    /// </summary>
    /// <response code="201">User has been successfully created.</response>
    /// <response code="400">Request validation has failed.</response>
    /// <response code="409">User with given credentials already exists.</response>
    /// <response code="500">Internal server error.</response>
    [ProducesResponseType(201)]
    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUser command)
    {
        var createdUserId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetUser), new {id = createdUserId}, null);
    }

    [HttpGet("users/{id:guid}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public Guid GetUser(Guid id) => id;

    /// <summary>
    /// Activate a user.
    /// </summary>
    /// <response code="200">User has been successfully activated.</response>
    /// <response code="422">User has not been found or is activated already.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("auth/activate-user/{id:guid}")]
    public async Task<IActionResult> ActivateUser(Guid id)
    {
        await _mediator.Send(new ActivateUser(id));

        return Ok();
    }
}