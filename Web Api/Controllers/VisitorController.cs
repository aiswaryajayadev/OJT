using Application.Commands;
using Application.Queries;
using Infrastructure.Models;
using Infrastructure.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]/[action]")]
public class VisitorController : ControllerBase
{
    private readonly IMediator _mediator;

    public VisitorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]

    public async Task<ActionResult<Visitor>> CreateVisitor([FromBody] VisitorCreationDTO visitorDto)
    {
        var command = new CreateVisitorCommand { VisitorDto = visitorDto };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

  

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Visitor>>> GetVisitorDetails()
    {
        var result = await _mediator.Send(new GetVisitorDetailsQuery());
        return Ok(result);
    }
}
