using Application.Commands;
using Application.Common;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;


namespace Application.Command_Handler
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand,APIResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly ILogger _logger;
        public AssignRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = Log.ForContext<CreateUserCommandHandler>();
        }
        public async Task<APIResponse> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                _logger.Information("User not found with Id: {@UserId}", request.UserId);
                return APIResponse.Failure("User not found.");
            }

            _logger.Information("Found User: {@UserName}", user.UserName);

            if (!await _roleManager.RoleExistsAsync(request.RoleName))
            {
                _logger.Information("Role not found: {@RoleName}", request.RoleName);
                return APIResponse.Failure("Role does not exist.");
            }

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);

            if (result.Succeeded)
            {
                _logger.Information("Successfully assigned role {@RoleName} to user {@UserName}", request.RoleName, user.UserName);
                return APIResponse.SuccessMessage("Role assigned successfully.");
            }

            _logger.Information("Failed to assign role {@RoleName} to user {@UserName}", request.RoleName, user.UserName);
            return APIResponse.Failure("Failed to assign role.");
        }
    }
}
