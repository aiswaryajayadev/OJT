using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web_Api.Controllers
{
   
        [ApiController]
    [Route("api/[action]")]
    public class AuthController : ControllerBase
        {
            private readonly IMediator _mediator;

            public AuthController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpPost]
            public async Task<IActionResult> Login([FromBody] LoginCommand command)
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogoutCommand());
            return Ok(new { Message = "Logged out successfully" });
        }
    }
    }
