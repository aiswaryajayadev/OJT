using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web_Api.Controllers
{
   
        [ApiController]
        [Route("api/[controller]")]
        public class AuthController : ControllerBase
        {
            private readonly IMediator _mediator;

            public AuthController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginCommand command)
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogoutCommand());
            return Ok(new { Message = "Logged out successfully" });
        }
    }
    }
