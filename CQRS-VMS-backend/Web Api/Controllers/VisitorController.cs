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
    // Creates a new visitor.
    /* [Authorize(Policy = "RequireUserRole")] */
    [HttpPost]
    public async Task<ActionResult<Visitor>> visitors([FromBody] VisitorCreationDTO visitorDto)
    {
        if (visitorDto == null)
        {
            return BadRequest("Visitor data cannot be null.");
        }

        try
        {
            var command = new CreateVisitorCommand { VisitorDto = visitorDto };
            var result = await _mediator.Send(command);

            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception as necessary
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }


    /*[Authorize(Policy = "RequireAdminOrManagerRole")]*/
    // Retrieves a list of visitors.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Visitor>>> visitors()
    {
        try
        {
            var result = await _mediator.Send(new GetVisitorDetailsQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception as necessary
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}