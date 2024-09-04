using Application.Commands;
using Application.Common;
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
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand,APIResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        public AssignRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<APIResponse> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return APIResponse.Failure("User not found.");

            if (!await _roleManager.RoleExistsAsync(request.RoleName))
                return APIResponse.Failure("Role does not exist.");

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);
            if (result.Succeeded)
                return APIResponse.SuccessMessage("Role assigned successfully.");

            return APIResponse.Failure("Failed to assign role.");
        }
    }
}
