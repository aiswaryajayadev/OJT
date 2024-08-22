using Application.Commands;
using Application.Common;
using Infrastructure.Models;
using Infrastructure.Repository.IRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, APIResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;

    
        
    public UpdateUserCommandHandler(UserManager<User> userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
            _userRepository = userRepository;
        
    }

    public async Task<APIResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        {
            var user = await _userRepository.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new APIResponse { Success = false, Message = "User not found" };
            }

            user.UserName = request.Username;
            user.Email = request.Email;

            var updateResult = await _userRepository.UpdateUserAsync(user);
            if (!updateResult.Succeeded)
            {
                return new APIResponse { Success = false, Message = "Failed to update user" };
            }

            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                var removePasswordResult = await _userRepository.RemovePasswordAsync(user);
                if (!removePasswordResult.Succeeded)
                {
                    return new APIResponse { Success = false, Message = "Failed to remove old password" };
                }

                var addPasswordResult = await _userRepository.AddPasswordAsync(user, request.NewPassword);
                if (!addPasswordResult.Succeeded)
                {
                    return new APIResponse { Success = false, Message = "Failed to add new password" };
                }
            }

            return new APIResponse { Success = true, Message = "User updated successfully" };
        }
    }
}