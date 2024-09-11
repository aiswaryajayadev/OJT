using Application.Command_Handler;
using Application.Commands;
using Application.Common;
using Infrastructure.Models;
using Infrastructure.Repository.IRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;

using Serilog;



public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, APIResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;




    public UpdateUserCommandHandler(UserManager<User> userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
            _userRepository = userRepository;
        _logger = Log.ForContext<CreateUserCommandHandler>();

    }

    public async Task<APIResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.Information("Handling UpdateUserCommand for UserId: {@UserId}", request.UserId);

        var user = await _userRepository.FindByIdAsync(request.UserId);
            if (user == null)
            {
            _logger.Warning("User with Id: {@UserId} not found.", request.UserId);
            return new APIResponse { Success = false, Message = "User not found" };
            }
        _logger.Information("Found User: {@UserName}, proceeding with update.", user.UserName);


        user.UserName = request.Username;
            user.Email = request.Email;

        _logger.Information("Updating user: {@UserName}, Email: {@Email}", user.UserName, user.Email);


        var updateResult = await _userRepository.UpdateUserAsync(user);
            if (!updateResult.Succeeded)
            {
            _logger.Error("Failed to update user: {@UserName}", user.UserName);
            return new APIResponse { Success = false, Message = "Failed to update user" };
            }
        _logger.Information("User update successful for UserId: {@UserId}", request.UserId);

        if (!string.IsNullOrEmpty(request.NewPassword))
            {
            _logger.Information("Updating password for User: {@UserName}", user.UserName);

            var removePasswordResult = await _userRepository.RemovePasswordAsync(user);
                if (!removePasswordResult.Succeeded)
                {
                _logger.Error("Failed to remove old password for User: {@UserName}", user.UserName);

                return new APIResponse { Success = false, Message = "Failed to remove old password" };
                }

            _logger.Information("Old password removed successfully for User: {@UserName}", user.UserName);

            var addPasswordResult = await _userRepository.AddPasswordAsync(user, request.NewPassword);
                if (!addPasswordResult.Succeeded)
                {
                _logger.Information("Old password removed successfully for User: {@UserName}", user.UserName);

                return new APIResponse { Success = false, Message = "Failed to add new password" };
                }
            _logger.Information("New password added successfully for User: {@UserName}", user.UserName);

        }
        _logger.Information("Update process completed for UserId: {@UserId}", request.UserId);


        return new APIResponse { Success = true, Message = "User updated successfully" };
        
    }
}