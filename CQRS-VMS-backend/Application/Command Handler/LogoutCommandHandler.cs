using Application.Commands;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;


namespace Application.Command_Handler
{
    public class LogoutCommandHandler:IRequestHandler<LogoutCommand>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;
        public LogoutCommandHandler(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _logger = Log.ForContext<CreateUserCommandHandler>();
        }

        public async Task<Unit> Handle(LogoutCommand request,CancellationToken cancellationToken)
        {
            _logger.Information("Handling logout command ");

            await _signInManager.SignOutAsync();

            _logger.Information("Logged out successfully ");

            return Unit.Value;
        }

    }
}
