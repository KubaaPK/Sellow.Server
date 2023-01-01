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

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUser command)
    {
        var createdUserId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetUser), new {id = createdUserId}, null);
    }

    [HttpGet("users/{id:guid}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public Guid GetUser(Guid id) => id;
}