using Application.Commands;
using Application.Queries;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost]
    public async Task<IActionResult> users([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }


    
    [Authorize(Policy = "RequireAdminOrManagerRole")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> users()
    {
        var result = await _mediator.Send(new UserListQuery());
        return Ok(result);
    }



    [Authorize(Policy = "RequireAdminRole")]
    [HttpPut]
    public async Task<IActionResult> user([FromBody] UpdateUserCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost]
    public async Task<IActionResult> assignrole([FromBody] AssignRoleCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [Authorize(Policy = "RequireAdminOrManagerRole")]
    [HttpGet("{userId}")]
    public async Task<IActionResult> role(string userId)
    {
        var roles = await _mediator.Send(new GetUserRolesQuery { UserId = userId });
        return Ok(roles);
    }
}