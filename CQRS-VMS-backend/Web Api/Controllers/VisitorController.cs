using Application.Commands;
using Application.Queries;
using Infrastructure.Models;
using Infrastructure.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[action]")]
public class VisitorController : ControllerBase
{
    private readonly IMediator _mediator;

    public VisitorController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [Authorize(Policy = "RequireUserRole")]   
    [HttpPost]
    public async Task<ActionResult<Visitor>> visitors([FromBody] VisitorCreationDTO visitorDto)
    {
        if (visitorDto == null)
        {
            return BadRequest("Visitor data cannot be null");
        }

        var command = new CreateVisitorCommand { VisitorDto = visitorDto };
        var result = await _mediator.Send(command);

        return Ok(result);
    }


    [Authorize(Policy = "RequireAdminOrManagerRole")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Visitor>>> visitors()
    {
        var result = await _mediator.Send(new GetVisitorDetailsQuery());
        return Ok(result);
    }
}
