using Application.Commands;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command_Handler
{
    public class LogoutCommandHandler:IRequestHandler<LogoutCommand>
    {
        private readonly SignInManager<User> _signInManager;
        public LogoutCommandHandler(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Unit> Handle(LogoutCommand request,CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            return Unit.Value;
        }

    }
}
